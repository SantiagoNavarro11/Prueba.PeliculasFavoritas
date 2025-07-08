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

    public class DLPeliculasFavoritas : CrudSqlRepositorio<PeliculasFavoritas>, IDLPeliculasFavoritas
    {
        #region Variables
        /// <summary>
        /// Contexto de la base de datos utilizado para acceder a los datos de la tabla Persona.
        /// </summary>
        private readonly PeliculasFavoritasContext contextDB;
        #endregion

        #region Constructor
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DLPersona"/>.
        /// </summary>
        /// <param name="context">Contexto de base de datos para realizar las consultas.</param>
        public DLPeliculasFavoritas(PeliculasFavoritasContext context) : base(context)
        {
            contextDB = context;
        }
        #endregion

        #region Consulta Personalizada
        /// <summary>
        /// Consulta los registros de personas en la base de datos según los parámetros proporcionados.
        /// Permite realizar la búsqueda por el ID de la persona o por el código de Transmilenio.
        /// También utiliza la funcionalidad de "LIKE" en el campo PersonaCodigoTransmilenio.
        /// </summary>
        /// <param name="objBusqueda">Parámetros de búsqueda para filtrar los registros de personas.</param>
        /// <returns>Una lista de personas que coinciden con los criterios de búsqueda.</returns>
        public async Task<IEnumerable<PeliculasFavoritasDto>> ConsultarPeliculasFavoritas(ParametrosPeliculasFavoritas objBusqueda)
        {
            var registro = await contextDB.PeliculasFavoritas
    .Include(p => p.Usuario) // Si tienes la relación Usuario → PeliculasFavoritas
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
        FechaAgregada = p.FechaAgregada,

      
    })
    .AsNoTracking()
    .ToListAsync();
            return registro;
        }
        #endregion
    }
}
