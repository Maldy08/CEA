using CEA.Domain.Entities.Viaticos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Viaticos
{
    internal class ViaticoConfiguration : IEntityTypeConfiguration<Viatico>
    {
        public void Configure(EntityTypeBuilder<Viatico> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("VIATICOS");
            builder.Property(e => e.Id).HasColumnName("ID");
            builder.Property(e => e.Oficina).HasPrecision(1).HasColumnName("OFICINA");
            builder.Property(e => e.Ejercicio).HasPrecision(4).HasColumnName("EJERCICIO");
            builder.Property(e => e.NoViat).HasPrecision(5).HasColumnName("NOVIAT");
            builder.Property(e => e.Fecha).HasColumnType("DATE").HasColumnName("FECHA");
            builder.Property(e => e.NoEmp).HasPrecision(4).HasColumnName("NOEMP");
            builder.Property(e => e.OrigenId).HasPrecision(3).HasColumnName("ORIGENID");
            builder.Property(e => e.DestinoId).HasPrecision(3).HasColumnName("DESTINOID");
            builder.Property(e => e.Motivo).HasMaxLength(300).HasColumnName("MOTIVO");
            builder.Property(e => e.FechaSal).HasColumnType("DATE").HasColumnName("FECHASAL");
            builder.Property(e => e.FechaReg).HasColumnType("DATE").HasColumnName("FECHAREG");
            builder.Property(e => e.Dias).HasPrecision(2).HasColumnName("DIAS");
            builder.Property(e => e.InforFecha).HasColumnType("DATE").HasColumnName("INFOR_FECHA");
            builder.Property(e => e.InforAct).HasMaxLength(500).HasColumnName("INFOR_ACT");
            builder.Property(e => e.Nota).HasMaxLength(500).HasColumnName("NOTA");
            builder.Property(e => e.Estatus).HasPrecision(1).HasColumnName("ESTATUS");
            builder.Property(e => e.FechaMod).HasColumnType("DATE").HasColumnName("FECHAMOD");
            builder.Property(e => e.Pol).HasPrecision(4).HasColumnName("POL");
            builder.Property(e => e.PolMes).HasPrecision(2).HasColumnName("POLMES");
            builder.Property(e => e.Caja).HasPrecision(2).HasColumnName("CAJA");
            builder.Property(e => e.CajaVale).HasPrecision(5).HasColumnName("CAJA_VALE");
            builder.Property(e => e.CajaRepo).HasPrecision(5).HasColumnName("CAJA_REPO");
            builder.Property(e => e.NoEmpCrea).HasPrecision(4).HasColumnName("NOEMP_CREA");
            builder.Property(e => e.InforResul).HasMaxLength(500).HasColumnName("INFOR_RESUL");

        }
    }
}
