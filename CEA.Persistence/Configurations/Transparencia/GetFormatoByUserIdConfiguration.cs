using CEA.Application.DTOs.Transparencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Transparencia
{
    internal class GetFormatoByUserIdConfiguration : IEntityTypeConfiguration<GetFormatoByUserIdDto>
    {
        public void Configure(EntityTypeBuilder<GetFormatoByUserIdDto> builder)
        {
            builder.HasNoKey();
            builder.ToView("GetFormatosByUserId");
            //builder.Property(e => e.IdDepto).HasColumnName("IDDEPTO");
            //builder.Property(e => e.IdUsuario).HasColumnName("IDUSUARIO");
            //builder.Property(e => e.Codigo).HasColumnName("CODIGO");
            //builder.Property(e => e.Descripcion).HasColumnName("DESCRIPCION");
        }
    }
}
