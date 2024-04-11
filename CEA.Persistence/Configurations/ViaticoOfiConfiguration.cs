

using CEA.Domain.Common.Interfaces;
using CEA.Domain.Entities.Viaticos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations
{
    internal class ViaticoOfiConfiguration : IEntityTypeConfiguration<ViaticoOfi>
    {

        public void Configure(EntityTypeBuilder<ViaticoOfi> builder)
        {
            builder.HasKey(x => x.IdOfi);
            builder.ToTable("VIATICOS_OFI");
            builder.Property(e => e.IdOfi).HasPrecision(1).HasColumnName("IDOFI");
            builder.Property(e => e.Nombre).HasMaxLength(50).HasColumnName("NOMBRE");
            builder.Property(e => e.RutaTrans).HasMaxLength(100).HasColumnName("RUTATRANS");

        }
    
    
    }
}
