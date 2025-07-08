namespace Core.PeliculasFavoritas.Interface.PeliculasFavoritasConsulta
{
    using Core.PeliculasFavoritas.Dto;
    using Core.PeliculasFavoritas.EntidadesPersonalizadas;
    using Core.PeliculasFavoritas.Entidades;
    using Utilitarios.Interfaces.ConfiguracionRepositorio;

    /// <summary>
    /// Interfaz que define las operaciones CRUD para la entidad <see cref="PeliculasFavoritas"/>
    /// extendiendo <see cref="ICrudSqlRepositorio{PeliculasFavoritas}"/>.
    /// Incluye la capacidad de realizar consultas específicas sobre los registros de la entidad <see cref="PeliculasFavoritas"/>.
    /// </summary>
    public interface IDLPeliculasFavoritas : ICrudSqlRepositorio<PeliculasFavoritas>
    {
        #region Instancias

        /// <summary>
        /// Consulta los registros de la entidad <see cref="PeliculasFavoritas"/> según los parámetros de búsqueda.
        /// </summary>
        /// <param name="objBusqueda">Parámetros de búsqueda para filtrar los registros de PeliculasFavoritas.</param>
        /// <returns>
        /// Una lista de objetos <see cref="PeliculasFavoritasDto"/> que cumplen con los criterios de búsqueda proporcionados.
        /// </returns>
        Task<IEnumerable<PeliculasFavoritasDto>> ConsultarPeliculasFavoritas(ParametrosPeliculasFavoritas objBusqueda);

        #endregion
    }
}
