using CEA.Application.DTOs.Nomina;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Nomina
{
    internal class FirmasTramiteDtoConfiguration : IEntityTypeConfiguration<FirmasTramiteDto>
    {
        public void Configure(EntityTypeBuilder<FirmasTramiteDto> entity)
        {
            entity.HasNoKey();
            entity.ToFunction("FN_TRAMITES_CONSULTA_FIRMAS");
            entity.Property(e => e.Firma1).HasColumnName("FIRMA_1");
            entity.Property(e => e.Firma2).HasColumnName("FIRMA_2");
        }
    }
}
