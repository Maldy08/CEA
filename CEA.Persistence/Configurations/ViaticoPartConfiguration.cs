
using CEA.Domain.Common.Interfaces;
using CEA.Domain.Entities.Viaticos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations
{
    internal class ViaticoPartConfiguration : IEntityTypeConfiguration<ViaticoPart>
    {
        public void Configure(EntityTypeBuilder<ViaticoPart> builder)
        {
            builder.HasKey(e => new { e.Oficina, e.Ejercicio, e.NoViat, e.Partida });
            builder.ToTable("VIATICOS_PART");

            builder.Property(e => e.Oficina).HasPrecision(1).HasColumnName("OFICINA");
            builder.Property(e => e.Ejercicio).HasPrecision(4).HasColumnName("EJERCICIO");
            builder.Property(e => e.NoViat).HasPrecision(5).HasColumnName("NOVIAT");
            builder.Property(e => e.Partida).HasPrecision(6).HasColumnName("PARTIDA");
            builder.Property(e => e.Importe).HasColumnName("IMPORTE");
        }
    }
}
