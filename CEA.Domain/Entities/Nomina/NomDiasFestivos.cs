namespace CEA.Domain.Entities.Nomina
{
    // Catálogo de días festivos (NOM_DIAS_FESTIVOS), compartido con nómina y Checador.Web.
    // Cada tipo de empleado (EMPLEADOS.ID_TIPOEMP: B base, C confianza, T contrato) tiene sus propias reglas.
    // Las oficinas a las que aplica van en NOM_DIAS_FESTIVOS_OFICINAS (sin renglones = todas).
    public class NomDiasFestivos
    {
        public int IdFestivo { get; set; }
        // Día que NO se trabaja (ya recorrido si hubo puente), sin hora.
        public DateTime FechaFestivo { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        // Fecha oficial cuando se recorrió; nula si no se movió.
        public DateTime? FechaOficial { get; set; }
        // 'HH:MI'. Ambas nulas = día completo; con alguna = se trabaja pero se permite llegar tarde / salir temprano.
        public string? HoraEntrada { get; set; }
        public string? HoraSalida { get; set; }
        // Aplica* 1 = ese tipo tiene el festivo; *Sexo nulo = ambos, 'F' / 'M' (EMPLEADOS.SEXO); *ReqHijos 1 = exige hijos.
        public int AplicaBase { get; set; }
        public string? BaseSexo { get; set; }
        public int BaseReqHijos { get; set; }
        public int AplicaConfianza { get; set; }
        public string? ConfSexo { get; set; }
        public int ConfReqHijos { get; set; }
        public int AplicaContrato { get; set; }
        public string? ContSexo { get; set; }
        public int ContReqHijos { get; set; }
        public int Activo { get; set; }
    }
}
