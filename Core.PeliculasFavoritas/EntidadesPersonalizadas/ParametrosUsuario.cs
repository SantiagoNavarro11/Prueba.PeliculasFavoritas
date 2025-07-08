using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.PeliculasFavoritas.EntidadesPersonalizadas
{
    /// <summary>
    /// Objeto de transferencia de datos para recibir parámetros de filtrado y búsqueda
    /// al consultar un usuario.
    /// </summary>
    public class ParametrosUsuario
    {
        /// <value>
        /// Correo electrónico del usuario.
        /// </value>
        public required string Correo { get; set; }
        /// <value>
        /// Fecha en la que el usuario fue registrado en el sistema.
        /// </value>
        public DateTime FechaRegistro { get; set; }
    }
}
