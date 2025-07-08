namespace Api.Parametricas.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using AutoMapper;
    using Core.PeliculasFavoritas.Dto;
    using Core.PeliculasFavoritas.EntidadesPersonalizadas;
    using Core.PeliculasFavoritas.Interface;

    /// <summary>
    /// Controlador API para gestionar las operaciones relacionadas con los usuarios del sistema.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
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
        /// Inicializa una nueva instancia de la clase <see cref="UsuarioController"/>.
        /// </summary>
        /// <param name="iMapper">Instancia de AutoMapper para la conversión de modelos.</param>
        /// <param name="iServiceUnitOfWork">Instancia de la unidad de trabajo de servicios.</param>
        public UsuarioController(IMapper iMapper, IServicioUnidadDeTrabajo iServiceUnitOfWork)
        {
            _iMapper = iMapper;
            _iServicioUnidadDeTrabajo = iServiceUnitOfWork;
        }

        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Consulta la lista de usuarios registrados según los parámetros de búsqueda especificados.
        /// </summary>
        /// <param name="parametrosBusqueda">
        /// Objeto que contiene los criterios de búsqueda para filtrar los usuarios.
        /// </param>
        /// <returns>
        /// Una respuesta HTTP con una lista de objetos <see cref="UsuarioDto"/>,
        /// o un código de estado HTTP correspondiente en caso de error.
        /// </returns>
        [HttpGet]
        [Route("ConsultarUsuarios")]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> ConsultarUsuarios([FromQuery] ParametrosUsuario parametrosBusqueda)
        {
            var listaUsuarios = await _iServicioUnidadDeTrabajo.PeliculasFavoritasServicio.ConsultarUsuarios(
                _iMapper.Map<ParametrosUsuario>(parametrosBusqueda));

            return Ok(listaUsuarios?.ToList());
        }

        #endregion
    }
}
