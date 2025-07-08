namespace Core.PeliculasFavoritas.Dto
{
    using Core.PeliculasFavoritas.Entidades;
    /// <summary>
    /// Objeto de transferencia de datos para crear una nueva película favorita.
    /// </summary>
    public class PeliculasFavoritasDto
    {
        /// <value>
        /// Identificador único de la película favorita.
        /// </value>
        public int Id { get; set; }

        /// <value>
        /// Identificador del usuario que agregó esta película como favorita.
        /// </value>
        public int UsuarioId { get; set; }

        /// <value>
        /// Usuario al que pertenece esta película favorita.
        /// </value>
        public Usuario Usuario { get; set; }

        /// <value>
        /// Identificador único en IMDb proporcionado por la API externa.
        /// </value>
        public string ImdbID { get; set; }

        /// <value>
        /// Título completo de la película.
        /// </value>
        public string? Titulo { get; set; }

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

        /// <value>
        /// URL del póster oficial de la película.
        /// </value>
        public string? PosterUrl { get; set; }

        /// <value>
        /// Sinopsis o descripción general de la película.
        /// </value>
        public string? Sinopsis { get; set; }

        /// <value>
        /// Calificación promedio de la película según IMDb.
        /// </value>
        public decimal? CalificacionIMDB { get; set; }

        /// <value>
        /// Duración total de la película (por ejemplo: "136 min").
        /// </value>
        public string? Duracion { get; set; }

        /// <value>
        /// Idioma principal de la película.
        /// </value>
        public string? Idioma { get; set; }

        /// <value>
        /// País o países de origen de la película.
        /// </value>
        public string? Pais { get; set; }

        /// <value>
        /// Lista de actores principales que participaron en la película.
        /// </value>
        public string? Actores { get; set; }

        /// <value>
        /// Premios y reconocimientos obtenidos por la película.
        /// </value>
        public string? Premios { get; set; }

        /// <value>
        /// Fecha en la que el usuario agregó esta película como favorita.
        /// </value>
        public DateTime FechaAgregada { get; set; }
    }
}
