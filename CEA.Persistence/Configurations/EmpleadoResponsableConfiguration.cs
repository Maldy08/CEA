using CEA.Domain.Entities.RecursosHumanos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations
{
    internal class EmpleadoResponsableConfiguration : IEntityTypeConfiguration<EmpleadoResponsable>
    {
        public void Configure(EntityTypeBuilder<EmpleadoResponsable> builder)
        {
            builder.HasNoKey();
            builder.ToView("VW_EMPLEADOS_RESPONSABLES");

            builder.Property(e => e.IdEmpleado).HasColumnName("EMPLEADO");
        }
    }
}
