using CEA.Application.DTOs.Nomina;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CEA.Persistence.Configurations.Nomina
{
    internal class FormatoTramiteEncabezadoDtoConfiguration : IEntityTypeConfiguration<FormatoTramiteEncabezadoDto>
    {
        public void Configure(EntityTypeBuilder<FormatoTramiteEncabezadoDto> entity)
        {
            entity.HasNoKey();
            entity.ToView("VS_TRAMITES_FORMATO");
            entity.Property(e => e.IdTramite).HasColumnName("ID_TRAMITE");
            entity.Property(e => e.TipoTramite).HasColumnName("TIPO_TRAMITE");
            entity.Property(e => e.DescTipo).HasColumnName("DESC_TIPO");
            entity.Property(e => e.Folio).HasColumnName("FOLIO");
            entity.Property(e => e.Ano).HasColumnName("ANO");
            entity.Property(e => e.FechaSolicitud).HasColumnName("FECHA_SOLICITUD");
            entity.Property(e => e.Estatus).HasColumnName("ESTATUS");
            entity.Property(e => e.DescEstatus).HasColumnName("DESC_ESTATUS");
            entity.Property(e => e.Modalidad).HasColumnName("MODALIDAD");
            entity.Property(e => e.NoEmp).HasColumnName("NOEMP");
            entity.Property(e => e.Nombre).HasColumnName("NOMBRE");
            entity.Property(e => e.Rfc).HasColumnName("RFC");
            entity.Property(e => e.FechaIngreso).HasColumnName("FECHA_INGRESO");
            entity.Property(e => e.Direccion).HasColumnName("DIRECCION");
            entity.Property(e => e.Departamento).HasColumnName("DEPARTAMENTO");
            entity.Property(e => e.Puesto).HasColumnName("PUESTO");
            entity.Property(e => e.Categoria).HasColumnName("CATEGORIA");
            entity.Property(e => e.FechaInicio).HasColumnName("FECHA_INICIO");
            entity.Property(e => e.FechaFin).HasColumnName("FECHA_FIN");
            entity.Property(e => e.FechaRegreso).HasColumnName("FECHA_REGRESO");
            entity.Property(e => e.Dias).HasColumnName("DIAS");
            entity.Property(e => e.NoEmpCubre).HasColumnName("NOEMP_CUBRE");
            entity.Property(e => e.NombreCubre).HasColumnName("NOMBRE_CUBRE");
            entity.Property(e => e.TipoIncidencia).HasColumnName("TIPO_INCIDENCIA");
            entity.Property(e => e.DescIncidencia).HasColumnName("DESC_INCIDENCIA");
            entity.Property(e => e.Motivo).HasColumnName("MOTIVO");
            entity.Property(e => e.Observaciones).HasColumnName("OBSERVACIONES");
            entity.Property(e => e.Firma1NoEmp).HasColumnName("FIRMA1_NOEMP");
            entity.Property(e => e.Firma1Nombre).HasColumnName("FIRMA1_NOMBRE");
            entity.Property(e => e.Firma1Puesto).HasColumnName("FIRMA1_PUESTO");
            entity.Property(e => e.Firma2NoEmp).HasColumnName("FIRMA2_NOEMP");
            entity.Property(e => e.Firma2Nombre).HasColumnName("FIRMA2_NOMBRE");
            entity.Property(e => e.Firma2Puesto).HasColumnName("FIRMA2_PUESTO");
            entity.Property(e => e.Firma3NoEmp).HasColumnName("FIRMA3_NOEMP");
            entity.Property(e => e.Firma3Nombre).HasColumnName("FIRMA3_NOMBRE");
            entity.Property(e => e.Firma3Puesto).HasColumnName("FIRMA3_PUESTO");
        }
    }

    internal class FormatoTramiteDetalleDtoConfiguration : IEntityTypeConfiguration<FormatoTramiteDetalleDto>
    {
        public void Configure(EntityTypeBuilder<FormatoTramiteDetalleDto> entity)
        {
            entity.HasNoKey();
            entity.ToView("VS_TRAMITES_FORMATO_DET");
            entity.Property(e => e.IdTramite).HasColumnName("ID_TRAMITE");
            entity.Property(e => e.Orden).HasColumnName("ORDEN");
            entity.Property(e => e.Concepto1).HasColumnName("CONCEPTO_1");
            entity.Property(e => e.Concepto2).HasColumnName("CONCEPTO_2");
            entity.Property(e => e.Valor1).HasColumnName("VALOR_1");
            entity.Property(e => e.Valor2).HasColumnName("VALOR_2");
            entity.Property(e => e.Valor3).HasColumnName("VALOR_3");
        }
    }
}
