using CEA.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations
{
    internal class DeptoCeaSeproaDtoConfiguration : IEntityTypeConfiguration<DeptoCeaSeproaDto>
    {
        public void Configure(EntityTypeBuilder<DeptoCeaSeproaDto> entity)
        {
            entity.HasNoKey();
            entity.ToView("VS_DEPTOSCEAYSEPROA");
            entity.Property(e => e.IdCea)
                .HasPrecision(3)
                .HasColumnName("ID_CEA");
            entity.Property(e => e.Descripcion)
                .HasColumnName("DESCRIPCION");

        }
    }
}
