using CEA.Domain.Entities.Oficios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CEA.Persistence.Configurations.Oficios
{
    internal class OficioConfiguration : IEntityTypeConfiguration<Oficio>
    {
        public void Configure(EntityTypeBuilder<Oficio> entity)
        {
   
                entity.HasKey(e =>e.Id).HasName("OFICIOS_PK");
                entity.ToTable("OFICIOS");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Ejercicio).HasPrecision(4).HasColumnName("EJERCICIO");
                entity.Property(e => e.Folio).HasPrecision(6).HasColumnName("FOLIO");
                entity.Property(e => e.Eor).HasPrecision(1).HasColumnName("EOR");
                entity.Property(e => e.Depto).HasPrecision(2).HasColumnName("DEPTO");
                entity.Property(e => e.DeptoRespon).HasPrecision(2).HasColumnName("DEPTO_RESPON");
                entity.Property(e => e.DestCargo).HasMaxLength(100).IsUnicode(false).HasColumnName("DEST_CARGO");
                entity.Property(e => e.DestDepen).HasMaxLength(100).IsUnicode(false).HasColumnName("DEST_DEPEN");
                entity.Property(e => e.DestNombre).HasMaxLength(100).IsUnicode(false).HasColumnName("DEST_NOMBRE");
                entity.Property(e => e.DestSiglas).HasMaxLength(25).IsUnicode(false).HasColumnName("DEST_SIGLAS");
                entity.Property(e => e.Empqentrega).HasPrecision(4).HasColumnName("EMPQENTREGA");
                entity.Property(e => e.Estatus).HasPrecision(2).HasColumnName("ESTATUS");
                entity.Property(e => e.Fecha).HasColumnType("DATE").HasColumnName("FECHA");
                entity.Property(e => e.FechaAcuse).HasColumnType("DATE").HasColumnName("FECHA_ACUSE");
                entity.Property(e => e.FechaCaptura).HasColumnType("DATE").HasColumnName("FECHA_CAPTURA");
                entity.Property(e => e.FechaLimite).HasColumnType("DATE").HasColumnName("FECHA_LIMITE");
                entity.Property(e => e.NoOficio).HasMaxLength(50).IsUnicode(false).HasColumnName("NO_OFICIO");
                entity.Property(e => e.Pdfpath).HasMaxLength(600).IsUnicode(false).HasColumnName("PDFPATH");
                entity.Property(e => e.Relacionoficio).HasMaxLength(10).IsUnicode(false).HasColumnName("RELACIONOFICIO");
                entity.Property(e => e.RemCargo).HasMaxLength(100).IsUnicode(false).HasColumnName("REM_CARGO");
                entity.Property(e => e.RemDepen).HasMaxLength(100).IsUnicode(false).HasColumnName("REM_DEPEN");
                entity.Property(e => e.RemNombre).HasMaxLength(100).IsUnicode(false).HasColumnName("REM_NOMBRE");
                entity.Property(e => e.RemSiglas).HasMaxLength(25).IsUnicode(false).HasColumnName("REM_SIGLAS");
                entity.Property(e => e.Tema).HasMaxLength(250).IsUnicode(false).HasColumnName("TEMA");
                entity.Property(e => e.Tipo).HasPrecision(1).HasColumnName("TIPO");
                entity.Property(e => e.idClasificacion).HasPrecision(2).HasColumnName("ID_CLASIFICACION");
        }
    }
}
