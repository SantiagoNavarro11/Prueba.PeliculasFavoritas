namespace InfraestructuraPeliculasFavoritas.Repositorio
{
    using Core.PeliculasFavoritas.Entidades;
    using Core.PeliculasFavoritas.Interface;
    using Core.PeliculasFavoritas.Interface.PeliculasFavoritasConsulta;
    using InfraestructuraPeliculasFavoritas.Mapeos;
    using InfraestructuraPeliculasFavoritas.Repositorio.PeliculasFavoritasConsulta;
    using System.Threading.Tasks;

    /// <summary>
    /// Implementación de la unidad de trabajo (Unit of Work) para el acceso a datos.
    /// Gestiona la instanciación y acceso a los repositorios de entidades,
    /// así como la persistencia y liberación de recursos de la base de datos.
    /// </summary>
    public class DLUnidadDeTrabajo : IDLUnidadDeTrabajo
    {
        #region Variables

        /// <summary>
        /// Contexto de base de datos del CarleraPeliculas utilizado para acceder a las entidades.
        /// </summary>
        private readonly PeliculasFavoritasContext _conexionBD;

        /// <summary>
        /// Instancia del repositorio para la entidad <see cref="Usuario"/>.
        /// </summary>
        private readonly IDLUsuario? _iDLUsuario;

        /// <summary>
        /// Instancia del repositorio para la entidad <see cref="PeliculasFavoritas"/>.
        /// </summary>
        private readonly IDLPeliculasFavoritas _iDLPeliculasFavoritas;

        #endregion

        #region Constructor

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DLUnidadDeTrabajo"/>.
        /// </summary>
        /// <param name="conexionSqlBD">
        /// Instancia del contexto de base de datos <see cref="PeliculasFavoritasContext"/> que será utilizado por los repositorios.
        /// </param>
        public DLUnidadDeTrabajo(PeliculasFavoritasContext conexionSqlBD)
        {
            _conexionBD = conexionSqlBD;
        }

        #endregion

        #region Instancias

        /// <summary>
        /// Obtiene una instancia del repositorio para la entidad <see cref="Usuario"/>.
        /// Si la instancia no existe, se crea una nueva utilizando la conexión actual.
        /// </summary>
        public IDLUsuario DLUsuario => _iDLUsuario ?? new DLUsuario(_conexionBD)!;

        /// <summary>
        /// Obtiene una instancia del repositorio para la entidad <see cref="PeliculasFavoritas"/>.
        /// Si la instancia no existe, se crea una nueva utilizando la conexión actual.
        /// </summary>
        public IDLPeliculasFavoritas DLPeliculasFavoritas => _iDLPeliculasFavoritas ?? new DLPeliculasFavoritas(_conexionBD)!;

        #endregion

        #region Liberar Conexión

        /// <summary>
        /// Libera los recursos no administrados utilizados por el contexto de base de datos de manera sincrónica.
        /// </summary>
        public void Dispose()
        {
            if (_conexionBD != null)
                _conexionBD.Dispose();
        }

        /// <summary>
        /// Libera los recursos no administrados utilizados por el contexto de base de datos de manera asincrónica.
        /// </summary>
        public async Task DisposeAsync()
        {
            if (_conexionBD != null)
                await _conexionBD.DisposeAsync();
        }

        #endregion

        #region Guardar Cambios

        /// <summary>
        /// Guarda de manera sincrónica todos los cambios realizados en el contexto de datos 
        /// y los persiste en la base de datos.
        /// </summary>
        public void SaveChanges() => _conexionBD.SaveChanges();

        /// <summary>
        /// Guarda de manera asincrónica todos los cambios realizados en el contexto de datos 
        /// y los persiste en la base de datos.
        /// </summary>
        public async Task SaveChangesAsync() => await _conexionBD.SaveChangesAsync();

        #endregion
    }
}
