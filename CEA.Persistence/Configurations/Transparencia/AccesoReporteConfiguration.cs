

using CEA.Domain.Entities.Transparencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Transparencia
{
    internal class AccesoReporteConfiguration : IEntityTypeConfiguration<AccesoReporte>
    {
        public void Configure(EntityTypeBuilder<AccesoReporte> builder)
        {
            builder.ToTable("AccesoReportes");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.IdAnexo)
             .HasMaxLength(3)
             .IsUnicode(false);
                    builder.Property(e => e.IdAnexoInciso)
                        .HasMaxLength(2)
                    .IsUnicode(false);

            builder.HasOne(d => d.IdDeptoNavigation).WithMany(p => p.AccesoReportes)
                .HasForeignKey(d => d.IdDepto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccesoReportes_Departamentos");

            builder.HasOne(d => d.Reporte).WithMany(p => p.AccesoReportes)
                .HasForeignKey(d => new { d.IdArticulo, d.IdAnexo, d.IdAnexoInciso })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccesoReportes_Reportes");
        }
    }
}
