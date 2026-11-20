using CEA.Domain.Entities.Nomina;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Nomina
{
    internal class NomVacaempMConfiguration : IEntityTypeConfiguration<NomVacaempM>
    {
        public void Configure(EntityTypeBuilder<NomVacaempM> entity)
        {
            // El ID lo genera la secuencia/trigger Oracle (ValueGeneratedOnAdd por convención).
            entity.HasKey(e => e.IdVacaemp).HasName("PK_NOM_VACAEMP_M");
            entity.ToTable("NOM_VACAEMP_M");
            entity.Property(e => e.IdVacaemp).HasPrecision(12).HasColumnName("ID_VACAEMP");
            entity.Property(e => e.IdTramite).HasPrecision(12).HasColumnName("ID_TRAMITE");
            entity.Property(e => e.Ano).HasPrecision(4).HasColumnName("ANO");
            entity.Property(e => e.Folio).HasMaxLength(25).IsUnicode(false).HasColumnName("FOLIO");
            entity.Property(e => e.NoEmp).HasPrecision(4).HasColumnName("NOEMP");
            entity.Property(e => e.DiasSolicitados).HasPrecision(3).HasColumnName("DIAS_SOLICITADOS");
            entity.Property(e => e.FDesde).HasColumnType("DATE").HasColumnName("FDESDE");
            entity.Property(e => e.FHasta).HasColumnType("DATE").HasColumnName("FHASTA");
            entity.Property(e => e.FechaRegreso).HasColumnType("DATE").HasColumnName("FECHA_REGRESO");
            entity.Property(e => e.IdEmpleadoCubre).HasPrecision(4).HasColumnName("ID_EMPLEADO_CUBRE");
            entity.Property(e => e.OrigenRegistro).HasMaxLength(1).IsUnicode(false).IsFixedLength().HasColumnName("ORIGEN_REGISTRO");
            entity.Property(e => e.IdEmpleadoCaptura).HasPrecision(4).HasColumnName("ID_EMPLEADO_CAPTURA");
            entity.Property(e => e.FechaCaptura).HasColumnType("DATE").HasColumnName("FECHA_CAPTURA");
        }
    }
}
