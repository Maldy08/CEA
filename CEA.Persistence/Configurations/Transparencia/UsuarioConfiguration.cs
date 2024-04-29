

using CEA.Domain.Common.Interfaces;
using CEA.Domain.Entities.Transparencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Transparencia
{
    internal class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(e => e.IdUsuario);
            builder.ToTable("Usuarios");

            builder.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            builder.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            builder.Property(e => e.Usuario1)
                .HasMaxLength(50)
                .IsUnicode(false)
            .HasColumnName("Usuario");

            builder.HasOne(d => d.IdDeptoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdDepto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_Departamentos");

            builder.HasOne(d => d.IdNivelNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdNivel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_UsuariosNiveles");

            builder.HasOne(d => d.IdPuestoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdPuesto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_Puestos");
        }
    }
}
