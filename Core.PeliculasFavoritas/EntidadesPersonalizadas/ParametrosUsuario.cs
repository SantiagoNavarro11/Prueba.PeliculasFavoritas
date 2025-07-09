namespace Core.PeliculasFavoritas.EntidadesPersonalizadas
{
    using System;
    /// <summary>
    /// Parametros de busqueda para el usuario.
    /// </summary>
    public class ParametrosUsuario
    {
        #region Parametros
        /// <value>
        /// Correo electrónico del usuario.
        /// </value>
        public string? Correo { get; set; }
        /// <value>
        /// Fecha en la que el usuario fue registrado en el sistema.
        /// </value>
        public DateTime? FechaRegistro { get; set; }
        #endregion
    }
}
