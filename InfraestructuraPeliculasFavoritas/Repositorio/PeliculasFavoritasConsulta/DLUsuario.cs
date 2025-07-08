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

        public void Actualizar(Usuario objActualizar)
        {
            throw new NotImplementedException();
        }

        public Task ActualizarAsync(Usuario objActualizar)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ActualizarUsuario(int id, UsuarioDto dto)
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

        public Task<IEnumerable<Usuario>> ConsultarLista(Expression<Func<Usuario, bool>> objBusqueda)
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

        public Task<UsuarioDto> ConsultarUsuarioPorId(int id)
        {
            throw new NotImplementedException();
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
        })
          .AsNoTracking()
          .ToListAsync();
            return registro;
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

        public Task<bool> EliminarUsuario(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertarUsuario(UsuarioDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioDto> ValidarCredenciales(string correo, string contrasena)
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

        Task<Usuario> ICrudSqlRepositorio<Usuario>.ConsultarPorId(int id)
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
        #endregion
    }
}
