
using CEA.Domain.Entities.Transparencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Transparencia
{
    internal class BitacoraArchivoConfiguration : IEntityTypeConfiguration<BitacoraArchivo>
    {
        public void Configure(EntityTypeBuilder<BitacoraArchivo> builder)
        {
            
            builder.ToTable("BitacoraArchivos");
            builder.HasKey(e => e.IdBitacora);

            builder.Property(e => e.FechaModificado).HasColumnType("datetime");
            builder.Property(e => e.FechaSubido).HasColumnType("datetime");
            builder.Property(e => e.Hipervinculo).IsUnicode(false);
            builder.Property(e => e.NombreArchivo).IsUnicode(false);
            builder.Property(e => e.NombreReporte)
                .HasMaxLength(50)
                .IsUnicode(false);
            builder.Property(e => e.RutaArchivo).IsUnicode(false);
        }
    }
}
