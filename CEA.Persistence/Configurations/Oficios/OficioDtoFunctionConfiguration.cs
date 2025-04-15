using CEA.Application.DTOs.Oficios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Oficios
{
    internal class OficioDtoFunctionConfiguration : IEntityTypeConfiguration<OficioDtoFunction>
    {
        public void Configure(EntityTypeBuilder<OficioDtoFunction> entity)
        {
            entity.HasNoKey();
            entity.ToFunction("F_LISTADOOFICIOS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Ren).HasColumnName("REN");
            entity.Property(e => e.Ejercicio).HasColumnName("EJERCICIO");
            entity.Property(e => e.Folio).HasColumnName("FOLIO");
            entity.Property(e => e.Eor).HasColumnName("EOR");
            entity.Property(e => e.Tipo).HasColumnName("TIPO");
            entity.Property(e => e.NoOficio).HasColumnName("NO_OFICIO");
            entity.Property(e => e.Pdfpath).HasColumnName("PDFPATH");
            entity.Property(e => e.Fecha).HasColumnName("FECHA").HasColumnType("DATE");
            entity.Property(e => e.FechaAcuse).HasColumnName("FECHA_ACUSE").HasColumnType("DATE");
            entity.Property(e => e.FechaCaptura).HasColumnName("FECHA_CAPTURA").HasColumnType("DATE");
            entity.Property(e => e.FechaLimite).HasColumnName("FECHA_LIMITE").HasColumnType("DATE");
            entity.Property(e => e.RemDepen).HasColumnName("REM_DEPEN");
            entity.Property(e => e.RemSiglas).HasColumnName("REM_SIGLAS");
            entity.Property(e => e.RemNombre).HasColumnName("REM_NOMBRE");
            entity.Property(e => e.RemCargo).HasColumnName("REM_CARGO");
            entity.Property(e => e.DestDepen).HasColumnName("DEST_DEPEN");
            entity.Property(e => e.DestSiglas).HasColumnName("DEST_SIGLAS");
            entity.Property(e => e.DestNombre).HasColumnName("DEST_NOMBRE");
            entity.Property(e => e.DestCargo).HasColumnName("DEST_CARGO");
            entity.Property(e => e.Tema).HasColumnName("TEMA");
            entity.Property(e => e.Estatus).HasColumnName("ESTATUS");
            entity.Property(e => e.Empqentrega).HasColumnName("EMPQENTREGA");
            entity.Property(e => e.Relacionoficio).HasColumnName("RELACIONOFICIO");
            entity.Property(e => e.Depto).HasColumnName("DEPTO");
            entity.Property(e => e.DeptoRespon).HasColumnName("DEPTO_RESPON");
            entity.Property(e => e.Observaciones).HasColumnName("OBSERVACIONES");
            entity.Property(e => e.Rol).HasColumnName("ROL");
            entity.Property(e => e.EstatusNum).HasColumnName("ESTATUSNUM");
        }
    }
}
