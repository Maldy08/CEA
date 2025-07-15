

using CEA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations
{
    internal class UsuarioConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)

        {


            builder.HasNoKey();
            builder.ToView("VS_USUARIOS");
            builder.Property(e => e.Activo).HasPrecision(1).HasColumnName("ACTIVO");
            builder.Property(e => e.Depto).HasPrecision(4).HasColumnName("DEPTO");
            builder.Property(e => e.DeptoDescripcion).IsRequired().HasMaxLength(100).IsUnicode(false).HasColumnName("DEPTO_DESCRIPCION");
            builder.Property(e => e.Descripcion).IsRequired().HasMaxLength(100).IsUnicode(false).HasColumnName("DESCRIPCION");
            builder.Property(e => e.IdPue).HasPrecision(3).HasColumnName("ID_PUE");
            builder.Property(e => e.Login).HasMaxLength(20).IsUnicode(false).HasColumnName("LOGIN");
            builder.Property(e => e.NoEmpleado).HasPrecision(4).HasColumnName("NO_EMPLEADO");
            builder.Property(e => e.NombreCompleto).HasMaxLength(152).IsUnicode(false).HasColumnName("NOMBRE_COMPLETO");
            builder.Property(e => e.Pass).HasMaxLength(20).IsUnicode(false).HasColumnName("PASS");
            builder.Property(e => e.Usuario).HasPrecision(2).HasColumnName("USUARIO");
            builder.Property(e => e.Viaticos).HasPrecision(1).HasColumnName("VIATICOS");
            builder.Property(e => e.ViaticosNivel).HasPrecision(1).HasColumnName("VIATICOS_NIVEL");
            builder.Property(e => e.Municipio).HasPrecision(1).HasColumnName("MUNICIPIO");
            builder.Property(e => e.Oficina).HasPrecision(1).HasColumnName("OFICINA");
            builder.Property(e => e.Compras).HasPrecision(1).HasColumnName("COMPRAS");
            builder.Property(e => e.ComprasNivel).HasPrecision(1).HasColumnName("COMPRAS_NIVEL");
            builder.Property(e => e.Almacen).HasPrecision(1).HasColumnName("ALMACEN");
            builder.Property(e => e.AlmacenNivel).HasPrecision(1).HasColumnName("ALMACEN_NIVEL");
            builder.Property(e => e.Activos).HasPrecision(1).HasColumnName("ACTIVOS");
            builder.Property(e => e.ActivosNivel).HasPrecision(1).HasColumnName("ACTIVOS_NIVEL");
            builder.Property(e => e.Contabilidad).HasPrecision(1).HasColumnName("CONTABILIDAD");
            builder.Property(e => e.ContabilidadNivel).HasPrecision(1).HasColumnName("CONTABILIDAD_NIVEL");
            builder.Property(e => e.Presupuestos).HasPrecision(1).HasColumnName("PRESUPUESTOS");
            builder.Property(e => e.PresupuestosNivel).HasPrecision(1).HasColumnName("PRESUPUESTOS_NIVEL");
            builder.Property(e => e.Nominas).HasPrecision(1).HasColumnName("NOMINAS");
            builder.Property(e => e.NominasNivel).HasPrecision(1).HasColumnName("NOMINAS_NIVEL");
            builder.Property(e => e.Bd).HasPrecision(1).HasColumnName("BD");
            builder.Property(e => e.Caja).HasPrecision(1).HasColumnName("CAJA");
            builder.Property(e => e.CajaNivel).HasPrecision(1).HasColumnName("CAJA_NIVEL");
            builder.Property(e => e.Polnom).HasMaxLength(20).IsUnicode(false).HasColumnName("POLNOM");
            builder.Property(e => e.Vales).HasPrecision(1).HasColumnName("VALES");
            builder.Property(e => e.ValesNivel).HasPrecision(1).HasColumnName("VALES_NIVEL");
            builder.Property(e => e.Oficios).HasPrecision(1).HasColumnName("OFICIOS");
            builder.Property(e => e.OficiosNivel).HasPrecision(1).HasColumnName("OFICIOS_NIVEL");
            builder.Property(e => e.Nombre).HasColumnName("NOMBRE");
            builder.Property(e => e.Paterno).HasColumnName("PATERNO");
            builder.Property(e => e.Materno).HasColumnName("MATERNO");

        }
    }
}
