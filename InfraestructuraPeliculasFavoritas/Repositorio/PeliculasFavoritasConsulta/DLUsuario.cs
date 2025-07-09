namespace InfraestructuraPeliculasFavoritas.Repositorio.PeliculasFavoritasConsulta
{
    using Core.PeliculasFavoritas.Dto;
    using Core.PeliculasFavoritas.Entidades;
    using Core.PeliculasFavoritas.EntidadesPersonalizadas;
    using Core.PeliculasFavoritas.Interface.PeliculasFavoritasConsulta;
    using InfraestructuraPeliculasFavoritas.Mapeos;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using Utilitarios.ConfiguracionRepositorio;
    using Utilitarios.Interfaces.ConfiguracionRepositorio;

    /// <summary>
    /// Repositorio de acceso a datos para las operaciones relacionadas con los usuarios.
    /// </summary>
    public class DLUsuario : CrudSqlRepositorio<PeliculasFavoritas>, IDLUsuario
    {
        #region Variables
        /// <summary>
        /// Contexto de la base de datos utilizado para acceder a los datos de la tabla Persona.
        /// </summary>
        private readonly PeliculasFavoritasContext contextDB;
        #endregion

        #region Constructor
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DLPersona"/>.
        /// </summary>
        /// <param name="context">Contexto de base de datos para realizar las consultas.</param>
        public DLUsuario(PeliculasFavoritasContext context) : base(context)
        {
            contextDB = context;
        }

        #endregion

        #region Consulta Personalizada
        /// <summary>
        /// Consulta los registros de personas en la base de datos según los parámetros proporcionados.
        /// Permite realizar la búsqueda por el ID de la persona o por el código de Transmilenio.
        /// También utiliza la funcionalidad de "LIKE" en el campo PersonaCodigoTransmilenio.
        /// </summary>
        /// <param name="objBusqueda">Parámetros de búsqueda para filtrar los registros de personas.</param>
        /// <returns>Una lista de personas que coinciden con los criterios de búsqueda.</returns>
        public async Task<IEnumerable<UsuarioDto>> ConsultarUsuarios(ParametrosUsuario objBusqueda)
        {
            var registro = await contextDB.Usuario
            .Include(u => u.PeliculasFavoritas) // Solo si la relación existe en la entidad Usuario
            .Where(u =>
            (string.IsNullOrEmpty(objBusqueda.Correo) || u.Correo.Contains(objBusqueda.Correo)) 
        )
            .Select(u => new UsuarioDto
        {
            Id  = u.Id,
            Nombre = u.Nombre,
            Correo = u.Correo,
            FechaRegistro = u.FechaRegistro,   
            Contrasena = u.Contrasena,
        })
          .AsNoTracking()
          .ToListAsync();
            return registro;
        }
        /// <summary>
        /// Actualiza los datos de un usuario existente en la base de datos.
        /// </summary>
        /// <param name="id">Identificador del usuario a actualizar.</param>
        /// <param name="usuario">Objeto con los datos actualizados del usuario.</param>
        /// <returns>True si el usuario fue actualizado correctamente; false si no se encontró el usuario.</returns>
        public async Task<bool> ActualizarUsuario(int id, Usuario usuario)
        {
            var existente = await contextDB.Usuario.FindAsync(id);
            if (existente == null)
                return false;

            // Actualiza campos necesarios
            existente.Nombre = usuario.Nombre;
            existente.Correo = usuario.Correo;
            existente.Contrasena = usuario.Contrasena;
            existente.FechaRegistro = usuario.FechaRegistro;

            contextDB.Usuario.Update(existente);
            await contextDB.SaveChangesAsync();
            return true;
        }
        /// <summary>
        /// Consulta un usuario por correo electrónico para validar credenciales de inicio de sesión.
        /// </summary>
        /// <param name="correo">Correo electrónico del usuario.</param>
        /// <param name="contrasena">Contraseña del usuario (sin encriptar).</param>
        /// <returns>DTO con los datos del usuario si existe; null si no se encuentra.</returns>
        public async Task<UsuarioDto?> ValidarCredenciales(string correo, string contrasena)

        {
            return await contextDB.Usuario
                .Where(u => u.Correo == correo)
                .Select(u => new UsuarioDto
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Correo = u.Correo,
                    FechaRegistro = u.FechaRegistro,
                    Contrasena = u.Contrasena
                })
                .FirstOrDefaultAsync();
        }
        /// <summary>
        /// Elimina un usuario de la base de datos a partir de su identificador.
        /// </summary>
        /// <param name="id">Identificador del usuario a eliminar.</param>
        /// <returns>True si el usuario fue eliminado correctamente; false si no se encontró.</returns>
        public async Task<bool> EliminarUsuario(int id)
        {
            var usuario = await contextDB.Usuario.FindAsync(id);
            if (usuario == null)
                return false;

            contextDB.Usuario.Remove(usuario);
            await contextDB.SaveChangesAsync();
            return true;
        }
        /// <summary>
        /// Inserta un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="usuario">Entidad con los datos del usuario a registrar.</param>
        /// <returns>Identificador del usuario insertado.</returns>
        public async Task<int> InsertarUsuario(Usuario usuario)
        {
            await contextDB.Usuario.AddAsync(usuario);
            await contextDB.SaveChangesAsync();
            return usuario.Id; // Asegúrate que 'Id' es la clave primaria
        }
        /// <summary>
        /// Consulta los datos de un usuario específico por su identificador.
        /// </summary>
        /// <param name="id">Identificador del usuario a consultar.</param>
        /// <returns>DTO con los datos del usuario si se encuentra; null si no existe.</returns>

        public async Task<UsuarioDto> ConsultarUsuarioPorId(int id)
        {
            var usuario = await contextDB.Usuario
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
                return null;

            return new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                Contrasena = usuario.Contrasena,
                FechaRegistro = usuario.FechaRegistro
            };
        }
        public void Eliminar(Usuario objEliminar)
        {
            throw new NotImplementedException();
        }

        public Task EliminarAsync(Usuario objEliminar)
        {
            throw new NotImplementedException();
        }

        public Task EliminarMasivoAsync(List<Usuario> objEliminar)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(Usuario objActualizar)
        {
            throw new NotImplementedException();
        }

        public Task ActualizarAsync(Usuario objActualizar)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertarUsuario(UsuarioDto dto)
        {
            throw new NotImplementedException();
        }

       

        Task<Usuario> ICrudSqlRepositorio<Usuario>.ConsultarPorId(byte id)
        {
            throw new NotImplementedException();
        }

        Task<Usuario> ICrudSqlRepositorio<Usuario>.ConsultarPorId(short id)
        {
            throw new NotImplementedException();
        }

        Task<Usuario> ICrudSqlRepositorio<Usuario>.ConsultarPorId(long id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Usuario>> ICrudSqlRepositorio<Usuario>.ConsultarStoreProcedureMultiParametro(string procedure, object[] variables, object[] parameters)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Usuario> ICrudSqlRepositorio<Usuario>.ConsultarTodos()
        {
            throw new NotImplementedException();
        }

        Task<Usuario> ICrudSqlRepositorio<Usuario>.ConsultarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> ConsultarObjeto(Expression<Func<Usuario, bool>> objBusqueda)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> ConsultarTodosFiltroQuery(Expression<Func<Usuario, bool>> objBusqueda)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Usuario>> ConsultarLista(Expression<Func<Usuario, bool>> objBusqueda)
        {
            throw new NotImplementedException();
        }

        public Task Adicionar(Usuario objAdicionar)
        {
            throw new NotImplementedException();
        }

        public Task AdicionarMasivo(List<Usuario> lstAdicionar)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
