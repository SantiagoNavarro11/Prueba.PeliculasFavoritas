namespace InfraestructuraPeliculasFavoritas.Mapeos
{
    using Core.PeliculasFavoritas.Dto;
    using Core.PeliculasFavoritas.Entidades;
    using AutoMapper;

    /// <summary>Mapeos entre Entidades <-> Dtos y viceversa.</summary>
    public class AutomapperProfile : Profile
    {
        #region Constructor
        /// <summary>Inicializa una nueva instancia de la clase <see cref="AutomapperProfile"/>.</summary>
        public AutomapperProfile()
        {
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<PeliculasFavoritas, PeliculasFavoritasDto>().ReverseMap();
        }
        #endregion
    }
}
