
using CEA.Domain.Entities.RecursosHumanos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations
{
    internal class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> builder)
        {
            builder.HasNoKey();
            builder.ToView("VS_EMPLEADOS");

            builder.Property(e => e.Activo).HasMaxLength(2).IsUnicode(false).HasColumnName("ACTIVO");
            builder.Property(e => e.Frecuencia).HasPrecision(2).HasColumnName("FRECUENCIA");
            builder.Property(e => e.IdEmpleado).HasPrecision(4).HasColumnName("EMPLEADO");
            builder.Property(e => e.Paterno).HasMaxLength(50).IsUnicode(false).HasColumnName("PATERNO");
            builder.Property(e => e.Materno).HasMaxLength(50).IsUnicode(false).HasColumnName("MATERNO");
            builder.Property(e => e.Nombre).HasMaxLength(50).IsUnicode(false).HasColumnName("NOMBRE");
            builder.Property(e => e.IdPue).HasPrecision(3).HasColumnName("ID_PUE");
            builder.Property(e => e.DescripcionPuesto).HasColumnName("DESCRIPCION_PUESTO");
            builder.Property(e => e.DeptoUe).HasPrecision(4).HasColumnName("DEPTOUE").HasDefaultValueSql("0");
            builder.Property(e => e.DescripcionDepto).HasColumnName("DESCRIPCION_DEPTO");
            builder.Property(e => e.DeptoComi).HasPrecision(4).HasColumnName("DEPTOCOMI").HasDefaultValueSql("0");
            builder.Property(e => e.NombreCompleto).HasColumnName("NOMBRE_COMPLETO");
            builder.Property(e => e.Municipio).HasColumnName("MUNICIPIO").HasPrecision(1);
            builder.Property(e => e.Oficina).HasColumnName("OFICINA").HasPrecision(1);
            builder.Property(e => e.Nivel).HasColumnName("NIVEL").HasPrecision(2);
            builder.Property(e => e.LugarTrab).HasColumnName("LUGARTRAB").HasPrecision(2);
            builder.Property(e => e.Correo).HasMaxLength(50).IsUnicode(false).HasColumnName("CORREO");
            builder.Property(e => e.DeptoPpto).HasPrecision(4).HasColumnName("DEPTOPPTO");
            builder.Property(e => e.Obra).HasPrecision(4).HasColumnName("OBRA");
        }
    }
}
