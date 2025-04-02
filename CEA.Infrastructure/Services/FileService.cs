using CEA.Application.DTOs;
using CEA.Application.Services;
using Microsoft.Extensions.Options;
using System.Globalization;
using Xceed.Words.NET;

namespace CEA.Infrastructure.Services
{
    public class FileService : IFileService
    {

        //protected readonly string rutaPredeterminadaOficios = "C:\\ceatransparencia\\oficios\\";
        private readonly string _rutaPredeterminadaOficios;

        public FileService(IOptions<FileServiceOptions> options)
        {
            _rutaPredeterminadaOficios = options.Value.DefaultPath;
        }

        public Task DownloadFileById(int fileName)
        {

            throw new NotImplementedException();
        }

        public async Task<MemoryStream> DownloadPdf(string path)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), _rutaPredeterminadaOficios, path);
            var pdfBytes = await File.ReadAllBytesAsync(filePath);
            var memoryStream = new MemoryStream(pdfBytes);
            return memoryStream;
        }

        public async Task<MemoryStream> DownloadWord()
        {
            try
            {
                var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var filePath = Path.Combine(desktopPath, "PlantillaCea.docx");


                if (!File.Exists(filePath))
                {
                    return null;
                }


                using (var document = DocX.Load(filePath))
                {

                    var reemplazos = new Dictionary<string, string>
                    {
                        { "{{DEPENDENCIA}}", "CEA" },
                        { "{{SECCION}}", "NO SE " },
                        { "{{OFICIO}}", "12.31-2025" },
                        { "{{RESPONSABLE}}", "ALEJANDRO RAMOS" },
                        { "{{PUESTO}}", "INFORMATICA" },
                        { "{{ASUNTO}}", "TESTEAR DOCX" },
                        { "{{FECHA}}", DateTime.Now.ToString("dd 'de' MMMM 'del' yyyy", new CultureInfo("es-ES")) }
                    };


                    foreach (var item in reemplazos)
                    {
                        document.ReplaceText(item.Key, item.Value);
                    }

                    var memoryStream = new MemoryStream();
                    document.SaveAs(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    return memoryStream;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }


        public async Task PostFileAsync(FileUploadDto fileData)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), _rutaPredeterminadaOficios, fileData.FolderName, fileData.FileName);
            await using var fileStream = new FileStream(filePath, FileMode.Create);
            await fileData.File.CopyToAsync(fileStream);
        }

        public async Task PostMultiFileAsync(List<FileUploadDto> fileData)
        {
            foreach (var file in fileData)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), _rutaPredeterminadaOficios, file.FolderName, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.File.CopyToAsync(fileStream);
                }
            }
        }

    }
}
