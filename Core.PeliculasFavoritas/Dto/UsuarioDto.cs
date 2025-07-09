namespace Core.PeliculasFavoritas.Dto
{
    /// <summary>
    /// Objeto de transferencia de datos para crear un Usuario.
    /// </summary>
    public class UsuarioDto
    {
        #region Propiedades
        /// <value>
        /// Identificador único del usuario.
        /// </value>
        public int Id { get; set; }

        /// <value>
        /// Nombre completo del usuario.
        /// </value>
        public string? Nombre { get; set; }

        /// <value>
        /// Correo electrónico del usuario.
        /// </value>
        public string? Correo { get; set; }        

        /// <value>
        /// Fecha en la que el usuario fue registrado en el sistema.
        /// </value>
        public DateTime FechaRegistro { get; set; }

        /// <value>
        /// Contraseña del usuario (preferiblemente almacenada de forma segura).
        /// </value>
        public string? Contrasena { get; set; }
        #endregion
    }
}
