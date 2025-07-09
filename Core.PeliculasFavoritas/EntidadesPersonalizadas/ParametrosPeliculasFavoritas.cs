namespace Core.PeliculasFavoritas.EntidadesPersonalizadas
{
    /// <summary>
    /// Parametros de busqueda para las peliculas favoritas.
    /// </summary>
    public class ParametrosPeliculasFavoritas
    {
        #region Parametros
        /// <value>
        /// Identificador del usuario que agregó esta película como favorita.
        /// </value>
        public int UsuarioId { get; set; }
        /// <value>
        /// Año de estreno de la película.
        /// </value>
        public int? Anio { get; set; }
        /// <value>
        /// Nombre del director de la película.
        /// </value>
        public string? Director { get; set; }

        /// <value>
        /// Género principal o categoría de la película.
        /// </value>
        public string? Genero { get; set; }
        #endregion
    }
}
