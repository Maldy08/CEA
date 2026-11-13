using CEA.Application.DTOs.Nomina;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Nomina
{
    internal class SaldoVacacionesDtoConfiguration : IEntityTypeConfiguration<SaldoVacacionesDto>
    {
        public void Configure(EntityTypeBuilder<SaldoVacacionesDto> entity)
        {
            entity.HasNoKey();
            entity.ToFunction("F_SALDO_VACACIONES");
            entity.Property(e => e.Empleado).HasColumnName("EMPLEADO");
            entity.Property(e => e.NombreCompleto).HasColumnName("NOMBRE_COMPLETO");
            entity.Property(e => e.FechaAlta).HasColumnType("DATE").HasColumnName("FECHAALTA");
            entity.Property(e => e.AntiguedadActualAnos).HasColumnName("ANTIGUEDAD_ACTUAL_ANOS");
            entity.Property(e => e.AnoDisponible).HasColumnName("A_ANO_DISPONIBLE");
            entity.Property(e => e.PeriodoDisponible).HasColumnName("A_PERIODO_DISPONIBLE");
            entity.Property(e => e.DiasPorDisfrutar).HasColumnName("A_DIAS_POR_DISFRUTAR");
            entity.Property(e => e.DiasTotalesPorDisfrutar).HasColumnName("B_DIAS_TOTALES_POR_DISFRUTAR");
        }
    }
}
