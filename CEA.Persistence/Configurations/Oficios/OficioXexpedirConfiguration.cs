using CEA.Domain.Entities.Oficios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CEA.Persistence.Configurations.Oficios
{
    internal class OficioXexpedirConfiguration : IEntityTypeConfiguration<OficioXexpedir>
    {
        public void Configure(EntityTypeBuilder<OficioXexpedir> entity)
        {
            entity.HasKey(e => new { e.Ejercicio, e.Depto, e.NoOficio })
                .HasName("OFICIOS_XEXPEDIR_PK");

            entity.ToTable("OFICIOS_XEXPEDIR");

            entity.Property(e => e.Ejercicio)
                .HasPrecision(4)
                .HasColumnName("EJERCICIO");

            entity.Property(e => e.Depto)
                .HasPrecision(2)
                .HasColumnName("DEPTO");

            entity.Property(e => e.NoOficio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NO_OFICIO");

            entity.Property(e => e.Estatus)
                .HasPrecision(2)
                .HasColumnName("ESTATUS");

            entity.Property(e => e.ExtCargo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EXT_CARGO");

            entity.Property(e => e.ExtDepen)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EXT_DEPEN");

            entity.Property(e => e.ExtNombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EXT_NOMBRE");

            entity.Property(e => e.ExtSiglas)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("EXT_SIGLAS");

            entity.Property(e => e.Fecha)
                .HasColumnType("DATE")
                .HasColumnName("FECHA");

            entity.Property(e => e.FechaCaptura)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("FECHA_CAPTURA");

            entity.Property(e => e.IdEmpleado)
                .HasPrecision(4)
                .HasColumnName("ID_EMPLEADO");

            entity.Property(e => e.IntCargo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("INT_CARGO");

            entity.Property(e => e.IntDepen)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("INT_DEPEN");

            entity.Property(e => e.IntNombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("INT_NOMBRE");

            entity.Property(e => e.IntSiglas)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("INT_SIGLAS");

            entity.Property(e => e.Tema)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TEMA");
        }
    }
}
