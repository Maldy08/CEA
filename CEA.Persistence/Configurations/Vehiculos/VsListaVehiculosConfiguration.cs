using CEA.Application.DTOs.Vehiculos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CEA.Persistence.Configurations.Vehiculos
{
    internal class VsListaVehiculosConfiguration : IEntityTypeConfiguration<VsListaVehiculosDto>
    {
        public void Configure(EntityTypeBuilder<VsListaVehiculosDto> builder)
        {
            builder.HasNoKey();
            builder.ToView("VS_LISTAVEHICULOS");
            builder.Property(e => e.Numero).HasColumnName("NUMERO");
            builder.Property(e => e.Ano).HasColumnName("ANO");
            builder.Property(e => e.NoActivo).HasColumnName("NOACTIVO");
            builder.Property(e => e.Placas).HasColumnName("PLACAS");
            builder.Property(e => e.Color).HasColumnName("COLOR");
            builder.Property(e => e.Odometro).HasColumnName("ODOMETRO");
            builder.Property(e => e.Estatus).HasColumnName("ESTATUS");
            builder.Property(e => e.Ubicacion).HasColumnName("UBICACION");
            builder.Property(e => e.FUltServ).HasColumnName("F_ULTSERV").HasColumnType("DATE");
            builder.Property(e => e.FProxServ).HasColumnName("F_PROXSERV").HasColumnType("DATE");
            builder.Property(e => e.Tipo).HasColumnName("TIPO");
            builder.Property(e => e.Capacidad).HasColumnName("CAPACIDAD");
            builder.Property(e => e.Pernoc).HasColumnName("PERNOC");
            builder.Property(e => e.Comentarios).HasColumnName("COMENTARIOS");
            builder.Property(e => e.Marca).HasColumnName("MARCA");
            builder.Property(e => e.Modelo).HasColumnName("MODELO");
            builder.Property(e => e.Serie).HasColumnName("SERIE");
            builder.Property(e => e.Descripcion).HasColumnName("DESCRIPCION");
            builder.Property(e => e.FechaAdq).HasColumnName("FECHAADQ").HasColumnType("DATE");
            builder.Property(e => e.BmEstatus).HasColumnName("BM_ESTATUS");
            builder.Property(e => e.Importe).HasColumnName("IMPORTE");
            builder.Property(e => e.Resguardo).HasColumnName("RESGUARDO");
            builder.Property(e => e.Depto).HasColumnName("DEPTO");
            builder.Property(e => e.Resguardante).HasColumnName("RESGUARDANTE");
            builder.Property(e => e.NombreAseg).HasColumnName("NOMBREASEG");
            builder.Property(e => e.NoSeguro).HasColumnName("NOSEGURO");
            builder.Property(e => e.Vigencia).HasColumnName("VIGENCIA").HasColumnType("DATE");

        }
    }
}
