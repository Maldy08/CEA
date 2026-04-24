using CEA.Domain.Entities.Bitacora;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Bitacora
{
    internal class TemaInvolucradosConfiguration : IEntityTypeConfiguration<TemaInvolucrado>
    {
        public void Configure(EntityTypeBuilder<TemaInvolucrado> entity)
        {
            entity.HasKey(e => e.Id)
                .HasName("TEMA_INVOLUCRADOS_PK");

            entity.ToTable("TEMA_INVOLUCRADOS");

            entity.Property(e => e.Id)
                .HasColumnName("ID_ASIGNACION");

            entity.Property(e => e.IdTema)
                .HasPrecision(10)
                .HasColumnName("ID_TEMA");

            entity.Property(e => e.IdUsuario)
                .HasPrecision(10)
                .HasColumnName("ID_USUARIO");

            entity.Property(e => e.TipoInvolucrado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TIPO_INVOLUCRADO");

            entity.Property(e => e.FechaAsignacion)
                .HasColumnType("DATE")
                .HasColumnName("FECHA_ASIGNACION");
        }
    }
}
