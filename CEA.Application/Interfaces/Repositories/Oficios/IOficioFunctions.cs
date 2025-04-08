using CEA.Application.DTOs.Oficios;

namespace CEA.Application.Interfaces.Repositories.Oficios
{
    public interface IOficioFunctions
    {
        Task<List<OficioDtoFunction>> GetListadoOficioFunction(int ejercicio, int eor, int idEmpleado);
        Task<List<OficioContadoresDashboardDto>> GetContadoresDashboard(int ejercicio, int idEmpleado);
        Task<List<OficioListaDashboardDto>> GetListaDashboard(int ejercicio, int idEmpleado);
        Task SaveBitacoraOficio(List<OficioBitacoraDto> bitacoraOficio);
        string ConvertOficioBitacoraDtoToXml(List<OficioBitacoraDto> datos);
        Task<OficioSpInsertarResult> SaveOficioSP(OficioDto oficioDto);
        string ConvertOficioDtoToXml(OficioDto oficioDto);
        Task<OficioSpFoliarResult> OficioSPFoliar(int ejercicio, int folio, int eor, int idEmpleado);

    }
}
