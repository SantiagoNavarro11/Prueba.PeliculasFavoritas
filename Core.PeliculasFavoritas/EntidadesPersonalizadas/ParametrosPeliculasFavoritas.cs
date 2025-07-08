using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.PeliculasFavoritas.EntidadesPersonalizadas
{
    /// <summary>
    /// Objeto de transferencia de datos para recibir parámetros de filtrado y búsqueda
    /// al consultar las películas favoritas de un usuario.
    /// </summary>
    public class ParametrosPeliculasFavoritas
    {
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
    }
}
