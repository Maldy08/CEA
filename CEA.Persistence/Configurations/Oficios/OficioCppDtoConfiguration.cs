using CEA.Application.DTOs.Oficios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Oficios
{
    internal class OficioCppDtoConfiguration : IEntityTypeConfiguration<OficioCppDto>
    {
        public void Configure(EntityTypeBuilder<OficioCppDto> entity)
        {
            entity.HasNoKey();
            entity.ToFunction("F_OFICIO_CPP");
            entity.Property(e => e.Nivel)
                .HasColumnName("NIVEL");

            entity.Property(e => e.Puesto)
                .HasColumnName("PUESTO");
        }
    }
    }
