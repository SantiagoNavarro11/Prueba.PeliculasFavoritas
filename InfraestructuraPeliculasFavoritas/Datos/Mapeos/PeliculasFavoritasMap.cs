namespace InfraestructuraPeliculasFavoritas.Datos.Mapeos
{
    using Core.PeliculasFavoritas.Entidades;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Configura la entidad <see cref="PeliculasFavoritas"/> con la tabla "PeliculasFavoritas" en la base de datos.
    /// </summary>
    public class PeliculasFavoritasMap : IEntityTypeConfiguration<PeliculasFavoritas>
    {
        public void Configure(EntityTypeBuilder<PeliculasFavoritas> builder)
        {
            #region Map
            builder.ToTable("PeliculasFavoritas");

            builder.HasKey(p => p.Id)
                   .HasName("PK_PeliculasFavoritas");

            builder.Property(p => p.Id).IsRequired();

            builder.Property(p => p.UsuarioId).IsRequired();

            builder.HasOne(p => p.Usuario)
                   .WithMany()
                   .HasForeignKey(p => p.UsuarioId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.ImdbID)
                   .HasMaxLength(20);

            builder.Property(p => p.Titulo)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(p => p.Anio)
                   .HasMaxLength(10);

            builder.Property(p => p.Director)
                   .HasMaxLength(100);

            builder.Property(p => p.Genero)
                   .HasMaxLength(100);

            builder.Property(p => p.PosterUrl)
                   .HasMaxLength(500);

            builder.Property(p => p.Sinopsis)
                   .HasMaxLength(1000);

            builder.Property(p => p.CalificacionIMDB)
                   .HasMaxLength(10);

            builder.Property(p => p.Duracion)
                   .HasMaxLength(20);

            builder.Property(p => p.Idioma)
                   .HasMaxLength(50);

            builder.Property(p => p.Pais)
                   .HasMaxLength(100);

            builder.Property(p => p.Actores)
                   .HasMaxLength(500);

            builder.Property(p => p.Premios)
                   .HasMaxLength(500);

            builder.Property(p => p.FechaAgregada)
                   .IsRequired()
                   .HasColumnType("datetime");

            builder.HasOne(p => p.Usuario)
                   .WithMany(u => u.PeliculasFavoritas) // 👈 Aquí le dices cuál es la propiedad de navegación en Usuario
                   .HasForeignKey(p => p.UsuarioId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
