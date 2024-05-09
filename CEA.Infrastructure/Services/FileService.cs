using CEA.Application.DTOs;
using CEA.Application.Services;

namespace CEA.Infrastructure.Services
{
    public class FileService : IFileService
    {

        protected readonly string rutaPredeterminada = "C:\\ceatransparencia\\";
        public Task DownloadFileById(int fileName)
        {
           
            throw new NotImplementedException();
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
