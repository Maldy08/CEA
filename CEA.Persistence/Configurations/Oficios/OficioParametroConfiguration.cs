using CEA.Domain.Entities.Oficios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CEA.Persistence.Configurations.Oficios
{
    internal class OficioParametroConfiguration : IEntityTypeConfiguration<OficioParametro>
    {
        public void Configure(EntityTypeBuilder<OficioParametro> entity)
        {
            //Tabla solamente de Folios de la mesa de correspondencia, no numeros consecutivos de oficio por departamento
            entity.ToTable("OFICIOS_PARAMETROS");

            entity.HasKey(o => o.Id)
                .HasName("PK_OFICIOS_PARAMETROS");

            entity.Property(o => o.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Ejercicio)
                .HasPrecision(4)
                .HasColumnName("EJERCICIO");

            entity.Property(e => e.NextFEnv)
                .HasPrecision(6)
                .HasColumnName("NEXT_F_ENV");

            entity.Property(e => e.NextFRec)
                .HasPrecision(6)
                .HasColumnName("NEXT_F_REC");

            entity.Property(e => e.NextFXexp)
                .HasPrecision(6)
                .HasColumnName("NEXT_F_XEXP");
        }
    }
}
