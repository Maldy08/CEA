using CEA.Domain.Entities.Bitacora;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Bitacora
{
    internal class TemaConfiguration : IEntityTypeConfiguration<Tema>
    {
        public void Configure(EntityTypeBuilder<Tema> entity)
        {
            entity.HasKey(e => e.Id)
                .HasName("TEMAS_PK");

            entity.ToTable("TEMAS");

            entity.Property(e => e.Id)
                .HasColumnName("ID_TEMA");

            entity.Property(e => e.Titulo)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("TITULO");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");

            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ESTADO");

            entity.Property(e => e.FechaCreacion)
                .HasColumnType("DATE")
                .HasColumnName("FECHA_CREACION");

            entity.Property(e => e.FechaLimite)
                .HasColumnType("DATE")
                .HasColumnName("FECHA_LIMITE");

            entity.Property(e => e.IdDepartamentoOrigen)
                .HasPrecision(4)
                .HasColumnName("ID_DEPARTAMENTO_ORIGEN");
        }
    }
}
