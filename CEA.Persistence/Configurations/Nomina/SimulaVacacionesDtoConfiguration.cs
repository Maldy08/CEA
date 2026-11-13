using CEA.Application.DTOs.Nomina;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Nomina
{
    internal class SimulaVacacionesDtoConfiguration : IEntityTypeConfiguration<SimulaVacacionesDto>
    {
        public void Configure(EntityTypeBuilder<SimulaVacacionesDto> entity)
        {
            entity.HasNoKey();
            entity.ToFunction("F_SIMULA_VACACIONES");
            entity.Property(e => e.NoEmp).HasColumnName("NOEMP");
            entity.Property(e => e.Ano).HasColumnName("ANO");
            entity.Property(e => e.Dias).HasColumnName("DIAS");
            entity.Property(e => e.Periodo).HasColumnName("PERIODO");
            entity.Property(e => e.DiasXDisfrutar).HasColumnName("DIASXDISFRUTAR");
        }
    }
}
