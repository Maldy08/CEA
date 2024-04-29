using CEA.Application.DTOs.Viaticos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CEA.Persistence.Configurations.Viaticos
{
    internal class ViaticosPorEmpleadoConfiguration : IEntityTypeConfiguration<ViaticosPorEmpleadoDto>
    {
        public void Configure(EntityTypeBuilder<ViaticosPorEmpleadoDto> builder)
        {
            builder.HasNoKey();
            builder.Property(e => e.Viatico).HasColumnName("VIATICO");
            builder.Property(e => e.Fecha).HasColumnType("DATE").HasColumnName("FECHA");
            builder.Property(e => e.Origen).HasMaxLength(300).HasColumnName("ORIGEN");
            builder.Property(e => e.Destino).HasMaxLength(300).HasColumnName("DESTINO");
            builder.Property(e => e.Motivo).HasMaxLength(300).HasColumnName("MOTIVO");
            builder.Property(e => e.Salida).HasColumnType("DATE").HasColumnName("SALIDA");
            builder.Property(e => e.Regreso).HasColumnType("DATE").HasColumnName("REGRESO");
            builder.Property(e => e.Estatus).HasMaxLength(1).HasColumnName("ESTATUS");
            builder.Property(e => e.Oficina).HasPrecision(1).HasColumnName("OFICINA");
            builder.Property(e => e.Ejercicio).HasPrecision(4).HasColumnName("EJERCICIO");
            builder.ToFunction("F_LISTAVIATICOSXEMP");
        }
    }
}
