using CEA.Application.DTOs;
using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Application.Services;
using ClosedXML.Excel;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Text.RegularExpressions;
using Xceed.Words.NET;

namespace CEA.Infrastructure.Services
{
    public class FileService : IFileService
    {

        //protected readonly string rutaPredeterminadaOficios = "C:\\ceatransparencia\\oficios\\";
        private readonly string _rutaPredeterminadaOficios;
        private readonly string _rutaPredeterminadaOficiosPlantilla = "C:\\SISCO\\";
        private readonly ILogger<FileService> _logger;
        private readonly IDeptoRepository _deptoRepository;
        private readonly IOficioFunctions _oficioFunctions;

        public FileService(IOptions<FileServiceOptions> options, ILogger<FileService> logger, IDeptoRepository deptoRepository, IOficioFunctions oficioFunctions)
        {
            _rutaPredeterminadaOficios = options.Value.DefaultPath;
            _logger = logger;
            _deptoRepository = deptoRepository;
            _oficioFunctions = oficioFunctions;
        }

        public int ExtractYearFromPath(string path)
        {
            // Define una expresión regular para encontrar el año
            var regex = new Regex(@"\b\d{4}\b");

            // Encuentra la coincidencia en la cadena
            var match = regex.Match(path);

            if (match.Success)
            {
                // Convierte la coincidencia a entero
                return int.Parse(match.Value);
            }

            throw new ArgumentException("No se encontró un año en la cadena proporcionada.");
        }

        public Task DownloadFileById(int fileName)
        {

            throw new NotImplementedException();
        }

