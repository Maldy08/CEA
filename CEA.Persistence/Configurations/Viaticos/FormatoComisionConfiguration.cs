using CEA.Application.DTOs.Viaticos;
using CEA.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEA.Persistence.Configurations.Viaticos
{
    internal class FormatoComisionConfiguration : IEntityTypeConfiguration<FormatoComisionDto>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<FormatoComisionDto> builder)
        {

            builder.HasNoKey();
            builder.ToView("VS_FORMATOCOMISION_VIATICOS");
            builder.Property(e => e.Oficina).HasColumnName("OFICINA");
            builder.Property(e => e.Ejercicio).HasColumnName("EJERCICIO");
            builder.Property(e => e.NoViat).HasColumnName("NOVIAT");
            builder.Property(e => e.Fecha).HasColumnName("FECHA").HasColumnType("DATE");
            builder.Property(e => e.NoEmp).HasColumnName("NOEMP");
            builder.Property(e => e.OrigenId).HasColumnName("ORIGENID");
            builder.Property(e => e.DestinoId).HasColumnName("DESTINOID");
            builder.Property(e => e.Motivo).HasColumnName("MOTIVO");
            builder.Property(e => e.FechaSal).HasColumnName("FECHASAL").HasColumnType("DATE");
            builder.Property(e => e.FechaReg).HasColumnName("FECHAREG").HasColumnType("DATE");
            builder.Property(e => e.Dias).HasColumnName("DIAS");
            builder.Property(e => e.InforAct).HasColumnName("INFOR_ACT");
            builder.Property(e => e.Importe).HasColumnName("IMPORTE").HasColumnType("FLOAT").HasPrecision(126);
            builder.Property(e => e.InforResul).HasColumnName("INFOR_RESUL");
            builder.Property(e => e.Nombre).HasColumnName("NOMBRE");
            builder.Property(e => e.Materno).HasColumnName("MATERNO");
            builder.Property(e => e.Paterno).HasColumnName("PATERNO");
            builder.Property(e => e.DescripcionPuesto).HasColumnName("DESCRIPCIONPUESTO");
            builder.Property(e => e.CdOrigen).HasColumnName("CDORIGEN");
            builder.Property(e => e.CdDestino).HasColumnName("CDDESTINO");
            builder.Property(e => e.QuienLoComisiona).HasColumnName("QUIENLOCOMISIONA");
            builder.Property(e => e.PuestoQuienLoComisiona).HasColumnName("PUESTOQUIENLOCOMISIONA");
            builder.Property(e => e.EdoOrigen).HasColumnName("EDOORIGEN");
            builder.Property(e => e.EdoDestino).HasColumnName("EDODESTINO");
            builder.Property(e => e.DeptoDescripcion).HasColumnName("DEPTODESCRIPCION");
            builder.Property(e => e.DirectorAdministrativo).HasColumnName("DIRECTOR_ADMINISTRATIVO");
        }
    }
}
