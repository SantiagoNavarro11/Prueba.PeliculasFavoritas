namespace InfraestructuraPeliculasFavoritas.Mapeos
{
    using Core.PeliculasFavoritas.Entidades;
    using InfraestructuraPeliculasFavoritas.Datos.Mapeos;
    using Microsoft.EntityFrameworkCore;


    /// <summary>Contexto para el manejo de peticiones de las tablas para la base de datos.</summary>
    public class PeliculasFavoritasContext : DbContext
    {
        #region Constructor

        /// <summary>Inicializa una nueva instancia de la clase Context.</summary>
        public PeliculasFavoritasContext()
        {
        }

        /// <summary>Inicializa una nueva instancia de la clase Context.</summary>
        public PeliculasFavoritasContext(DbContextOptions<PeliculasFavoritasContext> options)
            : base(options)
        {
        }


        /// <summary>Configuración del Contexto.</summary>
        /// <param name="optionsBuilder">Parámetros de configuración.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        #endregion

        #region Métodos

        /// <summary>Configura el modelo mediante el ModelBuilder.</summary>
        /// <param name="modelBuilder">Objeto ModelBuilder usado para configurar el modelo.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new PeliculasFavoritasMap());
        }

        #endregion

        #region Entidades
        /// <value>Declaración del DbSet Usuario.</value>
        public virtual DbSet<Usuario> Usuario { get; set; }

        /// <value>Declaración del DbSet PeliculasFavoritas.</value>
        public virtual DbSet<PeliculasFavoritas> PeliculasFavoritas { get; set; }

        
        #endregion

    }
}
