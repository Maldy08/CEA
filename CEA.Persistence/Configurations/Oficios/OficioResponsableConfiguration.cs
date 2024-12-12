using CEA.Domain.Entities.Oficios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEA.Persistence.Configurations.Oficios
{
    internal class OficioResponsableConfiguration : IEntityTypeConfiguration<OficioResponsable>
    {
        public void Configure(EntityTypeBuilder<OficioResponsable> entity)
        {
            entity.HasKey(e => new { e.Ejercicio, e.Folio, e.Eor })
              .HasName("OFICIOS_RESPONSABLE_PK");

            entity.ToTable("OFICIOS_RESPONSABLE");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                
                .HasPrecision(6);

            entity.Property(e => e.Ejercicio)
                .HasPrecision(4)
                .HasColumnName("EJERCICIO");

            entity.Property(e => e.Folio)
                .HasPrecision(6)
                .HasColumnName("FOLIO");

            entity.Property(e => e.Eor)
                .HasPrecision(1)
                .HasColumnName("EOR");

            entity.Property(e => e.IdEmpleado)
                .HasPrecision(4)
                .HasColumnName("ID_EMPLEADO");

            entity.Property(e => e.Rol)
                .HasPrecision(1)
                .HasColumnName("ROL");

            entity.Property(e => e.Iox)
                .HasPrecision(1)
                .HasColumnName("IOX");
        }
    }
}
