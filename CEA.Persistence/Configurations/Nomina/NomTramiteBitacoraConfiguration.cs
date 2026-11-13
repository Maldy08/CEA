using CEA.Domain.Entities.Nomina;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Nomina
{
    internal class NomTramiteBitacoraConfiguration : IEntityTypeConfiguration<NomTramiteBitacora>
    {
        public void Configure(EntityTypeBuilder<NomTramiteBitacora> entity)
        {
            // El ID lo genera la secuencia/trigger Oracle (ValueGeneratedOnAdd por convención).
            entity.HasKey(e => e.IdBitacora).HasName("PK_NOM_TRAMITES_BITACORA");
            entity.ToTable("NOM_TRAMITES_BITACORA");
            entity.Property(e => e.IdBitacora).HasPrecision(12).HasColumnName("ID_BITACORA");
            entity.Property(e => e.IdTramite).HasPrecision(12).HasColumnName("ID_TRAMITE");
            entity.Property(e => e.FechaMovimiento).HasColumnType("DATE").HasColumnName("FECHA_MOVIMIENTO");
            entity.Property(e => e.IdEmpleado).HasPrecision(4).HasColumnName("ID_EMPLEADO");
            entity.Property(e => e.IdEstatus).HasPrecision(3).HasColumnName("ID_ESTATUS");
            entity.Property(e => e.Accion).HasMaxLength(35).IsUnicode(false).HasColumnName("ACCION");
            entity.Property(e => e.Comentarios).HasMaxLength(1000).IsUnicode(false).HasColumnName("COMENTARIOS");
        }
    }
}
