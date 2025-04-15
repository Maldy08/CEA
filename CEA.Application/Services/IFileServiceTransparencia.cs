using CEA.Application.DTOs;

namespace CEA.Application.Services
{
    public interface IFileServiceTransparencia
    {
        public Task PostMultiFileAsync(List<FileUploadDto> fileData);
    }
}
