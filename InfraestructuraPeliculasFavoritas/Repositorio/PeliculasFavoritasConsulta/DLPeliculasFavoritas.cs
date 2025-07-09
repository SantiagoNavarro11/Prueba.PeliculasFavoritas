namespace InfraestructuraPeliculasFavoritas.Repositorio.PeliculasFavoritasConsulta
{
    using Core.PeliculasFavoritas.Dto;
    using Core.PeliculasFavoritas.Entidades;
    using Core.PeliculasFavoritas.EntidadesPersonalizadas;
    using Core.PeliculasFavoritas.Interface.PeliculasFavoritasConsulta;
    using InfraestructuraPeliculasFavoritas.Mapeos;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Utilitarios.ConfiguracionRepositorio;

    /// <summary>
    /// Repositorio de acceso a datos para las operaciones relacionadas con las películas favoritas.
    /// </summary>
    public class DLPeliculasFavoritas : CrudSqlRepositorio<PeliculasFavoritas>, IDLPeliculasFavoritas
    {
        #region Variables

        /// <summary>
        /// Contexto de la base de datos utilizado para acceder a los datos de películas favoritas.
        /// </summary>
        private readonly PeliculasFavoritasContext contextDB;

        #endregion

        #region Constructor

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DLPeliculasFavoritas"/>.
        /// </summary>
        /// <param name="context">Contexto de base de datos para realizar operaciones CRUD.</param>
        public DLPeliculasFavoritas(PeliculasFavoritasContext context) : base(context)
        {
            contextDB = context;
        }

        #endregion

        #region Consulta Personalizada

        /// <summary>
        /// Consulta las películas favoritas según los parámetros de búsqueda especificados.
        /// </summary>
        /// <param name="objBusqueda">Parámetros personalizados de búsqueda como género, año y director.</param>
        /// <returns>Una lista de objetos <see cref="PeliculasFavoritasDto"/> que cumplen los criterios.</returns>
        public async Task<IEnumerable<PeliculasFavoritasDto>> ConsultarPeliculasFavoritas(ParametrosPeliculasFavoritas objBusqueda)
        {
            var registro = await contextDB.PeliculasFavoritas
                .Include(p => p.Usuario)
                .Where(p =>
                    (string.IsNullOrEmpty(objBusqueda.Genero) || p.Genero.Contains(objBusqueda.Genero)) &&
                    (objBusqueda.Anio == null || p.Anio == objBusqueda.Anio) &&
                    (string.IsNullOrEmpty(objBusqueda.Director) || p.Director.Contains(objBusqueda.Director))
                )
                .Select(p => new PeliculasFavoritasDto
                {
                    Id = p.Id,
                    UsuarioId = p.UsuarioId,
                    ImdbID = p.ImdbID,
                    Titulo = p.Titulo,
                    Anio = p.Anio,
                    Director = p.Director,
                    Genero = p.Genero,
                    PosterUrl = p.PosterUrl,
                    Sinopsis = p.Sinopsis,
                    CalificacionIMDB = p.CalificacionIMDB,
                    Duracion = p.Duracion,
                    Idioma = p.Idioma,
                    Pais = p.Pais,
                    Actores = p.Actores,
                    Premios = p.Premios,
                    FechaAgregada = p.FechaAgregada
                })
                .AsNoTracking()
                .ToListAsync();

            return registro;
        }

        /// <summary>
        /// Inserta una nueva película favorita en la base de datos.
        /// </summary>
        /// <param name="pelicula">Entidad <see cref="PeliculasFavoritas"/> con los datos a insertar.</param>
        /// <returns>El identificador de la película insertada.</returns>
        public async Task<int> InsertarPeliculaFavorita(PeliculasFavoritas pelicula)
        {
            await contextDB.PeliculasFavoritas.AddAsync(pelicula);
            await contextDB.SaveChangesAsync();
            return pelicula.Id;
        }

        /// <summary>
        /// Actualiza los datos de una película favorita existente en la base de datos.
        /// </summary>
        /// <param name="id">Identificador de la película a actualizar.</param>
        /// <param name="objPeliculas">Objeto con los nuevos datos de la película.</param>
        /// <returns>True si se actualizó correctamente; de lo contrario, false.</returns>
        public async Task<bool> ActualizarPeliculaFavorita(int id, PeliculasFavoritas objPeliculas)
        {
            var existente = await contextDB.PeliculasFavoritas.FindAsync(id);
            if (existente == null)
                return false;

            existente.ImdbID = objPeliculas.ImdbID;
            existente.Titulo = objPeliculas.Titulo;
            existente.Anio = objPeliculas.Anio;
            existente.Director = objPeliculas.Director;
            existente.Genero = objPeliculas.Genero;
            existente.PosterUrl = objPeliculas.PosterUrl;
            existente.Sinopsis = objPeliculas.Sinopsis;
            existente.CalificacionIMDB = objPeliculas.CalificacionIMDB;
            existente.Duracion = objPeliculas.Duracion;
            existente.Idioma = objPeliculas.Idioma;
            existente.Pais = objPeliculas.Pais;
            existente.Actores = objPeliculas.Actores;
            existente.Premios = objPeliculas.Premios;
            existente.FechaAgregada = objPeliculas.FechaAgregada;
            existente.UsuarioId = objPeliculas.UsuarioId;

            contextDB.PeliculasFavoritas.Update(existente);
            await contextDB.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Consulta una película favorita por su identificador.
        /// (Actualmente no implementado).
        /// </summary>
        /// <param name="id">Identificador de la película.</param>
        /// <returns>Un objeto <see cref="PeliculasFavoritasDto"/> si se implementa.</returns>
        public Task<PeliculasFavoritasDto> ConsultarPeliculaPorId(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Elimina una película favorita de la base de datos por su ID.
        /// </summary>
        /// <param name="id">Identificador de la película a eliminar.</param>
        /// <returns>True si fue eliminada correctamente; false si no se encontró.</returns>
        public async Task<bool> EliminarPeliculaFavorita(int id)
        {
            var pelicula = await contextDB.PeliculasFavoritas.FindAsync(id);
            if (pelicula == null)
                return false;

            contextDB.PeliculasFavoritas.Remove(pelicula);
            await contextDB.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
