using CEA.Domain.Entities.Oficios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Oficios
{
    internal class OficioGpiConfiguration : IEntityTypeConfiguration<OficioGpi>
    {
        public void Configure(EntityTypeBuilder<OficioGpi> entity)
        {
            entity.HasKey(e => e.IdGpi);
            entity.ToTable("OFICIOS_GPI");
            entity.Property(e => e.IdGpi).HasColumnName("ID_GPI");
            entity.Property(e => e.Nombre).HasMaxLength(100).IsUnicode(false).HasColumnName("NOMBRE");
            entity.Property(e => e.PrimerRen).HasMaxLength(100).IsUnicode(false).HasColumnName("PRIMER_REN");
            entity.Property(e => e.SegundoRen).HasMaxLength(100).IsUnicode(false).HasColumnName("SEGUNDO_REN");
            entity.Property(e => e.Correo).HasMaxLength(100).IsUnicode(false).HasColumnName("CORREO");
            entity.Property(e => e.FechaCaptura).HasColumnType("datetime").HasColumnName("FECHA_CAPTURA");
            entity.Property(e => e.Activo).HasColumnName("ACTIVO");

        }
    }
}
