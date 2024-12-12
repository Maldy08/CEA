using CEA.Application.DTOs.Vehiculos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CEA.Persistence.Configurations.Vehiculos
{
    internal class VsWtVehiculosConfiguration : IEntityTypeConfiguration<VsWtVehiculosDto>
    {
        public void Configure(EntityTypeBuilder<VsWtVehiculosDto> builder)
        {
            builder.HasNoKey();
            builder.ToView("VS_WT_VEHICULOS");
            builder.Property(e => e.Numero).HasColumnName("NUMERO");
            builder.Property(e => e.Marca).HasColumnName("MARCA");
            builder.Property(e => e.Modelo).HasColumnName("MODELO");
            builder.Property(e => e.Placas).HasColumnName("PLACAS");
            builder.Property(e => e.Serie).HasColumnName("SERIE");
            builder.Property(e => e.Descripcion).HasColumnName("DESCRIPCION");
            builder.Property(e => e.Depto).HasColumnName("DEPTO");
            builder.Property(e => e.Ano).HasColumnName("ANO");
        }
    }
}
