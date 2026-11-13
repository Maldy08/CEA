namespace CEA.Application.DTOs.Nomina
{
    // Renglón crudo de FN_CONSULTA_VACACIONES: 1) días hábiles, 2) festivos o mensaje de error,
    // 3..N) periodos "NOEMP|ANO|DIAS|PERIODO|DIASXDISFRUTAR". Orden = ROWNUM, para no depender del orden de lectura.
    public class ConsultaVacacionesRenglonDto
    {
        public int Orden { get; set; }
        public string? Valor { get; set; }
    }
}
