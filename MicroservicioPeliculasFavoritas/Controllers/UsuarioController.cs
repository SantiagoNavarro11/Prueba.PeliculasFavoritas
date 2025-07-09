using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Core.PeliculasFavoritas.Dto;
using Core.PeliculasFavoritas.Entidades;
using Core.PeliculasFavoritas.EntidadesPersonalizadas;
using Core.PeliculasFavoritas.Interface;

namespace Api.Parametricas.Controllers
{
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var (usuario, error) = await _iServicioUnidadDeTrabajo.UsuarioServicio.ValidarCredenciales(login.Correo, login.Contrasena);

            if (!string.IsNullOrWhiteSpace(error))
                return BadRequest(new { mensaje = error });

            return Ok(usuario);
        }
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
            var listaUsuarios = await _iServicioUnidadDeTrabajo.UsuarioServicio.ConsultarUsuarios(_iMapper.Map<ParametrosUsuario>(parametrosBusqueda));

            return Ok(listaUsuarios?.ToList());
        }
        /// <summary>
        /// Inserta un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="usuarioDto">Objeto con los datos del usuario a insertar.</param>
        /// <returns>Respuesta HTTP que indica el resultado de la operación.</returns>
        [HttpPost]
        [Route("InsertarUsuario")]
        public async Task<IActionResult> InsertarUsuario([FromBody] UsuarioDto usuarioDto)
        {
            try
            {
                var entidad = _iMapper.Map<Usuario>(usuarioDto);

                await _iServicioUnidadDeTrabajo.UsuarioServicio.InsertarUsuario(entidad);

                return Ok("Usuario registrado exitosamente.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                // Error no controlado
                return StatusCode(500, new { mensaje = "Error inesperado al insertar el usuario.", detalle = ex.Message });
            }
        }

        /// <summary>
        /// Actualiza un usuario existente en la base de datos.
        /// </summary>
        /// <param name="usuarioDto">Objeto con los datos actualizados del usuario.</param>
        /// <returns>Respuesta HTTP que indica el resultado de la operación.</returns>
        [HttpPut]
        [Route("ActualizarUsuario")]
        public async Task<IActionResult> ActualizarUsuario([FromBody] UsuarioDto usuarioDto)
        {
            var entidad = _iMapper.Map<Usuario>(usuarioDto);
            await _iServicioUnidadDeTrabajo.UsuarioServicio.ActualizarUsuario(entidad);
            return Ok("Usuario actualizado correctamente.");
        }

        /// <summary>
        /// Elimina un usuario de la base de datos según su identificador.
        /// </summary>
        /// <param name="id">Identificador único del usuario a eliminar.</param>
        /// <returns>Respuesta HTTP que indica el resultado de la operación.</returns>
        [HttpDelete]
        [Route("EliminarUsuario/{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            await _iServicioUnidadDeTrabajo.UsuarioServicio.EliminarUsuario(id);
            return Ok("Usuario eliminado correctamente.");
        }

        #endregion
    }
}
