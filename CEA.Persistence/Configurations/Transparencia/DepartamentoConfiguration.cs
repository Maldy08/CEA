
using CEA.Domain.Entities.Transparencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Transparencia
{
    internal class DepartamentoConfiguration : IEntityTypeConfiguration<Departamento>
    {
        public void Configure(EntityTypeBuilder<Departamento> builder)
        {
            builder.HasKey(e => e.IdDepto);
            builder.ToTable("Departamentos");

            builder.Property(e => e.IdDepto).ValueGeneratedNever();
            builder.Property(e => e.CodigoDepto)
                .HasMaxLength(2)
                .IsUnicode(false);
            builder.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
