
using CEA.Domain.Common.Interfaces;
using CEA.Domain.Entities.Viaticos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations
{
    internal class ViaticoPaisConfiguration : IEntityTypeConfiguration<ViaticoPais>
    {
        public void Configure(EntityTypeBuilder<ViaticoPais> builder)
        {
            builder.HasKey(e => e.IdPais);
            builder.ToTable("VIATICOS_PAIS");

            builder.Property(e => e.IdPais).HasPrecision(3).HasColumnName("IDPAIS");
            builder.Property(e => e.Pais).HasMaxLength(50).HasColumnName("PAIS");
        }
    }
}
