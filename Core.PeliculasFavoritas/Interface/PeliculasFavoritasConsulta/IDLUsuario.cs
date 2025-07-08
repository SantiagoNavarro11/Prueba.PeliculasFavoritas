namespace Core.PeliculasFavoritas.Interface.PeliculasFavoritasConsulta
{
    using Core.PeliculasFavoritas.Dto;
    using Core.PeliculasFavoritas.Entidades;
    using Core.PeliculasFavoritas.EntidadesPersonalizadas;
    using Utilitarios.Interfaces.ConfiguracionRepositorio;

    /// <summary>
    /// Interfaz que define las operaciones CRUD para la entidad <see cref="Usuario"/>
    /// extendiendo <see cref="ICrudSqlRepositorio{Usuario}"/>.
    /// Incluye la capacidad de realizar consultas específicas sobre los registros de la entidad <see cref="Usuario"/>.
    /// </summary>
    public interface IDLUsuario : ICrudSqlRepositorio<Usuario>
    {
        #region Instancias
        /// <summary>
        /// Consulta los registros de la entidad <see cref="Usuario"/> según los parámetros de búsqueda.
        /// </summary>
        /// <param name="objBusqueda">Parámetros de búsqueda para filtrar los registros de Usuarios.</param>
        /// <returns>
        /// Una lista de objetos <see cref="UsuarioDto"/> que cumplen con los criterios de búsqueda proporcionados.
        /// </returns>
        Task<IEnumerable<UsuarioDto>> ConsultarUsuarios(ParametrosUsuario objBusqueda);
        /// <summary>
        /// Registra un nuevo usuario en el sistema.
        /// </summary>
        /// <param name="dto">DTO con la información del nuevo usuario.</param>
        /// <returns>
        /// Identificador único del usuario insertado.
        /// </returns>
        Task<int> InsertarUsuario(UsuarioDto dto);
        /// <summary>
        /// Actualiza los datos de un usuario existente.
        /// </summary>
        /// <param name="id">Identificador del usuario a actualizar.</param>
        /// <param name="dto">DTO con los nuevos valores del usuario.</param>
        /// <returns>
        /// Valor booleano que indica si la actualización fue exitosa.
        /// </returns>
        Task<bool> ActualizarUsuario(int id, UsuarioDto dto);
        /// <summary>
        /// Elimina un usuario del sistema.
        /// </summary>
        /// <param name="id">Identificador del usuario a eliminar.</param>
        /// <returns>
        /// Valor booleano que indica si la eliminación fue exitosa.
        /// </returns>
        Task<bool> EliminarUsuario(int id);
        /// <summary>
        /// Consulta un usuario específico por su identificador.
        /// </summary>
        /// <param name="id">Identificador del usuario.</param>
        /// <returns>
        /// Objeto <see cref="UsuarioDto"/> correspondiente al usuario solicitado.
        /// </returns>
        Task<UsuarioDto> ConsultarUsuarioPorId(int id);
        /// <summary>
        /// Valida las credenciales de un usuario para el inicio de sesión.
        /// </summary>
        /// <param name="correo">Correo electrónico del usuario.</param>
        /// <param name="contrasena">Contraseña del usuario.</param>
        /// <returns>
        /// Objeto <see cref="UsuarioDto"/> si las credenciales son válidas, de lo contrario null.
        /// </returns>
        Task<UsuarioDto> ValidarCredenciales(string correo, string contrasena);
        #endregion
    }
}
