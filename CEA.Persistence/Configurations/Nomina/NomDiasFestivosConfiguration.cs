using CEA.Domain.Entities.Nomina;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Nomina
{
    internal class NomDiasFestivosConfiguration : IEntityTypeConfiguration<NomDiasFestivos>
    {
        public void Configure(EntityTypeBuilder<NomDiasFestivos> entity)
        {
            // El ID lo genera la secuencia/trigger Oracle (ValueGeneratedOnAdd por convención).
            entity.HasKey(e => e.IdFestivo).HasName("PK_NOM_DIAS_FESTIVOS");
            entity.ToTable("NOM_DIAS_FESTIVOS");
            entity.Property(e => e.IdFestivo).HasColumnName("ID_FESTIVO");
            entity.Property(e => e.FechaFestivo).HasColumnType("DATE").HasColumnName("FECHA_FESTIVO");
            entity.Property(e => e.Descripcion).HasMaxLength(100).IsUnicode(false).HasColumnName("DESCRIPCION");
            entity.Property(e => e.FechaOficial).HasColumnType("DATE").HasColumnName("FECHA_OFICIAL");
            entity.Property(e => e.HoraEntrada).HasMaxLength(5).IsUnicode(false).HasColumnName("HORA_ENTRADA");
            entity.Property(e => e.HoraSalida).HasMaxLength(5).IsUnicode(false).HasColumnName("HORA_SALIDA");
            entity.Property(e => e.AplicaBase).HasPrecision(1).HasColumnName("APLICA_BASE");
            entity.Property(e => e.BaseSexo).HasMaxLength(1).IsUnicode(false).HasColumnName("BASE_SEXO");
            entity.Property(e => e.BaseReqHijos).HasPrecision(1).HasColumnName("BASE_REQ_HIJOS");
            entity.Property(e => e.AplicaConfianza).HasPrecision(1).HasColumnName("APLICA_CONFIANZA");
            entity.Property(e => e.ConfSexo).HasMaxLength(1).IsUnicode(false).HasColumnName("CONF_SEXO");
            entity.Property(e => e.ConfReqHijos).HasPrecision(1).HasColumnName("CONF_REQ_HIJOS");
            entity.Property(e => e.AplicaContrato).HasPrecision(1).HasColumnName("APLICA_CONTRATO");
            entity.Property(e => e.ContSexo).HasMaxLength(1).IsUnicode(false).HasColumnName("CONT_SEXO");
            entity.Property(e => e.ContReqHijos).HasPrecision(1).HasColumnName("CONT_REQ_HIJOS");
            entity.Property(e => e.Activo).HasPrecision(1).HasColumnName("ACTIVO");
        }
    }
}
