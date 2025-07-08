namespace Core.PeliculasFavoritas.Interface
{
    using Core.PeliculasFavoritas.Interface.PeliculasFavoritasConsulta;
    using System.Threading.Tasks;

    /// <summary>
    /// Define las operaciones para la unidad de trabajo (IServicioUnidadDeTrabajo) en el contexto de peliculas favoritas.
    /// La unidad de trabajo gestiona las instancias de repositorios relacionados con las entidades de la base de datos.
    /// </summary>
    public interface IDLUnidadDeTrabajo
    {

        #region Instancias

        /// <summary>
        /// Instancia del repositorio de Peliculas Favoritas.
        /// Este repositorio se encarga de las operaciones CRUD sobre la entidad de Peliculas Favoritas.
        /// </summary>
        IDLPeliculasFavoritas DLPeliculasFavoritas { get; }

        /// <summary>
        /// Instancia del repositorio de Usuario.
        /// Este repositorio se encarga de las operaciones CRUD sobre la entidad de Usuario.
        /// </summary>
        IDLUsuario DLUsuario { get; }

        #endregion

        #region Liberar Conexión

        /// <summary>
        /// Libera la conexión con la base de datos de manera sincrónica.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Libera la conexión con la base de datos de manera asincrónica.
        /// </summary>
        Task DisposeAsync();

        #endregion

        #region Guardar Cambios

        /// <summary>
        /// Guarda los cambios realizados en la conexión a la base de datos de manera sincrónica.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Guarda los cambios realizados en la conexión a la base de datos de manera asincrónica.
        /// </summary>
        Task SaveChangesAsync();

        #endregion
    }
}
