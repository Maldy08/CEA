

using CEA.Application.DTOs;
using CEA.Application.DTOs.Oficios;
using Microsoft.AspNetCore.Http;

namespace CEA.Application.Services
{
    public interface IFileService
    {
        
        public Task PostMultiFileAsync(List<FileUploadDto> fileData);
        public Task PostFileAsync(FileUploadDto fileData);
        public Task DownloadFileById(int fileName);
        public Task<MemoryStream> DownloadPdf(int ejercicio,int folio,int eor);
        
        //metodo para regresar un archivo de word
        public Task<MemoryStream> DownloadWord(OficioDto oficioDto);
    }
}
