



using CEA.Application.DTOs;
using CEA.Application.DTOs.Oficios;

namespace CEA.Application.Services
{
    public interface IFileService
    {

        public Task PostMultiFileAsync(List<FileUploadDto> fileData);
        public Task PostFileAsync(FileUploadDto fileData);
        public Task DownloadFileById(int fileName);
        public Task<MemoryStream> DownloadPdf(int ejercicio, int folio, int eor);
        public Task<MemoryStream> DownloadWord(OficioDto oficioDto, int? idPuesto = null, int? idDepto = null);
        public Task<MemoryStream> DownloadExcel(List<OficioDtoFunction> oficios);
    }
}
