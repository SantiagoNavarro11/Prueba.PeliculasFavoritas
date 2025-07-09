namespace Core.PeliculasFavoritas.Dto
{
    /// <summary>
    /// Objeto de transferencia de datos (DTO) utilizado para la autenticación de usuarios en el sistema.
    /// Contiene las credenciales necesarias para validar el inicio de sesión.
    /// </summary>
    public class LoginDto
    {
        #region Propiedades

        /// <summary>
        /// Correo electrónico del usuario que desea iniciar sesión.
        /// </summary>
        public string? Correo { get; set; }

        /// <summary>
        /// Contraseña del usuario correspondiente al correo proporcionado.
        /// </summary>
        public string? Contrasena { get; set; }

        #endregion
    }
}