        public async Task<MemoryStream> DownloadPdf(int ejercicio, int folio, int eor)
        {
            var defaultPath = _rutaPredeterminadaOficios.Replace("{EJERCICIO}", ejercicio.ToString());
            string subFolder = eor switch
            {
                1 => "OFICIOS-EXPEDIDOS",
                2 => "OFICIOS-RECIBIDOS",
                3 => "OFICIOS-EXPEDIDOS",
                4 => "OFICIOS-RECIBIDOS",
                _ => throw new ArgumentException("Valor de EOR no válido")
            };
            var path = Path.Combine(defaultPath, subFolder, $"{ejercicio}-{eor}-{folio}.pdf");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), path);
            var pdfBytes = await File.ReadAllBytesAsync(filePath);
            var memoryStream = new MemoryStream(pdfBytes);
            return memoryStream;
        }

        public async Task<MemoryStream> DownloadWord(OficioDto oficio)
        {
            try
            {
                var depto = await _deptoRepository.GetSeproaByIdAsync(oficio.Depto);
                var filePath = Path.Combine(_rutaPredeterminadaOficiosPlantilla, "PlantillaCea.docx");
                _logger.LogInformation($"Ruta de la plantilla: {filePath}");

                if (!File.Exists(filePath))
                {
                    // Registra un mensaje de error si el archivo no existe
                    _logger.LogError($"La plantilla no existe en la ruta: {filePath}");
                    SystemException ex = new SystemException($"La plantilla no exiso c1te en la ruta: {filePath}");
                    throw ex;
                }

                using (var document = DocX.Load(filePath))
                {
                    var oficiosCPP = await _oficioFunctions.OficioCpp(oficio.Ejercicio, oficio.Folio);
                    var oficiosCppTexto = oficiosCPP != null && oficiosCPP.Any()
                            ? string.Join(Environment.NewLine, oficiosCPP.Select(o => o.Puesto))
                                : " ";

                    var reemplazos = new Dictionary<string, string>
                    {
                       { "{{DEPENDENCIA}}", oficio.Tipo == 1 ? "COMISION ESTATAL DEL AGUA DE BAJA CALIFORNIA" : "SECRETARÍA PARA EL MANEJO, SANEAMIENTO Y PROTECCIÓN DEL AGUA" },
                       { "{{SECCION}}", depto.Descripcion },
                       { "{{OFICIO}}", oficio.NoOficio },
                       { "{{DEST_RESPONSABLE}}", oficio.DestNombre },
                       { "{{DEST_PUESTO}}", oficio.DestCargo },
                       { "{{DEST_SIGLAS}}", oficio.DestDepen },
                       { "{{ASUNTO}}", oficio.Tema },
                       { "{{FECHA}}", DateTime.Now.ToString("dd 'de' MMMM 'del' yyyy", new CultureInfo("es-ES")) },
                       { "{{REM_RESPONSABLE}}", oficio.RemNombre == "VÍCTOR DANIEL AMADOR BARRAGÁN" ? "DR. " + oficio.RemNombre : oficio.RemNombre },
                       { "{{REM_PUESTO}}", oficio.RemCargo },
                       { "{{REM_SIGLAS}}", oficio.RemDepen },
                       { "{{CCP_LIST}}", oficiosCppTexto }
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
                _logger.LogError($"Error: {ex.Message}");
                throw;
            }
        }


        public async Task PostFileAsync(FileUploadDto fileData)
        {
            var ejercicio = fileData.Ejercicio; // O el valor que corresponda
            var defaultPath = _rutaPredeterminadaOficios.Replace("{EJERCICIO}", ejercicio.ToString());

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), defaultPath, fileData.FolderName, fileData.FileName);
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

        public Task<MemoryStream> DownloadExcel(List<OficioDtoFunction> oficios)
        {
            try
            {
                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Listado de oficios");
               
                var headers = new List<string>
                {
                    "Ejercicio", "Folio", "No. Oficio", "Fecha Oficio", "Tipo", "Tema", "Destinatario Nombre",
                    "Destinatario Cargo", "Destinatario Dependencia", "Remitente Nombre", "Remitente Cargo",
                    "Remitente Dependencia", "Depto", "EOR", "Fecha Recepción", "Estatus",
                    "Observaciones"
                };
                for (int i = 0; i < headers.Count; i++)
                {
                    worksheet.Cell(1, i + 1).Value = headers[i];
                    worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                }
               
                for (int i = 0; i < oficios.Count; i++)
                {
                    var oficio = oficios[i];
                    worksheet.Cell(i + 2, 1).Value = oficio.Ejercicio;
                    worksheet.Cell(i + 2, 2).Value = oficio.Folio;
                    worksheet.Cell(i + 2, 3).Value = oficio.NoOficio;
                    worksheet.Cell(i + 2, 4).Value = oficio.Fecha.ToString("dd/MM/yyyy");
                    worksheet.Cell(i + 2, 5).Value = oficio.Tipo;
                    worksheet.Cell(i + 2, 6).Value = oficio.Tema;
                    worksheet.Cell(i + 2, 7).Value = oficio.DestNombre;
                    worksheet.Cell(i + 2, 8).Value = oficio.DestCargo;
                    worksheet.Cell(i + 2, 9).Value = oficio.DestDepen;
                    worksheet.Cell(i + 2, 10).Value = oficio.RemNombre;
                    worksheet.Cell(i + 2, 11).Value = oficio.RemCargo;
                    worksheet.Cell(i + 2, 12).Value = oficio.RemDepen;
                    worksheet.Cell(i + 2, 13).Value = oficio.Depto;
                    worksheet.Cell(i + 2, 14).Value = oficio.Eor == 1 ? "EXPEDIDO" : "POR EXPEDIR";
                    worksheet.Cell(i + 2, 15).Value = oficio.FechaAcuse?.ToString("dd/MM/yyyy") ?? "";
                    worksheet.Cell(i + 2, 16).Value = oficio.Estatus ?? "";
                    worksheet.Cell(i + 2, 17).Value = oficio.Observaciones ?? "";

                }

                worksheet.Columns().AdjustToContents();

                var memoryStream = new MemoryStream();
                workbook.SaveAs(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                return Task.FromResult(memoryStream);
            }


            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                throw;
            }
        }
    }
}
