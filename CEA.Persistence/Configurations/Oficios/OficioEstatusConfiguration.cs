

using CEA.Domain.Entities.Oficios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Oficios
{
    internal class OficioEstatusConfiguration : IEntityTypeConfiguration<OficioEstatus>
    {
        public void Configure(EntityTypeBuilder<OficioEstatus> entity)
        {
            entity.HasKey(e => new { e.IdEstatus, e.Eor }).HasName("OFICIOS_ESTATUS_PK");
            entity.ToTable("OFICIOS_ESTATUS");
            entity.Property(e => e.IdEstatus)
                .HasPrecision(2)
                .HasColumnName("ID_ESTATUS");
            entity.Property(e => e.IdEstatus)
                .HasPrecision(1);

            entity.Property(e => e.Eor)
                .HasPrecision(2)
                .HasColumnName("EOR");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
        }
    }
}
