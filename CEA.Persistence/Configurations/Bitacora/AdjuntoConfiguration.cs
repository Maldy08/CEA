using CEA.Domain.Entities.Bitacora;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Bitacora
{
    internal class AdjuntoConfiguration : IEntityTypeConfiguration<Adjunto>
    {
        public void Configure(EntityTypeBuilder<Adjunto> entity)
        {
            entity.HasKey(e => e.Id)
                .HasName("ADJUNTOS_PK");

            entity.ToTable("ADJUNTOS");

            entity.Property(e => e.Id)
                .HasColumnName("ID_ADJUNTO");

            entity.Property(e => e.IdAvance)
                .HasPrecision(10)
                .HasColumnName("ID_AVANCE");

            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");

            entity.Property(e => e.Url)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("URL");

            entity.Property(e => e.TipoMime)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TIPO_MIME");
        }
    }
}
