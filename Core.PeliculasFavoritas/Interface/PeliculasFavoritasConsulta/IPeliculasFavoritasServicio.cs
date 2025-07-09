namespace Core.PeliculasFavoritas.Interface.PeliculasFavoritasConsulta
{
    using Core.PeliculasFavoritas.Dto;
    using Core.PeliculasFavoritas.Entidades;
    using Core.PeliculasFavoritas.EntidadesPersonalizadas;

    /// <summary>
    /// Interfaz que define los servicios relacionados con la consulta de la entidad <see cref="PeliculasFavoritas"/>.
    /// Esta interfaz se utiliza para servicios específicos que requieren realizar consultas sobre registros de Peliculas Favoritas.
    /// </summary>
    public interface IPeliculasFavoritasServicio
    {
        #region Instancias

        /// <summary>
        /// Consulta los registros de la entidad <see cref="PeliculasFavoritas"/> según los parámetros de búsqueda.
        /// </summary>
        /// <param name="parametrosBusqueda">Parámetros de entrada para la consulta de registros de Peliculas Favoritas.</param>
        /// <returns>
        /// Una lista de objetos <see cref="PeliculasFavoritasDto"/> que cumplen con los criterios de búsqueda proporcionados.
        /// </returns>
        Task<IEnumerable<PeliculasFavoritasDto>> ConsultarPeliculasFavoritas(ParametrosPeliculasFavoritas parametrosBusqueda);

        /// <summary>
        /// Inserta un nuevo registro en la tabla de Películas Favoritas.
        /// </summary>
        /// <param name="pelicula">Entidad <see cref="PeliculasFavoritas"/> a insertar.</param>
        /// <returns>Una tarea asincrónica que representa la operación de inserción.</returns>
        Task<PeliculasFavoritas> InsertarPelicula(PeliculasFavoritas pelicula);

        /// <summary>
        /// Actualiza un registro existente de Película Favorita.
        /// </summary>
        /// <param name="pelicula">Entidad <see cref="PeliculasFavoritas"/> con los datos actualizados.</param>
        /// <returns>Una tarea asincrónica que representa la operación de actualización.</returns>
        Task<bool> ActualizarPelicula(PeliculasFavoritas pelicula);

        /// <summary>
        /// Elimina un registro de Película Favorita según su identificador.
        /// </summary>
        /// <param name="id">Identificador único de la película a eliminar.</param>
        /// <returns>Una tarea asincrónica que representa la operación de eliminación.</returns>
        Task<bool> EliminarPelicula(int id);

        #endregion
    }
}
