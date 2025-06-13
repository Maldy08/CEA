

using CEA.Application.DTOs.Oficios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Oficios
{
    internal class OficioContadoresDashboardDtoConfiguration : IEntityTypeConfiguration<OficioContadoresDashboardDto>
    {
        public void Configure(EntityTypeBuilder<OficioContadoresDashboardDto> entity)
        {
            entity.HasNoKey();
            entity.ToFunction("F_CONTADORES_DASHBOARD1");
            entity.Property(e => e.TotExp).HasColumnName("TOT_EXP");
            entity.Property(e => e.TotRec).HasColumnName("TOT_REC");
            entity.Property(e => e.TotXExp).HasColumnName("TOT_XEXP");
            entity.Property(e => e.TotXExpTrans).HasColumnName("TOT_XEXPTRANS");
            entity.Property(e => e.TotRecXVenc).HasColumnName("TOT_RECXVEN");
            entity.Property(e => e.TotExpXVenc).HasColumnName("TOT_EXPXVEN");
            entity.Property(e => e.ProxFRec).HasColumnName("PROX_FREC");
            entity.Property(e => e.ProxFEnv).HasColumnName("PROX_FENV");
        }
    }
}
