using CEA.Domain.Entities.Oficios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Oficios
{
    internal class OficioClasificacionConfiguration : IEntityTypeConfiguration<OficioClasificacion>
    {
        public void Configure(EntityTypeBuilder<OficioClasificacion> entity)
        {
            entity.HasNoKey();

            entity.ToTable("OFICIOS_CLASIFICACION");

            entity.Property(e => e.Id)
                .HasColumnName("ID");

            entity.Property(e => e.Codigo)
                .HasColumnName("CODIGO");

            entity.Property(e => e.Nombre)
                .HasColumnName("NOMBRE");

            entity.Property(e => e.Descripcion)
                .HasColumnName("DESCRIPCION");


        }
    }
}
