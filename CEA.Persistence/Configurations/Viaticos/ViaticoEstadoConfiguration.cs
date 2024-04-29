using CEA.Domain.Entities.Viaticos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Viaticos
{
    internal class ViaticoEstadoConfiguration : IEntityTypeConfiguration<ViaticoEstado>
    {
        public void Configure(EntityTypeBuilder<ViaticoEstado> builder)
        {
            builder.HasKey(e => e.IdEstado);
            builder.ToTable("VIATICOS_ESTADO");

            builder.Property(e => e.Id).HasPrecision(3).HasColumnName("IDESTADO");
            builder.Property(e => e.IdEstado).HasPrecision(3).HasColumnName("IDESTADO");
            builder.Property(e => e.IdPais).HasPrecision(3).HasColumnName("IDPAIS");
            builder.Property(e => e.Estado).HasMaxLength(50).HasColumnName("ESTADO");
        }
    }
}
