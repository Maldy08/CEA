using CEA.Domain.Entities.Oficios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CEA.Persistence.Configurations.Oficios
{
    internal class OficioBitacoraConfiguration : IEntityTypeConfiguration<OficioBitacora>
    {
        public void Configure(EntityTypeBuilder<OficioBitacora> entity)
        {
            entity.HasKey(e => e.Id)
                .HasName("OFICIOS_BITACORA_PK");

            entity.ToTable("OFICIOS_BITACORA");


            entity.Property(e => e.Id)
                .HasColumnName("ID");
       

            entity.Property(e => e.Ejercicio)
                .HasPrecision(4)
                .HasColumnName("EJERCICIO");

            entity.Property(e => e.Eor)
                .HasPrecision(1)
                .HasColumnName("EOR");

            entity.Property(e => e.Folio)
                .HasPrecision(6)
                .HasColumnName("FOLIO");

            entity.Property(e => e.FechaCaptura)
                .HasColumnType("DATE")
                .HasColumnName("FECHA_CAPTURA");

            entity.Property(e => e.Comentarios)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("COMENTARIOS");

            entity.Property(e => e.Estatus)
                .HasPrecision(2)
                .HasColumnName("ESTATUS");

            entity.Property(e => e.IdEmpleado)
                .HasPrecision(4)
                .HasColumnName("ID_EMPLEADO");

        }
    }
}
