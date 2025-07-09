namespace Core.PeliculasFavoritas.Servicios
{
    using Core.PeliculasFavoritas.Dto;
    using Core.PeliculasFavoritas.Entidades;
    using Core.PeliculasFavoritas.EntidadesPersonalizadas;
    using Core.PeliculasFavoritas.Interface;
    using Core.PeliculasFavoritas.Interface.PeliculasFavoritasConsulta;
    using Utilitarios.Expresiones;

    /// <summary>
    /// Servicio para manejar las operaciones relacionadas con la entidad <see cref="Usuario"/>.
    /// </summary>
    public class UsuarioServicios : IUsuarioServicio
    {
        #region Variables

        /// <summary>
        /// Unidad de trabajo que encapsula todos los repositorios del sistema.
        /// </summary>
        private readonly IDLUnidadDeTrabajo _iDLUnidadDeTrabajo;

        #endregion

        #region Constructor

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="UsuarioServicios"/>.
        /// </summary>
        /// <param name="iDLUnitOfWork">Unidad de trabajo para acceder a los repositorios de datos.</param>
        public UsuarioServicios(IDLUnidadDeTrabajo iDLUnitOfWork)
        {
            _iDLUnidadDeTrabajo = iDLUnitOfWork;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Consulta todos los usuarios que coincidan con los parámetros de búsqueda proporcionados.
        /// </summary>
        /// <param name="parametrosBusqueda">
        /// Objeto que contiene los filtros para realizar la búsqueda, como el correo electrónico.
        /// </param>
        /// <returns>
        /// Una colección de objetos <see cref="UsuarioDto"/> que cumplen con los criterios especificados.
        /// </returns>
        public async Task<IEnumerable<UsuarioDto>> ConsultarUsuarios(ParametrosUsuario parametrosBusqueda)
        {
            return await _iDLUnidadDeTrabajo.DLUsuario.ConsultarUsuarios(parametrosBusqueda);
        }

        /// <summary>
        /// Inserta un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="usuario">Entidad con los datos del usuario a insertar.</param>
        /// <returns>Entidad del usuario insertado.</returns>
        public async Task<Usuario> InsertarUsuario(Usuario usuario)
        {
            string errores = string.Empty;

            if (usuario == null)
                errores += "El usuario no puede ser nulo. | ";

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                errores += "El nombre del usuario es obligatorio. | ";

            if (string.IsNullOrWhiteSpace(usuario.Correo))
                errores += "El correo del usuario es obligatorio. | ";
            else if (!ExpresionesRegulares.EsCorreoValido(usuario.Correo))
                errores += "El correo electrónico no es válido. | ";

            if (string.IsNullOrWhiteSpace(usuario.Contrasena))
                errores += "La contraseña del usuario es obligatoria. | ";

            if (usuario.FechaRegistro == default)
                usuario.FechaRegistro = DateTime.UtcNow;

            // Consulta si ya existe el correo
            if (string.IsNullOrWhiteSpace(errores))
            {
                var existentes = await _iDLUnidadDeTrabajo.DLUsuario.ConsultarUsuarios(new ParametrosUsuario { Correo = usuario.Correo });
                if (existentes.Any())
                    errores += "Ya existe un usuario con ese correo. | ";
            }

            if (!string.IsNullOrWhiteSpace(errores))
                throw new ArgumentException(errores.Trim().TrimEnd('|'));

            usuario.Contrasena = ExpresionesRegulares.GenerarHashSha256(usuario.Contrasena);

            usuario.Id = await _iDLUnidadDeTrabajo.DLUsuario.InsertarUsuario(usuario);
            return usuario;
        }
        /// <summary>
        /// Valida las credenciales ingresadas por el usuario (correo y contraseña).
        /// </summary>
        /// <param name="correo">Correo electrónico ingresado por el usuario.</param>
        /// <param name="contrasena">Contraseña ingresada por el usuario.</param>
        /// <returns>
        /// Una tupla que contiene un <see cref="UsuarioDto"/> si las credenciales son válidas, y un mensaje de error en caso contrario.
        /// </returns>
        public Task<(UsuarioDto? usuario, string error)> ValidarCredenciales(string correo, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contrasena))
                return Task.FromResult<(UsuarioDto?, string)>((null, "El correo y la contraseña son obligatorios."));

            // Resto del código debe ser async si vas a usar await
            return ValidarCredencialesInterno(correo, contrasena);
        }
        /// <summary>
        /// Lógica interna que realiza la validación de credenciales del usuario.
        /// </summary>
        /// <param name="correo">Correo del usuario.</param>
        /// <param name="contrasena">Contraseña en texto plano ingresada por el usuario.</param>
        /// <returns>
        /// Una tupla que contiene el <see cref="UsuarioDto"/> si la autenticación es exitosa, o un mensaje de error si falla.
        /// </returns>
        private async Task<(UsuarioDto?, string)> ValidarCredencialesInterno(string correo, string contrasena)
        {
            var usuario = await _iDLUnidadDeTrabajo.DLUsuario.ValidarCredenciales(correo, contrasena);
            if (usuario == null)
                return (null, "El usuario no existe.");

            bool esValida = ExpresionesRegulares.VerificarHash(contrasena, usuario.Contrasena);
            if (!esValida)
                return (null, "Contraseña incorrecta.");

            usuario.Contrasena = "";
            return (usuario, "");
        }

        public async Task<bool> EliminarUsuario(int id)
        {
            return await _iDLUnidadDeTrabajo.DLUsuario.EliminarUsuario(id);
        }

        /// <inheritdoc/>
        public async Task<UsuarioDto> ConsultarUsuarioPorId(int id)
        {
            return await _iDLUnidadDeTrabajo.DLUsuario.ConsultarUsuarioPorId(id);
        }

        public async Task<bool> ActualizarUsuario(Usuario usuario)
        {

            return await _iDLUnidadDeTrabajo.DLUsuario.ActualizarUsuario(usuario.Id, usuario);
        }
        #endregion
    }
}
