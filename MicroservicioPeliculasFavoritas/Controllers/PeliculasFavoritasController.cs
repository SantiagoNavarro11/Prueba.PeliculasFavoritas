namespace Api.Parametricas.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Core.PeliculasFavoritas.Entidades;
    using AutoMapper;
    using Core.PeliculasFavoritas.Interface;
    using Core.PeliculasFavoritas.Dto;
    using Core.PeliculasFavoritas.EntidadesPersonalizadas;

    /// <summary>
    /// Controlador API para gestionar las operaciones relacionadas con las películas favoritas de los usuarios.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PeliculasFavoritasController : ControllerBase
    {
        #region Variables

        /// <summary>
        /// Inyección de dependencias de la unidad de trabajo de servicios,
        /// utilizada para acceder a la lógica de negocio.
        /// </summary>
        private readonly IServicioUnidadDeTrabajo _iServicioUnidadDeTrabajo;

        /// <summary>
        /// Inyección de dependencia de AutoMapper para la conversión entre entidades y DTOs.
        /// </summary>
        private readonly IMapper _iMapper;

        #endregion

        #region Constructor

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PeliculasFavoritasController"/>.
        /// </summary>
        /// <param name="iMapper">Instancia de AutoMapper para la conversión de modelos.</param>
        /// <param name="iServiceUnitOfWork">Instancia de la unidad de trabajo de servicios.</param>
        public PeliculasFavoritasController(IMapper iMapper, IServicioUnidadDeTrabajo iServiceUnitOfWork)
        {
            _iMapper = iMapper;
            _iServicioUnidadDeTrabajo = iServiceUnitOfWork;
        }

        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Consulta la lista de películas favoritas registradas, filtradas por los parámetros proporcionados.
        /// </summary>
        /// <param name="parametrosBusqueda">
        /// Objeto que contiene los criterios de búsqueda, como usuario, año, título, etc.
        /// </param>
        /// <returns>
        /// Una respuesta HTTP con una lista de objetos <see cref="PeliculasFavoritasDto"/>,
        /// o un código de estado HTTP correspondiente en caso de error.
        /// </returns>
        [HttpGet]
        [Route("ConsultarPeliculasFavoritas")]
        public async Task<ActionResult<IEnumerable<PeliculasFavoritasDto>>> ConsultarPeliculasFavoritas([FromQuery] ParametrosPeliculasFavoritas parametrosBusqueda)
        {
            IEnumerable<PeliculasFavoritasDto> listaPeliculasFavoritas =
                await _iServicioUnidadDeTrabajo.PeliculasFavoritasServicio.ConsultarPeliculasFavoritas(
                    _iMapper.Map<ParametrosPeliculasFavoritas>(parametrosBusqueda));

            return Ok(_iMapper.Map<List<PeliculasFavoritas>>(listaPeliculasFavoritas?.ToList()));
        }

        #endregion
    }
}
