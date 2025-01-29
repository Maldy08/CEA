

using CEA.Application.DTOs.Oficios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Oficios
{
    internal class OficioDtoConfiguration : IEntityTypeConfiguration<OficioDto>
    {
        public void Configure(EntityTypeBuilder<OficioDto> entity)
        {
            entity.HasNoKey();
            entity.ToView("VW_OFICIOS_LISTA_MC");
            entity.Property(e => e.Id).HasPrecision(10).HasColumnName("ID");
            entity.Property(e => e.Ejercicio).HasPrecision(4).HasColumnName("EJERCICIO");
            entity.Property(e => e.Folio).HasPrecision(6).HasColumnName("FOLIO");
            entity.Property(e => e.Eor).HasPrecision(1).HasColumnName("EOR");
            entity.Property(e => e.Tipo).HasPrecision(1).HasColumnName("TIPO");
            entity.Property(e => e.NoOficio).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnName("NO_OFICIO");
            entity.Property(e => e.Pdfpath).HasMaxLength(300).IsUnicode(false).HasColumnName("PDFPATH");
            entity.Property(e => e.Fecha).HasColumnType("DATE").HasColumnName("FECHA");
            entity.Property(e => e.FechaAcuse).HasColumnType("DATE").HasColumnName("FECHA_ACUSE");
            entity.Property(e => e.FechaCaptura).HasColumnType("DATE").HasColumnName("FECHA_CAPTURA");
            entity.Property(e => e.FechaLimite).HasColumnType("DATE").HasColumnName("FECHA_LIMITE");
            entity.Property(e => e.RemDepen).IsRequired().HasMaxLength(100).IsUnicode(false).HasColumnName("REM_DEPEN");
            entity.Property(e => e.RemSiglas).IsRequired().HasMaxLength(25).IsUnicode(false).HasColumnName("REM_SIGLAS");
            entity.Property(e => e.RemNombre).IsRequired().HasMaxLength(100).IsUnicode(false).HasColumnName("REM_NOMBRE");
            entity.Property(e => e.RemCargo).IsRequired().HasMaxLength(100).IsUnicode(false).HasColumnName("REM_CARGO");
            entity.Property(e => e.DestDepen).IsRequired().HasMaxLength(100).IsUnicode(false).HasColumnName("DEST_DEPEN");
            entity.Property(e => e.DestSiglas).IsRequired().HasMaxLength(25).IsUnicode(false).HasColumnName("DEST_SIGLAS");
            entity.Property(e => e.DestNombre).IsRequired().HasMaxLength(100).IsUnicode(false).HasColumnName("DEST_NOMBRE");
            entity.Property(e => e.DestCargo).IsRequired().HasMaxLength(100).IsUnicode(false).HasColumnName("DEST_CARGO");
            entity.Property(e => e.Tema).IsRequired().HasMaxLength(250).IsUnicode(false).HasColumnName("TEMA");
            entity.Property(e => e.Estatus).HasPrecision(2).HasColumnName("ESTATUS");
            entity.Property(e => e.Empqentrega).HasPrecision(4).HasColumnName("EMPQENTREGA");
            entity.Property(e => e.Relacionoficio).HasMaxLength(10).IsUnicode(false).HasColumnName("RELACIONOFICIO");
            entity.Property(e => e.Depto).HasPrecision(2).HasColumnName("DEPTO");
            entity.Property(e => e.DeptoRespon).HasPrecision(2).HasColumnName("DEPTO_RESPON");
            entity.Property(e => e.IdEmpleado).HasPrecision(4).HasColumnName("ID_EMPLEADO");
            entity.Property(e => e.NombreResponsable).HasColumnName("NOMBRE_RESPON").IsUnicode(false);
            entity.Property(e => e.Rol).HasPrecision(1).HasColumnName("ROL");

        }
    }
}
