using CEA.Domain.Entities.IndiArct;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.IndiArct
{
    internal class IndiArctEnergiaConfiguration : IEntityTypeConfiguration<IndiArctEnergia>
    {
        public void Configure(EntityTypeBuilder<IndiArctEnergia> entity)
        {
            entity.HasKey(e => e.Id)
                .HasName("INDI_ARCT_ENERGIA_PK");

            entity.ToTable("INDI_ARCT_ENERGIA");

            // El ID lo genera la secuencia/trigger Oracle (ValueGeneratedOnAdd por convención).
            entity.Property(e => e.Id)
                .HasColumnName("ID_ARCT_ENERGIA");

            entity.Property(e => e.Anio)
                .HasPrecision(4)
                .HasColumnName("ANIO");

            entity.Property(e => e.Mes)
                .HasPrecision(2)
                .HasColumnName("MES");

            entity.Property(e => e.Volumenes)
                .HasColumnType("NUMBER(15,2)")
                .HasColumnName("VOLUMENES");

            entity.Property(e => e.Kwh)
                .HasColumnType("NUMBER(15,2)")
                .HasColumnName("KWH");

            entity.Property(e => e.Costo)
                .HasColumnType("NUMBER(15,2)")
                .HasColumnName("COSTO");
        }
    }
}
