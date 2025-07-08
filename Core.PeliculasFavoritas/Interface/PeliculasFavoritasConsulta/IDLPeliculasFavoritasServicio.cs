namespace Core.PeliculasFavoritas.Interface.PeliculasFavoritasConsulta
{
    using Core.PeliculasFavoritas.Dto;
    using Core.PeliculasFavoritas.Entidades;
    using Core.PeliculasFavoritas.EntidadesPersonalizadas;

    /// <summary>
    /// Interfaz que define los servicios relacionados con la consulta de la entidad <see cref="PeliculasFavoritas"/>.
    /// Esta interfaz se utiliza para servicios específicos que requieren realizar consultas sobre registros de Peliculas Favoritas.
    /// </summary>
    public interface IDLPeliculasFavoritasServicio
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
        /// Consulta los registros de la entidad <see cref="Usuario"/> según los parámetros de búsqueda.
        /// </summary>
        /// <param name="parametrosBusqueda">Parámetros de entrada para la consulta de registros de Usuario.</param>
        /// <returns>
        /// Una lista de objetos <see cref="UsuarioDto"/> que cumplen con los criterios de búsqueda proporcionados.
        /// </returns>
        Task<IEnumerable<UsuarioDto>> ConsultarUsuarios(ParametrosUsuario parametrosBusqueda);

        #endregion
    }
}
