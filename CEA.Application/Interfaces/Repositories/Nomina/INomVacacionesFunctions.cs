using CEA.Application.DTOs.Nomina;

namespace CEA.Application.Interfaces.Repositories.Nomina
{
    public interface INomVacacionesFunctions
    {
        Task<List<SimulaVacacionesDto>> SimulaVacaciones(int noEmp, int dias);
        Task<SaldoVacacionesDto?> SaldoVacaciones(int noEmp);
        Task<List<string?>> ConsultaVacaciones(int noEmp, DateTime desde, DateTime hasta);
    }
}
