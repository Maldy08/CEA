using CEA.Domain.Entities.Oficios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CEA.Persistence.Configurations.Oficios
{
    internal class OficioUsuExtConfiguration : IEntityTypeConfiguration<OficioUsuExt>
    {
        public void Configure(EntityTypeBuilder<OficioUsuExt> entity)
        {
            entity.HasKey(e => e.Id)
                .HasName("OFICIOS_USUEXT_PK");

            entity.ToTable("OFICIOS_USUEXT");

            entity.Property(e => e.Id)
                .HasPrecision(4)
                .HasColumnName("ID");

            entity.Property(e => e.IdExterno)
                .HasPrecision(4)
                .HasColumnName("ID_EXTERNO");

            entity.Property(e => e.Activo)
                .HasPrecision(1)
                .HasColumnName("ACTIVO");

            entity.Property(e => e.Cargo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CARGO");

            entity.Property(e => e.Empresa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMPRESA");

            entity.Property(e => e.FechaCaptura)
                .HasColumnType("DATE")
                .HasColumnName("FECHA_CAPTURA");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");

            entity.Property(e => e.Siglas)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("SIGLAS");
        }
    }
}
