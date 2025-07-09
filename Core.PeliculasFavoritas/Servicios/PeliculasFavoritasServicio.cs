namespace Core.PeliculasFavoritas.Servicios
{
    using Core.PeliculasFavoritas.Dto;
    using Core.PeliculasFavoritas.EntidadesPersonalizadas;
    using Core.PeliculasFavoritas.Interface;
    using Core.PeliculasFavoritas.Interface.PeliculasFavoritasConsulta;

    /// <summary>
    /// Servicio principal que implementa la interfaz <see cref="IPeliculasFavoritasServicio"/> 
    /// para manejar operaciones relacionadas con la gestión de películas favoritas.
    /// </summary>
    public class PeliculasFavoritasServicio : IPeliculasFavoritasServicio
    {
        #region Variables

        /// <summary>
        /// Unidad de trabajo que encapsula todos los repositorios de peliculas favoritas.
        /// </summary>
        private readonly IDLUnidadDeTrabajo _iDLUnidadDeTrabajo;

        #endregion

        #region Constructor

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PeliculasFavoritasServicio"/>.
        /// </summary>
        /// <param name="iDLUnitOfWork">
        /// Instancia de la unidad de trabajo que proporciona acceso a los repositorios de películas y usuarios.
        /// </param>
        public PeliculasFavoritasServicio(IDLUnidadDeTrabajo iDLUnitOfWork)
        {
            _iDLUnidadDeTrabajo = iDLUnitOfWork;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Consulta la lista de películas favoritas según los parámetros de búsqueda especificados.
        /// </summary>
        /// <param name="parametrosBusqueda">Parámetros para filtrar las películas favoritas (género, año, director).</param>
        /// <returns>Una colección de objetos <see cref="PeliculasFavoritasDto"/> que coinciden con los filtros.</returns>
        public async Task<IEnumerable<PeliculasFavoritasDto>> ConsultarPeliculasFavoritas(ParametrosPeliculasFavoritas parametrosBusqueda)
        {
            return await _iDLUnidadDeTrabajo.DLPeliculasFavoritas.ConsultarPeliculasFavoritas(parametrosBusqueda);
        }
        /// <summary>
        /// Inserta una nueva película favorita en la base de datos.
        /// </summary>
        /// <param name="pelicula">Entidad con los datos de la película a insertar.</param>
        /// <returns>Tarea asincrónica de inserción.</returns>
        public async Task<Entidades.PeliculasFavoritas> InsertarPelicula(Entidades.PeliculasFavoritas pelicula)
        {
            string errores = string.Empty;

            // Validación: entidad nula
            if (pelicula == null)
                errores += "La entidad de película no puede ser nula. | ";

            // Validación: UsuarioId requerido
            if (pelicula?.UsuarioId <= 0)
                errores += "El ID del usuario debe ser mayor que cero. | ";

            // Validación: Título obligatorio
            if (string.IsNullOrWhiteSpace(pelicula?.Titulo))
                errores += "El título de la película es obligatorio. | ";

            // Validación: IMDb ID obligatorio
            if (string.IsNullOrWhiteSpace(pelicula?.ImdbID))
                errores += "El ID de IMDb es obligatorio. | ";

            // Validación: Fecha agregada (asignar si es default)
            if (pelicula != null && pelicula.FechaAgregada == default)
                pelicula.FechaAgregada = DateTime.UtcNow;

            // Validación: Verificar existencia del usuario
            if (pelicula != null && pelicula.UsuarioId > 0)
            {
                var usuarioExiste = await _iDLUnidadDeTrabajo.DLUsuario.ConsultarPorId(pelicula.UsuarioId);
                if (usuarioExiste == null)
                    errores += $"No se encontró ningún usuario con el ID {pelicula.UsuarioId}. | ";
            }

            // Lanzar errores si existen
            if (!string.IsNullOrWhiteSpace(errores))
                throw new ArgumentException(errores.Trim().TrimEnd('|'));

            // Inserción
            pelicula.Id = await _iDLUnidadDeTrabajo.DLPeliculasFavoritas.InsertarPeliculaFavorita(pelicula);

            return pelicula;
        }


        /// <summary>
        /// Actualiza una película favorita existente en la base de datos.
        /// </summary>
        /// <param name="pelicula">Entidad con los datos actualizados de la película.</param>
        /// <returns>Tarea asincrónica de actualización.</returns>
        public async Task<bool> ActualizarPelicula(Entidades.PeliculasFavoritas pelicula)
        {
            string errores = string.Empty;

            // Validación: entidad nula
            if (pelicula == null)
                errores += "La entidad de película no puede ser nula. | ";

            // Validación: ID obligatorio
            if (pelicula?.Id <= 0)
                errores += "El ID de la película es inválido. | ";

            // Validación: UsuarioId requerido
            if (pelicula?.UsuarioId <= 0)
                errores += "El ID del usuario debe ser mayor que cero. | ";

            // Validación: Título obligatorio
            if (string.IsNullOrWhiteSpace(pelicula?.Titulo))
                errores += "El título de la película es obligatorio. | ";

            // Validación: IMDb ID obligatorio
            if (string.IsNullOrWhiteSpace(pelicula?.ImdbID))
                errores += "El ID de IMDb es obligatorio. | ";

            // Validación: Verificar existencia del usuario
            if (pelicula != null && pelicula.UsuarioId > 0)
            {
                var usuarioExiste = await _iDLUnidadDeTrabajo.DLUsuario.ConsultarPorId(pelicula.UsuarioId);
                if (usuarioExiste == null)
                    errores += $"No se encontró ningún usuario con el ID {pelicula.UsuarioId}. | ";
            }

            // Lanzar errores si existen
            if (!string.IsNullOrWhiteSpace(errores))
                throw new ArgumentException(errores.Trim().TrimEnd('|'));

            // Actualización
            return await _iDLUnidadDeTrabajo.DLPeliculasFavoritas.ActualizarPeliculaFavorita(pelicula.Id, pelicula);
        }

        /// <summary>
        /// Elimina una película favorita de la base de datos según su identificador.
        /// </summary>
        /// <param name="id">Identificador único de la película a eliminar.</param>
        /// <returns>Tarea asincrónica de eliminación.</returns>
        public async Task<bool> EliminarPelicula(int id)
        {
            return await _iDLUnidadDeTrabajo.DLPeliculasFavoritas.EliminarPeliculaFavorita(id);
        }
        #endregion
    }
}
