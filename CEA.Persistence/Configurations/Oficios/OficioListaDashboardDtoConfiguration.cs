using CEA.Application.DTOs.Oficios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Oficios
{
    public class OficioListaDashboardDtoConfiguration : IEntityTypeConfiguration<OficioListaDashboardDto>
    {
        public void Configure(EntityTypeBuilder<OficioListaDashboardDto> entity)
        {
            entity.HasNoKey();
            entity.ToFunction("F_LISTA_DASHBOARD1");

            entity.Property(e => e.Letra).HasColumnName("LETRA");
            entity.Property(e => e.Eor).HasColumnName("EOR");
            entity.Property(e => e.Folio).HasColumnName("FOLIO");
            entity.Property(e => e.Fecha).HasColumnName("FECHA");
            entity.Property(e => e.Tipo).HasColumnName("TIPO");
            entity.Property(e => e.NomTipo).HasColumnName("NOM_TIPO");
            entity.Property(e => e.NoOficio).HasColumnName("NO_OFICIO");
            entity.Property(e => e.Rn).HasColumnName("RN");

        }
    }
}
