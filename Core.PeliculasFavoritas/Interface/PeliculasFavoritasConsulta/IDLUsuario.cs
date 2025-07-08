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

        #endregion
    }
}
