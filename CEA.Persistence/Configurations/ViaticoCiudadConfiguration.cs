

using CEA.Domain.Common.Interfaces;
using CEA.Domain.Entities.Viaticos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations
{
    internal class ViaticoCiudadConfiguration : IEntityTypeConfiguration<ViaticoCiudad>
    {
        public void Configure(EntityTypeBuilder<ViaticoCiudad> builder)
        {
            builder.HasKey(e => e.IdCiudad);
            builder.ToTable("VIATICOS_CIUDAD");

            builder.Property(e => e.IdCiudad).HasPrecision(3).HasColumnName("IDCIUDAD");
            builder.Property(e => e.IdEstado).HasPrecision(3).HasColumnName("IDESTADO");
            builder.Property(e => e.Ciudad).HasMaxLength(50).HasColumnName("CIUDAD");
        }
    }
}
