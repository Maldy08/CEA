

using Microsoft.AspNetCore.Http;

namespace CEA.Application.DTOs
{
    public class FileUploadDto
    {
        public IFormFile File { get; set; } = null!;
        public string? FolderName { get; set; } = string.Empty;
        public string? FileName { get; set; } = string.Empty;
        public string? FilePath { get; set; } = string.Empty;
        public int Ejercicio { get; set; }
        public int Folio { get; set; }
        public int Eor { get; set; }
    }
}
