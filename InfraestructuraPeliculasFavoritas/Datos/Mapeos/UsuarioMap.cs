namespace InfraestructuraPeliculasFavoritas.Datos.Mapeos
{
    using Core.PeliculasFavoritas.Entidades;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Configura la entidad <see cref="Usuario"/> y su correspondencia con la tabla "Usuarios" en la base de datos.
    /// </summary>
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            #region Map
            // Nombre de la tabla
            builder.ToTable("Usuarios");

            // Clave primaria
            builder.HasKey(u => u.Id)
                .HasName("PK_Usuarios");

            // Propiedades
            builder.Property(u => u.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Correo)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Contrasena)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.FechaRegistro)
                .IsRequired()
                .HasColumnType("datetime");
            #endregion
        }
    }
}
