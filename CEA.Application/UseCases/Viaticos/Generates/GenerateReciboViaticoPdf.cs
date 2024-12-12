using CEA.Application.Interfaces.Repositories.Viaticos;
using Gehtsoft.PDFFlow.Builder;
using Gehtsoft.PDFFlow.Models.Enumerations;
using Gehtsoft.PDFFlow.Models.Shared;
using MediatR;
using System.Globalization;

namespace CEA.Application.Features.Viaticos.Generates
{

    public record GenerateReciboViaticoPdf : IRequest<Stream>
    {
        public int Ejercicio { get; set; }
        public int Oficina { get; set; }
        public int NoViat { get; set; }

        public GenerateReciboViaticoPdf(int ejercicio, int oficina, int noViat)
        {
            Ejercicio = ejercicio;
            Oficina = oficina;
            NoViat = noViat;
        }
    }
    internal class GenerateReciboViaticoPdfHandler : IRequestHandler<GenerateReciboViaticoPdf, Stream>
    {
        private readonly IFormatoComisionRepository _formatoComisionRepository;
        public GenerateReciboViaticoPdfHandler(IFormatoComisionRepository formatoComisionRepository)
        {
            _formatoComisionRepository = formatoComisionRepository;
        }
        public async Task<Stream> Handle(GenerateReciboViaticoPdf request, CancellationToken cancellationToken)
        {
            var result = await _formatoComisionRepository.GetFormatoComisionByOficinaEjercicioNoviat(request.Ejercicio, request.Oficina, request.NoViat);
            var myStream = new FileStream("wwwroot/files/ReciboViatico.pdf", FileMode.Create);
            var imageDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets");
            var pdfPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "ReciboViatico.pdf");
            var logoceaybc = Path.Combine(imageDir, "logobcycea.jpg");

            DocumentBuilder.New() // Recibo Viatcios
            .AddSection().
               SetSize(PaperSize.Letter)
               .SetOrientation(PageOrientation.Portrait)
               .AddFooterToBothPages(10).AddParagraph("AW-CEA").SetFontSize(5).ToSection()
           .AddTable()
               .SetContentRowStyleBorder(borderBuilder => borderBuilder.SetStroke(Stroke.None))
               .AddColumnToTable("", XUnit.FromPercent(40))
               .AddColumnToTable("", XUnit.FromPercent(60))
                   .AddRow()
                       .AddCell()
                       .SetVerticalAlignment(VerticalAlignment.Center)
                       .AddImageToCell(logoceaybc, 288, 70, ScalingMode.UserDefined)
                   .ToRow()
                       .AddCell()
                       .SetHorizontalAlignment(HorizontalAlignment.Center)
                       .SetVerticalAlignment(VerticalAlignment.Center)
                       .SetFontSize(12)
                           .AddParagraph("COMISION ESTATAL DEL AGUA DE BAJA CALIFORNIA")
                           .SetBold()
                       .ToCell()
                       .AddParagraphToCell("OFICINA CEA " + result?.CdOrigen)
                       .AddParagraphToCell("RECIBO DE VIATICOS")

               .ToSection()

               //Tabla 1
               .AddTable()
               .SetMarginTop(20)
               .SetContentRowStyleBorder(borderBuilder => borderBuilder.SetStroke(Stroke.Solid))
               .AddColumnToTable("", XUnit.FromPercent(50))
               .AddColumnToTable("", XUnit.FromPercent(50))
                   .AddRow()
                   .SetFontSize(9)
                       .AddCell()
                           .SetVerticalAlignment(VerticalAlignment.Center)
                           .SetPadding(4)
                           .AddParagraph("BUENO POR: ")
                           .AddText("$" + result?.Importe.ToString("0.00"))
                               .SetBold()
                           .ToRow()
                       .AddCell()
                           .SetVerticalAlignment(VerticalAlignment.Center)
                           .SetPadding(4)
                           .AddParagraph("NO. DE OFICIO: ").SetMarginBottom(10)
                           .AddText("V" + result?.Oficina + "-" + result?.NoViat.ToString() + "/" + result?.Fecha.ToString("yy"))
                               .SetBold()
                           .ToCell()
                           .AddParagraph("FECHA: " + result?.Fecha.ToString("dd") + " DE " + result?.Fecha.ToString("MMMM", CultureInfo.GetCultureInfo("es-MX")).ToUpper() + " DE " + result?.Fecha.ToString("yyyy"))
               .ToSection()

