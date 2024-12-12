using CEA.Application.DTOs.Viaticos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CEA.Persistence.Configurations.Viaticos
{
    internal class ViaticoDetalleConfiguration : IEntityTypeConfiguration<ViaticoDetalleDto>
    {
        public void Configure(EntityTypeBuilder<ViaticoDetalleDto> entity)
        {
            entity.HasNoKey();
            entity.ToFunction("F_DETALLEVIATICO");

            entity.Property(e => e.NoViatico).HasColumnName("NOVIATICO");
            entity.Property(e => e.Fecha).HasColumnType("DATE").HasColumnName("FECHA");
            entity.Property(e => e.NoEmp).HasColumnName("NOEMP");
            entity.Property(e => e.Nombre).HasColumnName("NOMBRE");
            entity.Property(e => e.Puesto).HasColumnName("PUESTO");
            entity.Property(e => e.Depto).HasColumnName("DEPTO");
            entity.Property(e => e.Origen).HasColumnName("ORIGEN");
            entity.Property(e => e.OrigenNom).HasColumnName("ORIGEN_NOM");
            entity.Property(e => e.Destino).HasColumnName("DESTINO");
            entity.Property(e => e.DestinoNom).HasColumnName("DESTINO_NOM");
            entity.Property(e => e.ComisionTitulo).HasColumnName("COMISION_TITULO");
            entity.Property(e => e.ComisionDetalle).HasColumnName("COMISION_DETALLE");
            entity.Property(e => e.FechaSalida).HasColumnType("DATE").HasColumnName("FECHA_SALIDA");
            entity.Property(e => e.FechaRegreso).HasColumnType("DATE").HasColumnName("FECHA_REGRESO");
            entity.Property(e => e.Dias).HasColumnName("DIAS");
            entity.Property(e => e.Estatus).HasColumnName("ESTATUS");
            entity.Property(e => e.Importe).HasColumnName("IMPORTE");
        }
    }
}
