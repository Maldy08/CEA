using CEA.Domain.Entities.Nomina;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Nomina
{
    internal class NomPeriodosConfiguration : IEntityTypeConfiguration<NomPeriodos>
    {
        public void Configure(EntityTypeBuilder<NomPeriodos> entity)
        {
            entity.HasKey(e => new { e.PerNom, e.AnoProceso })
                .HasName("PK_NOMPERIODOS");
            entity.ToTable("NOM_PERIODOS");

            entity.Property(e => e.TipoNom)
                .HasColumnName("TIPONOM");

            entity.Property(e => e.PerNom)
                .HasColumnName("PERNOM");

            entity.Property(e => e.FechaDes)
                .HasColumnType("DATE")
                .HasColumnName("FECHADES");

            entity.Property(e => e.FechaHas)
                .HasColumnType("DATE")
                .HasColumnName("FECHAHAS");

            entity.Property(e => e.FincDes)
                .HasColumnType("DATE")
                .HasColumnName("FINCDES");

            entity.Property(e => e.FinciHas)
                .HasColumnType("DATE")
                .HasColumnName("FINCIHAS");

            entity.Property(e => e.FechaAp)
                .HasColumnType("DATE")
                .HasColumnName("FECHAAP");

            entity.Property(e => e.PolDev)
                .HasColumnName("POLDEV");

            entity.Property(e => e.PolEje)
                .HasColumnName("POLEJE");

            entity.Property(e => e.PolPag)
                .HasColumnName("POLPAG");

            entity.Property(e => e.Referencia)
                .HasColumnName("REFERENCIA");

            entity.Property(e => e.Estatus)
                .HasColumnName("ESTATUS");

            entity.Property(e => e.AnoProceso)
                .HasColumnName("ANOPROCESO");

            entity.Property(e => e.TotalPerc)
                .HasColumnType("DECIMAL(18, 2)")
                .HasColumnName("TOTALPERC");

            entity.Property(e => e.TotalDesc)
                .HasColumnType("DECIMAL(18, 2)")
                .HasColumnName("TOTALDESC");

            entity.Property(e => e.CatXMes)
                .HasColumnName("CATXMES");


        }
    }
}