               //Tabla 2
               .AddTable()
               .SetMarginTop(20)
               .SetContentRowStyleBorder(borderBuilder => borderBuilder.SetStroke(Stroke.Solid))
               .AddColumnToTable("", XUnit.FromPercent(30))
               .AddColumnToTable("", XUnit.FromPercent(70))
                   .AddRow()
                   .SetFontSize(9)
                       .AddCell()
                           .SetVerticalAlignment(VerticalAlignment.Center)
                           .SetPadding(10)
                           .AddParagraph("NOMBRE: ").SetMarginBottom(10)
                           .ToCell()
                           .AddParagraphToCell("DEPARTAMENTO:").AddParagraph("").SetMarginBottom(10)
                           .ToCell()
                           .AddParagraphToCell("PUESTO:")
                           .ToRow()
                       .AddCell()
                           .SetVerticalAlignment(VerticalAlignment.Center)
                           .SetPadding(10)
                           .SetBold()
                           .AddParagraph(result?.Nombre + " " + result?.Paterno + " " + result?.Materno + " " + "(" + result?.NoEmp + ")").SetMarginBottom(10)
                           .ToCell()
                           .AddParagraphToCell(result?.DeptoDescripcion).AddParagraph("").SetMarginBottom(10)
                           .ToCell()
                           .AddParagraphToCell(result?.DescripcionPuesto)
               .ToSection()

               //Tabla 3
               .AddTable()
               .SetMarginTop(20)
               .SetBorderStroke(Stroke.None)
               .AddColumnToTable("", XUnit.FromPercent(100))
                   .AddRow()
                       .AddCell()
                           .SetHorizontalAlignment(HorizontalAlignment.Center)
                           .SetVerticalAlignment(VerticalAlignment.Center)
                           .SetPadding(10)
                           .SetFontSize(9)
                           .AddParagraph("DATOS DE COMISIÓN")
                           .ToCell()
               .ToSection()
               .AddTable().AddColumnToTable("", XUnit.FromPercent(100)).AddRow().AddCell()
               .AddTable()
               .SetBorderStroke(Stroke.None)
               .AddColumnToTable("", XUnit.FromPercent(100 / 6))
               .AddColumnToTable("", XUnit.FromPercent(100 / 6))
               .AddColumnToTable("", XUnit.FromPercent(100 / 6))
               .AddColumnToTable("", XUnit.FromPercent(100 / 6))
               .AddColumnToTable("", XUnit.FromPercent(100 / 6))
               .AddColumnToTable("", XUnit.FromPercent(20))

                   .AddRow()
                   .SetFontSize(8)
                       .AddCell()
                           .SetHorizontalAlignment(HorizontalAlignment.Center)
                           .SetVerticalAlignment(VerticalAlignment.Center)
                           .SetPadding(2)
                           .AddParagraph("ORIGEN")
                           .ToRow()
                       .AddCell()
                           .SetHorizontalAlignment(HorizontalAlignment.Center)
                           .SetVerticalAlignment(VerticalAlignment.Center)
                           .AddParagraph("DESTINO")
                           .ToCell()
                           .ToRow()
                       .AddCell()
                           .SetHorizontalAlignment(HorizontalAlignment.Center)
                           .SetVerticalAlignment(VerticalAlignment.Center)
                           .AddParagraph("FECHA INICIO")
                           .ToCell()
                           .ToRow()
                       .AddCell()
                           .SetHorizontalAlignment(HorizontalAlignment.Center)
                           .SetVerticalAlignment(VerticalAlignment.Center)
                           .AddParagraph("FECHA TERMINO")
                           .ToCell()
                           .ToRow()
                       .AddCell()
                           .SetHorizontalAlignment(HorizontalAlignment.Center)
                           .SetVerticalAlignment(VerticalAlignment.Center)
                           .AddParagraph("DIAS")
                           .ToCell()
                           .ToRow()
                       .AddCell()
                           .SetHorizontalAlignment(HorizontalAlignment.Center)
                           .SetVerticalAlignment(VerticalAlignment.Center)
                           .AddParagraph("IMPORTE")
                           .ToCell()
               .ToSection()
               .AddTable().AddColumnToTable("", XUnit.FromPercent(100)).AddRow().AddCell()
               .AddTable()
               .SetBorderStroke(Stroke.None)
               .AddColumnToTable("", XUnit.FromPercent(100 / 6))
               .AddColumnToTable("", XUnit.FromPercent(100 / 6))
               .AddColumnToTable("", XUnit.FromPercent(100 / 6))
               .AddColumnToTable("", XUnit.FromPercent(100 / 6))
               .AddColumnToTable("", XUnit.FromPercent(100 / 6))
               .AddColumnToTable("", XUnit.FromPercent(20))

