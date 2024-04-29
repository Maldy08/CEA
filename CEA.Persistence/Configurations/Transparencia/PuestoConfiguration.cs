using CEA.Domain.Entities.Transparencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CEA.Persistence.Configurations.Transparencia
{
    internal class PuestoConfiguration : IEntityTypeConfiguration<Puesto>
    {
        public void Configure(EntityTypeBuilder<Puesto> builder)
        {
            builder.ToTable("Puestos");
            builder.HasKey(e => e.IdPuesto);
            builder.Property(e => e.IdPuesto).ValueGeneratedNever();
            builder.Property(e => e.Descripcion).HasMaxLength(50).IsUnicode(false);
        }
    }
}
