using CEA.Application.DTOs;
using CEA.Application.Services;
using Microsoft.AspNetCore.Hosting;

namespace CEA.Infrastructure.Services
{
    public class FileService : IFileService
    {

        protected readonly string rutaPredeterminada = "C:\\ceatransparencia\\";
        protected readonly string rutaPredeterminadaOficios = "C:\\ceatransparencia\\oficios\\";

        public Task DownloadFileById(int fileName)
        {

            throw new NotImplementedException();
        }

        public async Task<MemoryStream> DownloadPdf(string path)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), rutaPredeterminadaOficios, path);
            var pdfBytes = await File.ReadAllBytesAsync(filePath);
            var memoryStream = new MemoryStream(pdfBytes);
            return memoryStream;
        }

        public async Task PostFileAsync(FileUploadDto fileData)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), rutaPredeterminadaOficios, fileData.FolderName, fileData.FileName);
            await using var fileStream = new FileStream(filePath, FileMode.Create);
            await fileData.File.CopyToAsync(fileStream);
        }

        public async Task PostMultiFileAsync(List<FileUploadDto> fileData)
        {
            foreach (var file in fileData)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), rutaPredeterminada, file.FolderName, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.File.CopyToAsync(fileStream);
                }
            }
        }

    }
}
