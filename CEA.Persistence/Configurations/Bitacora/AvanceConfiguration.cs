using CEA.Domain.Entities.Bitacora;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Bitacora
{
    internal class AvanceConfiguration : IEntityTypeConfiguration<Avance>
    {
        public void Configure(EntityTypeBuilder<Avance> entity)
        {
            entity.HasKey(e => e.Id)
                .HasName("AVANCES_PK");

            entity.ToTable("AVANCES");

            entity.Property(e => e.Id)
                .HasColumnName("ID_AVANCE");

            entity.Property(e => e.IdTema)
                .HasPrecision(10)
                .HasColumnName("ID_TEMA");

            entity.Property(e => e.IdUsuario)
                .HasPrecision(10)
                .HasColumnName("ID_USUARIO");

            entity.Property(e => e.Observaciones)
                .HasMaxLength(4000)
                .IsUnicode(false)
                .HasColumnName("OBSERVACIONES");

            entity.Property(e => e.FechaHora)
                .HasColumnType("DATE")
                .HasColumnName("FECHA_HORA");

            entity.Property(e => e.FechaEdicion)
                .HasColumnType("DATE")
                .HasColumnName("FECHA_EDICION")
                .IsRequired(false);
        }
    }
}
