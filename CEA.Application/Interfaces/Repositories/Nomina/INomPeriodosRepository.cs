using CEA.Application.DTOs.Nomina;

namespace CEA.Application.Interfaces.Repositories.Nomina
{
    public interface INomPeriodosRepository
    {
        public Task<List<NomPeriodosDto>> GetNomPeriodosByTipoNomEjercicioAsync(int tipoNom, int ejercicio);

    }
}
