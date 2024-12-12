

using CEA.Domain.Entities.Vehiculos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Vehiculos
{
    internal class VhCatVehiculosConfiguration : IEntityTypeConfiguration<VhCatVehiculos>
    {
        public void Configure(EntityTypeBuilder<VhCatVehiculos> builder)
        {
            builder.ToTable("VH_CATVEHICULOS").HasKey(e => e.NoEcon);
            builder.Property(e => e.NoEcon).HasColumnName("NOECON");
            builder.Property(e => e.NoActivo).HasColumnName("NOACTIVO");
            builder.Property(e => e.Ano).HasColumnName("ANO");
            builder.Property(e => e.Placas).HasColumnName("PLACAS");
            builder.Property(e => e.Color).HasColumnName("COLOR");
            builder.Property(e => e.Odometro).HasColumnName("ODOMETRO");
            builder.Property(e => e.Estatus).HasColumnName("ESTATUS");
            builder.Property(e => e.Ubicacion).HasColumnName("UBICACION");
            builder.Property(e => e.FUltServ).HasColumnName("F_ULTSERV").HasColumnType("DATE");
            builder.Property(e => e.FProxServ).HasColumnName("F_PROXSERV").HasColumnType("DATE");
            builder.Property(e => e.Tipo).HasColumnName("TIPO");
            builder.Property(e => e.Capacidad).HasColumnName("CAPACIDAD");
            builder.Property(e => e.Pernoc).HasColumnName("PERNOC");
            builder.Property(e => e.Comentarios).HasColumnName("COMENTARIOS");


        }
    }
}