                   .AddRow()
                   .SetBold()
                   .SetFontSize(8)
                       .AddCell()
                           .SetHorizontalAlignment(HorizontalAlignment.Center)
                           .SetVerticalAlignment(VerticalAlignment.Center)
                           .SetPadding(8)
                           .AddParagraph(result?.CdOrigen)
                           .ToCell()
                           .AddParagraphToCell("").AddParagraph(result?.EdoOrigen)
                           .ToCell()
                           .ToRow()
                       .AddCell()
                           .SetHorizontalAlignment(HorizontalAlignment.Center)
                           .SetVerticalAlignment(VerticalAlignment.Center)
                           .AddParagraph(result?.CdDestino)
                           .ToCell()
                           .AddParagraphToCell("").AddParagraph(result?.EdoDestino)
                           .ToCell()
                           .ToRow()
                       .AddCell()
                           .SetHorizontalAlignment(HorizontalAlignment.Center)
                           .SetVerticalAlignment(VerticalAlignment.Center)
                           .AddParagraph(result?.FechaSal.ToString("dd/M/yyyy"))
                           .ToCell()
                           .ToRow()
                       .AddCell()
                           .SetHorizontalAlignment(HorizontalAlignment.Center)
                           .SetVerticalAlignment(VerticalAlignment.Center)
                           .AddParagraph(result?.FechaReg.ToString("dd/M/yyyy"))
                           .ToCell()
                           .ToRow()
                       .AddCell()
                           .SetHorizontalAlignment(HorizontalAlignment.Center)
                           .SetVerticalAlignment(VerticalAlignment.Center)
                           .AddParagraph(result?.Dias.ToString())
                           .ToCell()
                           .ToRow()
                       .AddCell()
                           .SetHorizontalAlignment(HorizontalAlignment.Center)
                           .SetVerticalAlignment(VerticalAlignment.Center)
                           .AddParagraph("$" + result?.Importe.ToString("0.00"))
                           .ToCell()
               .ToSection()
               .AddTable().AddColumnToTable("", XUnit.FromPercent(100)).AddRow().AddCell()
               .AddTable()
               .SetBorderStroke(Stroke.None)
               .AddColumnToTable("", XUnit.FromPercent(40))
               .AddColumnToTable("", XUnit.FromPercent(60))

                   .AddRow()
                   .SetFontSize(8)
                       .AddCell()
                           .SetHorizontalAlignment(HorizontalAlignment.Center)
                           .SetVerticalAlignment(VerticalAlignment.Center)
                           .SetPadding(2)
                           .AddParagraph("MOTIVO")
                           .ToRow()
                       .AddCell()
                           .SetHorizontalAlignment(HorizontalAlignment.Center)
                           .SetVerticalAlignment(VerticalAlignment.Center)
                           .SetPadding(2)
                           .AddParagraph("ACTIVIDADES")
                           .ToCell()
                           .ToRow()
                  .ToSection()
               .AddTable().AddColumnToTable("", XUnit.FromPercent(100)).AddRow().AddCell()
               .AddTable()
               .SetBorderStroke(Stroke.None)
               .AddColumnToTable("", XUnit.FromPercent(40))
               .AddColumnToTable("", XUnit.FromPercent(60))

                   .AddRow()
                   .SetFontSize(9)
                       .AddCell()
                           .SetPadding(16)
                           .AddParagraph(result?.Motivo.ToUpper())
                           .ToRow()
                       .AddCell()
                           .SetPadding(16)
                           .AddParagraph(result?.InforAct.PadRight(500).ToUpper())
                           .ToCell()
                           .ToRow()
                  .ToSection()

               //Tabla 4 
               .AddTable().SetMarginTop(20).AddColumnToTable("", XUnit.FromPercent(100)).AddRow().AddCell()
               .AddTable()
               .SetBorderStroke(Stroke.None)
               .AddColumnToTable("", XUnit.FromPercent(50))
               .AddColumnToTable("", XUnit.FromPercent(50))

                   .AddRow()
                   .SetFontSize(8)
                       .AddCell()
                           .SetHorizontalAlignment(HorizontalAlignment.Center)
                           .SetVerticalAlignment(VerticalAlignment.Center)
                           .SetPadding(8)
                           .AddParagraph("AUTORIZO").SetMarginBottom(50)
                           .ToCell()
                           .AddParagraph("___________________________________________").SetBold()
                           .ToCell().SetPadding(4)
                           .AddParagraphToCell("").AddParagraph(result?.DirectorAdministrativo).SetBold()
                           .ToCell()
                           .AddParagraphToCell("DIRECTOR ADMINISTRATIVO")
                           .ToRow()
                       .AddCell()
                           .SetHorizontalAlignment(HorizontalAlignment.Center)
                           .SetVerticalAlignment(VerticalAlignment.Center)
                           .SetPadding(8)
                           .AddParagraph("RECIBÍ").SetMarginBottom(50)
                           .ToCell()
                           .AddParagraph("___________________________________________").SetBold()
                           .ToCell().SetPadding(4)
                           .AddParagraphToCell("").AddParagraph(result?.Nombre + " " + result?.Paterno + " " + result?.Materno).SetBold()
                           .ToCell()
                           .AddParagraphToCell(result?.DescripcionPuesto)
                           .ToRow()

                  .ToSection()
           //Build a file:
           .ToDocument().Build(myStream);
            myStream.Close();

            if (File.Exists(pdfPath))
            {
                var abc = File.ReadAllBytes(pdfPath);
                File.WriteAllBytes(pdfPath, abc);
                return new MemoryStream(abc);
          
                //return new FileStreamResult(ms, "application/pdf") { FileDownloadName = "ReciboViatico-" + "V" + result?.Oficina + "-" + result?.NoViat.ToString() + "-" + result?.Fecha.ToString("yy") + ".pdf" };
            }
            else
            {
                return null;
            }
        }
    }
}
