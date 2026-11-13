namespace CEA.Application.DTOs.Nomina
{
    // Formato oficial impreso de un trámite de RH: VS_TRAMITES_FORMATO (encabezado) + VS_TRAMITES_FORMATO_DET (desglose).
    // Igual para todos los tipos de trámite; lo que no aplica al tipo viene null.
    public class FormatoTramiteDto
    {
        public FormatoTramiteEncabezadoDto Encabezado { get; set; } = new();
        public List<FormatoTramiteDetalleDto> Detalle { get; set; } = new();
    }

    // Renglón de VS_TRAMITES_FORMATO.
    public class FormatoTramiteEncabezadoDto
    {
        public int IdTramite { get; set; }
        public int TipoTramite { get; set; }
        public string? DescTipo { get; set; }
        public string Folio { get; set; } = string.Empty;
        public int Ano { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public int Estatus { get; set; }
        public string? DescEstatus { get; set; }
        public string? Modalidad { get; set; }

        public int NoEmp { get; set; }
        public string? Nombre { get; set; }
        public string? Rfc { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public string? Direccion { get; set; }
        public string? Departamento { get; set; }
        public string? Puesto { get; set; }
        public string? Categoria { get; set; }

        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public DateTime? FechaRegreso { get; set; }
        public int? Dias { get; set; }
        public int? NoEmpCubre { get; set; }
        public string? NombreCubre { get; set; }
        // Permisos / justificantes (NOM_PERMISOS_JUSTIFICANTES)
        public int? TipoIncidencia { get; set; }
        public string? DescIncidencia { get; set; }
        public string? Motivo { get; set; }
        public string? Observaciones { get; set; }

        public int? Firma1NoEmp { get; set; }
        public string? Firma1Nombre { get; set; }
        public string? Firma1Puesto { get; set; }
        public int? Firma2NoEmp { get; set; }
        public string? Firma2Nombre { get; set; }
        public string? Firma2Puesto { get; set; }
        public int? Firma3NoEmp { get; set; }
        public string? Firma3Nombre { get; set; }
        public string? Firma3Puesto { get; set; }
    }

    // Renglón de VS_TRAMITES_FORMATO_DET. Significado de cada columna según el tipo de trámite
    // (vacaciones: año, periodo, saldo anterior, solicitados, restantes).
    public class FormatoTramiteDetalleDto
    {
        public int IdTramite { get; set; }
        public int Orden { get; set; }
        public string? Concepto1 { get; set; }
        public string? Concepto2 { get; set; }
        public decimal? Valor1 { get; set; }
        public decimal? Valor2 { get; set; }
        public decimal? Valor3 { get; set; }
    }
}
