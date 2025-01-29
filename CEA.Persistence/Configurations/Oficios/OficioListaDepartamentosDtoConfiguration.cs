using CEA.Application.DTOs.Oficios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Oficios
{
    internal class OficioListaDepartamentosDtoConfiguration : IEntityTypeConfiguration<OficioListaDepartamentosDto>
    {
        public void Configure(EntityTypeBuilder<OficioListaDepartamentosDto> entity)
        {
            entity.HasNoKey();
            entity.ToFunction("LISTA_DEPARTAMENTOS");

            entity.Property(e => e.IdCea).HasColumnName("ID_CEA");
            entity.Property(e => e.Descripcion).HasColumnName("DESCRIPCION");
            entity.Property(e => e.Siglas).HasColumnName("SIGLAS");
            entity.Property(e => e.Responsable).HasColumnName("RESPONSABLE");
            entity.Property(e => e.Puesto).HasColumnName("PUESTO");
        }
    }
}
