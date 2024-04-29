
using CEA.Domain.Entities.Transparencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Transparencia
{
    internal class UsuariosNiveleConfiguration : IEntityTypeConfiguration<UsuariosNivele>
    {
        public void Configure(EntityTypeBuilder<UsuariosNivele> builder)
        {
            builder.HasKey(e => e.IdNivel);
            builder.ToTable("UsuariosNiveles");

            builder.Property(e => e.IdNivel)
                .ValueGeneratedNever()
                .HasColumnName("IdNIvel");
            builder.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
