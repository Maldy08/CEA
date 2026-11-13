using CEA.Application.DTOs.Nomina;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Nomina
{
    internal class ConsultaVacacionesRenglonDtoConfiguration : IEntityTypeConfiguration<ConsultaVacacionesRenglonDto>
    {
        public void Configure(EntityTypeBuilder<ConsultaVacacionesRenglonDto> entity)
        {
            entity.HasNoKey();
            entity.ToFunction("FN_CONSULTA_VACACIONES");
            entity.Property(e => e.Orden).HasColumnName("ORDEN");
            entity.Property(e => e.Valor).HasColumnName("VALOR");
        }
    }
}
