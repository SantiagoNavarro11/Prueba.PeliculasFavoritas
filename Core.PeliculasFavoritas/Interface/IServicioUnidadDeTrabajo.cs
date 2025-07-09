namespace Core.PeliculasFavoritas.Interface
{
    using Core.PeliculasFavoritas.Interface.PeliculasFavoritasConsulta;
    /// <summary>Contrato que provee cada uno de los servicios para ser implementados.</summary>
    public interface IServicioUnidadDeTrabajo 
    {
        #region Instancias	
        /// <summary>Inicialización y verificación de la instancia para el servicio PeliculasFavoritasServicio.</summary>
        public IPeliculasFavoritasServicio PeliculasFavoritasServicio { get; }

        /// <summary>Inicialización y verificación de la instancia para el servicio .</summary>
        public IUsuarioServicio UsuarioServicio { get; }
        #endregion
    }
}
