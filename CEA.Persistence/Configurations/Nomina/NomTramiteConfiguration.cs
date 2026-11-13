using CEA.Domain.Entities.Nomina;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Nomina
{
    internal class NomTramiteConfiguration : IEntityTypeConfiguration<NomTramite>
    {
        public void Configure(EntityTypeBuilder<NomTramite> entity)
        {
            // El ID lo genera la secuencia/trigger Oracle (ValueGeneratedOnAdd por convención).
            entity.HasKey(e => e.IdTramite).HasName("PK_NOM_TRAMITES");
            entity.ToTable("NOM_TRAMITES");
            entity.Property(e => e.IdTramite).HasPrecision(12).HasColumnName("ID_TRAMITE");
            entity.Property(e => e.Ano).HasPrecision(4).HasColumnName("ANO");
            entity.Property(e => e.Folio).HasMaxLength(25).IsUnicode(false).HasColumnName("FOLIO");
            entity.Property(e => e.TipoTramite).HasPrecision(2).HasColumnName("TIPO_TRAMITE");
            entity.Property(e => e.IdEmpleado).HasPrecision(4).HasColumnName("ID_EMPLEADO");
            entity.Property(e => e.Ambito).HasMaxLength(1).IsUnicode(false).HasColumnName("AMBITO");
            entity.Property(e => e.IdEmpleadoSupervisor).HasPrecision(4).HasColumnName("ID_EMPLEADO_SUPERVISOR");
            entity.Property(e => e.FechaSolicitud).HasColumnType("DATE").HasColumnName("FECHA_SOLICITUD");
            entity.Property(e => e.Estatus).HasPrecision(3).HasColumnName("ESTATUS");
            entity.Property(e => e.FechaAutorizacion).HasColumnType("DATE").HasColumnName("FECHA_AUTORIZACION");
            entity.Property(e => e.FechaCancelacion).HasColumnType("DATE").HasColumnName("FECHA_CANCELACION");
            entity.Property(e => e.Valida1).HasPrecision(4).HasColumnName("VALIDA_1");
            entity.Property(e => e.Valida2).HasPrecision(4).HasColumnName("VALIDA_2");
        }
    }
}
