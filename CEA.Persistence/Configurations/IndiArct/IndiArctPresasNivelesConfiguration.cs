using CEA.Domain.Entities.IndiArct;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.IndiArct
{
    internal class IndiArctPresasNivelesConfiguration : IEntityTypeConfiguration<IndiArctPresasNiveles>
    {
        public void Configure(EntityTypeBuilder<IndiArctPresasNiveles> entity)
        {
            entity.HasKey(e => e.Id)
                .HasName("INDI_ARCT_PRESASNIVELES_PK");

            entity.ToTable("INDI_ARCT_PRESASNIVELES");

            // El ID lo genera la secuencia/trigger Oracle (ValueGeneratedOnAdd por convención).
            entity.Property(e => e.Id)
                .HasColumnName("ID_CAPTURA_VOLPRESAS");

            entity.Property(e => e.Anio)
                .HasPrecision(4)
                .HasColumnName("ANIO");

            entity.Property(e => e.Mes)
                .HasPrecision(2)
                .HasColumnName("MES");

            entity.Property(e => e.IdPresa)
                .HasPrecision(10)
                .HasColumnName("ID_PRESA");

            entity.Property(e => e.VolumenM3)
                .HasColumnType("NUMBER(15,2)")
                .HasColumnName("VOLUMEN_M3");

            entity.HasOne<IndiArctCatpresas>()
                .WithMany()
                .HasForeignKey(e => e.IdPresa)
                .HasConstraintName("FK_PRESASNIVELES_PRESA");
        }
    }
}
