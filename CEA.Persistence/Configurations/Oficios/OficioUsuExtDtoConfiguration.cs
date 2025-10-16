using CEA.Application.DTOs.Oficios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Oficios
{
    internal class OficioUsuExtDtoConfiguration : IEntityTypeConfiguration<OficioUsuExtDto>
    {
        public void Configure(EntityTypeBuilder<OficioUsuExtDto> entity)
        {
            entity.ToView("VS_OFICIOS_USUEXT");
            entity.HasNoKey();
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdExterno).HasColumnName("ID_EXTERNO");
            entity.Property(e => e.Frecuencia).HasColumnName("FRECUENCIA");
            entity.Property(e => e.Empresa).HasMaxLength(100).IsUnicode(false).HasColumnName("EMPRESA");
            entity.Property(e => e.Siglas).HasMaxLength(25).IsUnicode(false).HasColumnName("SIGLAS");
            entity.Property(e => e.Nombre).HasMaxLength(100).IsUnicode(false).HasColumnName("NOMBRE");
            entity.Property(e => e.Cargo).HasMaxLength(100).IsUnicode(false).HasColumnName("CARGO");
            entity.Property(e => e.Activo).HasPrecision(1).HasColumnName("ACTIVO");


        }
    }
}
