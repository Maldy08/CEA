

using CEA.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace CEA.Application.Services
{
    public interface IFileService
    {
        
        public Task PostMultiFileAsync(List<FileUploadDto> fileData);
        public Task PostFileAsync(FileUploadDto fileData);
        public Task DownloadFileById(int fileName);
        public Task<MemoryStream> DownloadPdf(string path);
    }
}
