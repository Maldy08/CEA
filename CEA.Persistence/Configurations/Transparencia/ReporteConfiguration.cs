
using CEA.Domain.Entities.Transparencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Transparencia
{
    internal class ReporteConfiguration : IEntityTypeConfiguration<Reporte>
    {
        public void Configure(EntityTypeBuilder<Reporte> builder)
        {
            builder.HasKey(e => new { e.IdArticulo, e.IdAnexo, e.IdAnexoInciso });
            builder.ToTable("Reportes");

            builder.Property(e => e.IdAnexo)
                .HasMaxLength(3)
                .IsUnicode(false);
            builder.Property(e => e.IdAnexoInciso)
                .HasMaxLength(2)
                .IsUnicode(false);
            builder.Property(e => e.Codigo)
                .HasMaxLength(50)
                .IsUnicode(false);
            builder.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false);
            builder.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
