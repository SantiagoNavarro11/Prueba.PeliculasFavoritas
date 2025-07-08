using Core.PeliculasFavoritas.Dto;
using Core.PeliculasFavoritas.Entidades;
using Core.PeliculasFavoritas.EntidadesPersonalizadas;
using Core.PeliculasFavoritas.Interface;
using Core.PeliculasFavoritas.Interface.PeliculasFavoritasConsulta;

namespace Core.SistemaPos.Servicios
{

    /// <summary>
    /// Servicio principal que implementa la interfaz <see cref="ISistemaPosService"/> 
    /// para manejar operaciones relacionadas con el sistema POS.
    /// </summary>
    public class PeliculasFavoritasServicio : IDLPeliculasFavoritasServicio
    {
        #region Variables

        /// <summary>
        /// Unidad de trabajo que encapsula todos los repositorios del sistema POS.
        /// </summary>
        private readonly IDLUnidadDeTrabajo _iDLUnidadDeTrabajo;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor de la clase <see cref="SistemaPosService"/>.
        /// </summary>
        /// <param name="iDLUnitOfWork">Instancia de la unidad de trabajo para acceder a los repositorios de datos.</param>
        public PeliculasFavoritasServicio(IDLUnidadDeTrabajo iDLUnitOfWork)
        {
            _iDLUnidadDeTrabajo = iDLUnitOfWork;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Consulta la lista de formas de pago disponibles en el sistema POS
        /// utilizando los parámetros de búsqueda especificados.
        /// </summary>
        /// <param name="parametrosBusqueda">
        /// Parámetros personalizados de búsqueda de formas de pago.
        /// </param>
        /// <returns>
        /// Una colección enumerable de objetos <see cref="SistemaPosFormasPago"/> 
        /// que cumplen con los criterios de búsqueda.
        /// </returns>
        public async Task<IEnumerable<UsuarioDto>> ConsultarUsuarios(ParametrosUsuario parametrosBusqueda)
        {
            return await _iDLUnidadDeTrabajo.DLUsuario.ConsultarUsuarios(parametrosBusqueda);
        }

        /// <summary>
        /// Consulta la lista de tipos de identificación disponibles en el sistema POS
        /// utilizando los parámetros de búsqueda especificados.
        /// </summary>
        /// <param name="parametrosBusqueda">
        /// Parámetros personalizados de búsqueda de tipos de identificación.
        /// </param>
        /// <returns>
        /// Una colección enumerable de objetos <see cref="SistemaPosTipoIdentificacion"/> 
        /// que cumplen con los criterios de búsqueda.
        /// </returns>
        public async Task<IEnumerable<PeliculasFavoritasDto>> ConsultarPeliculasFavoritas(ParametrosPeliculasFavoritas parametrosBusqueda)
        {
            return await _iDLUnidadDeTrabajo.DLPeliculasFavoritas.ConsultarPeliculasFavoritas(parametrosBusqueda);
        }

      
        #endregion
    }
}
