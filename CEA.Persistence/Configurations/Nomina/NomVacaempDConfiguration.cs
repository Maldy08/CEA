using CEA.Domain.Entities.Nomina;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Nomina
{
    internal class NomVacaempDConfiguration : IEntityTypeConfiguration<NomVacaempD>
    {
        public void Configure(EntityTypeBuilder<NomVacaempD> entity)
        {
            entity.HasKey(e => new { e.IdVacaemp, e.PerVac }).HasName("PK_NOM_VACAEMP_D");
            entity.ToTable("NOM_VACAEMP_D");
            entity.Property(e => e.IdVacaemp).HasPrecision(12).ValueGeneratedNever().HasColumnName("ID_VACAEMP");
            entity.Property(e => e.NoEmp).HasPrecision(4).HasColumnName("NOEMP");
            entity.Property(e => e.Ano).HasPrecision(4).HasColumnName("ANO");
            entity.Property(e => e.NoMov).HasPrecision(4).HasColumnName("NOMOV");
            entity.Property(e => e.FDesde).HasColumnType("DATE").HasColumnName("FDESDE");
            entity.Property(e => e.FHasta).HasColumnType("DATE").HasColumnName("FHASTA");
            entity.Property(e => e.DiasVac).HasPrecision(3).HasColumnName("DIASVAC");
            entity.Property(e => e.PerVac).HasMaxLength(6).IsUnicode(false).HasColumnName("PERVAC");
            entity.Property(e => e.DiasXDisf).HasPrecision(3).HasColumnName("DIASXDISF");
        }
    }
}
