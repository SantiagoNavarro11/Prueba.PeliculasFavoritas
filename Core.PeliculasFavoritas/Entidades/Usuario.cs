using Utilitarios.Entidades;

namespace Core.PeliculasFavoritas.Entidades
{
    /// <summary>
    /// Representa a un usuario dentro del sistema.
    /// </summary>
    public class Usuario : EntidadBase
    {
        /// <value>
        /// Identificador único del usuario.
        /// </value>
        public int Id { get; set; }

        /// <value>
        /// Nombre completo del usuario.
        /// </value>
        public required string? Nombre { get; set; }

        /// <value>
        /// Correo electrónico del usuario.
        /// </value>
        public required string Correo { get; set; }

        /// <value>
        /// Contraseña del usuario (preferiblemente almacenada de forma segura).
        /// </value>
        public required string Contrasena { get; set; }

        /// <value>
        /// Fecha en la que el usuario fue registrado en el sistema.
        /// </value>
        public DateTime FechaRegistro { get; set; }

        /// <value>
        /// Lista de películas favoritas asociadas al usuario.
        /// </value>
        public List<PeliculasFavoritas>? PeliculasFavoritas { get; set; }
    }
}
