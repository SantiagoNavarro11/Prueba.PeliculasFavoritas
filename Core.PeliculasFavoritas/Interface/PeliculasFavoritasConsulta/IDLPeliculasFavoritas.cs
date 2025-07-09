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

        /// <summary>
        /// Inserta un nuevo registro de película favorita.
        /// </summary>
        /// <param name="peliculasFavorita">peliculasFavorita con la información de la película a registrar.</param>
        /// <returns>
        /// Identificador único de la película insertada.
        /// </returns>
        Task<int> InsertarPeliculaFavorita(PeliculasFavoritas peliculasFavorita);

        /// <summary>
        /// Actualiza los datos de una película favorita existente.
        /// </summary>
        /// <param name="id">Identificador de la película a actualizar.</param>
        /// <param name="peliculasFavorita">peliculasFavorita con los nuevos valores de la película.</param>
        /// <returns>
        /// Valor booleano que indica si la actualización fue exitosa.
        /// </returns>
        Task<bool> ActualizarPeliculaFavorita(int id, PeliculasFavoritas peliculasFavorita);

        /// <summary>
        /// Elimina una película favorita del usuario.
        /// </summary>
        /// <param name="id">Identificador de la película a eliminar.</param>
        /// <returns>
        /// Valor booleano que indica si la eliminación fue exitosa.
        /// </returns>
        Task<bool> EliminarPeliculaFavorita(int id);

        /// <summary>
        /// Consulta una película favorita específica por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la película.</param>
        /// <returns>
        /// Objeto <see cref="PeliculasFavoritasDto"/> correspondiente al registro solicitado.
        /// </returns>
        Task<PeliculasFavoritasDto> ConsultarPeliculaPorId(int id);

        Task<bool> ExistePeliculaFavorita(int usuarioId, string imdbID);

        #endregion
    }
}
