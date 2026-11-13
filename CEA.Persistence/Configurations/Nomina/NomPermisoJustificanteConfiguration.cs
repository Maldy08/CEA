using CEA.Domain.Entities.Nomina;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Nomina
{
    internal class NomPermisoJustificanteConfiguration : IEntityTypeConfiguration<NomPermisoJustificante>
    {
        public void Configure(EntityTypeBuilder<NomPermisoJustificante> entity)
        {
            // El ID lo genera SEQ_NOM_PERMISOS_JUSTIFICANTES vía TRG_NOM_PERM_JUST_BI.
            entity.HasKey(e => e.IdPermisoJustificante).HasName("PK_NOM_PERM_JUST");
            entity.ToTable("NOM_PERMISOS_JUSTIFICANTES");
            entity.Property(e => e.IdPermisoJustificante).HasPrecision(12).HasColumnName("ID_PERMISO_JUSTIFICANTE");
            entity.Property(e => e.IdTramite).HasPrecision(12).HasColumnName("ID_TRAMITE");
            entity.Property(e => e.TipoIncidencia).HasPrecision(1).HasColumnName("TIPO_INCIDENCIA");
            entity.Property(e => e.FechaInicio).HasColumnType("DATE").HasColumnName("FECHA_INICIO");
            entity.Property(e => e.FechaFin).HasColumnType("DATE").HasColumnName("FECHA_FIN");
            entity.Property(e => e.JustificacionMotivo).HasMaxLength(500).IsUnicode(false).HasColumnName("JUSTIFICACION_MOTIVO");
            entity.Property(e => e.IdEmpleadoCubre).HasPrecision(4).HasColumnName("ID_EMPLEADO_CUBRE");
        }
    }
}
