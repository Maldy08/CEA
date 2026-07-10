using CEA.Domain.Entities.IndiArct;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.IndiArct
{
    internal class IndiArctCatpresasConfiguration : IEntityTypeConfiguration<IndiArctCatpresas>
    {
        public void Configure(EntityTypeBuilder<IndiArctCatpresas> entity)
        {
            entity.HasKey(e => e.Id)
                .HasName("INDI_ARCT_CATPRESAS_PK");

            entity.ToTable("INDI_ARCT_CATPRESAS");

            // Catálogo: el ID se asigna manualmente (no hay secuencia/trigger).
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID_PRESA");

            entity.Property(e => e.NombreOficial)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_OFICIAL");

            entity.Property(e => e.Municipio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MUNICIPIO");

            entity.Property(e => e.Latitud)
                .HasColumnType("NUMBER(9,6)")
                .HasColumnName("LATITUD");

            entity.Property(e => e.Longitud)
                .HasColumnType("NUMBER(9,6)")
                .HasColumnName("LONGITUD");

            entity.Property(e => e.CorrientePrincipal)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CORRIENTE_PRINCIPAL");

            entity.Property(e => e.CapacidadNamoHm3)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("CAPACIDAD_NAMO_HM3");

            entity.Property(e => e.UsoPrincipal)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USO_PRINCIPAL");
        }
    }
}
