using CEA.Application.Interfaces.Repositories.Viaticos;
using Gehtsoft.PDFFlow.Builder;
using Gehtsoft.PDFFlow.Models.Enumerations;
using Gehtsoft.PDFFlow.Models.Shared;
using MediatR;
using System.Globalization;


namespace CEA.Application.Features.Viaticos.Generates
{
    public record GenerateFormatoComisionPdf: IRequest<Stream>
    {
        public int Ejercicio { get; set; }
        public int Oficina { get; set; }
        public int Noviat { get; set; }

        public GenerateFormatoComisionPdf(int ejercicio, int oficina, int noviat)
        {
            Ejercicio = ejercicio;
            Oficina = oficina;
            Noviat = noviat;
        }
    }

    internal class GenerateFormatoComisionPdfHandler: IRequestHandler<GenerateFormatoComisionPdf, Stream>
    {
        private readonly IFormatoComisionRepository _formatoComisionRepository;
        public GenerateFormatoComisionPdfHandler(IFormatoComisionRepository formatoComisionRepository)
        {
            _formatoComisionRepository = formatoComisionRepository;
        }


        public async Task<Stream> Handle(GenerateFormatoComisionPdf request, CancellationToken cancellationToken)
        {
            var result = await _formatoComisionRepository.GetFormatoComisionByOficinaEjercicioNoviat(request.Ejercicio, request.Oficina, request.Noviat);
            var myStream = new FileStream("wwwroot/files/FormatoComision.pdf", FileMode.Create);
            //Create a document builder:
            var imageDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets");
            var pdfPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "FormatoComision.pdf");
            var logoceaybc = Path.Combine(imageDir, "logobcycea.jpg");

            DocumentBuilder.New() // Formato Comisión
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
                       .AddParagraphToCell("OFICINA CEA " + result)
                       .AddParagraphToCell("FORMATO DE COMISIÓN")

               .ToSection()
               //Tabla 1
               .AddTable()
               .SetMarginTop(20)
               .SetBorderStroke(Stroke.None)
               .AddColumnToTable("", XUnit.FromPercent(100 / 4))
               .AddColumnToTable("", XUnit.FromPercent(100 / 4))
               .AddColumnToTable("", XUnit.FromPercent(100 / 4))
               .AddColumnToTable("", XUnit.FromPercent(100 / 4))

                   .AddRow()
                       .AddCell()
                           .AddParagraph("")
                           .ToCell()
                           .ToRow()
                       .AddCell()
                           .AddParagraph("")
                           .ToCell()
                           .ToRow()
                       .AddCell()
                           .AddParagraph("")
                           .ToCell()
                           .ToRow()
                       .AddCell()
                           .SetVerticalAlignment(VerticalAlignment.Center)
                           .SetPadding(2)

                           .AddParagraph(result?.Fecha.ToString("dd") + " DE " + result?.Fecha.ToString("MMMM", CultureInfo.GetCultureInfo("es-MX")).ToUpper() + " DE " + result?.Fecha.ToString("yyyy"))
                           .ToCell()
                           .AddParagraph("NO. DE OFICIO: ").SetMarginBottom(10)
                           .AddText("V" + result?.Oficina + "-" + result?.NoViat.ToString() + "/" + result?.Fecha.ToString("yy"))
                               .SetBold()
                           .ToCell()
               .ToSection()


               //Tabla 2
               .AddTable()
               .SetMarginTop(10)
               .SetBorderStroke(Stroke.None)
               .AddColumnToTable("", XUnit.FromPercent(100))

                   .AddRow()
                   .SetBold()
                       .AddCell()
                           .SetVerticalAlignment(VerticalAlignment.Center)
                           .AddParagraph(result?.Nombre + " " + result?.Paterno + " " + result?.Materno)
                           .ToCell()
                           .AddParagraph(result?.DeptoDescripcion)
                           .ToCell()
                           .AddParagraphToCell(result?.DescripcionPuesto).AddParagraph()
                           .ToCell()
                           .AddParagraphToCell("P R E S E N T E .-")
                           .ToRow()
                  .ToSection()
                .AddParagraph("POR MEDIO DE LA PRESENTE SE LE COMUNICA A USTED, QUE DEBERA TRASLADARSE A LA CIUDAD DE " + result?.CdDestino + ", " + result?.EdoDestino +
                " EL DIA " + result?.FechaSal.ToString("dd") + " DE " + result?.FechaSal.ToString("MMMM", CultureInfo.GetCultureInfo("es-MX")).ToUpper() + " DE " + result?.FechaSal.ToString("yyyy") + ", " +
                 result?.Dias.ToString() + " DIA(S) DEL PRESENTE AÑO CON LA FINALIDAD DE:")
                .SetMarginTop(25)
                .ToSection()
                   //Motivo
                   .AddParagraph(result?.Motivo.ToUpper())
                   .SetMarginTop(15)
                   .SetBold()
                .ToSection()
                .AddParagraph("REALIZADO LAS SIGUIENTES ACTIVIDADES")
                .SetMarginTop(35)
                .ToSection()
                   //Motivo
                   .AddParagraph(result?.InforAct.ToUpper().ToUpper())
                   .SetMarginTop(15)
                   .SetBold()
                .ToSection()

               //Tabla 4 
               .AddTable()
               .SetBorderStroke(Stroke.None)
               .AddColumnToTable("", XUnit.FromPercent(100))

                   .AddRow()
                       .AddCell()
                           .SetHorizontalAlignment(HorizontalAlignment.Center)
                           .SetVerticalAlignment(VerticalAlignment.Center)
                           .SetPadding(8)
                           .AddParagraph("___________________________________________________________________").SetBold()
                           .SetMarginTop(150)
                           .ToCell().SetPadding(4)
                           .AddParagraphToCell("").AddParagraph(result?.QuienLoComisiona).SetBold()
                           .ToCell()
                           .AddParagraphToCell(result?.PuestoQuienLoComisiona)
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
               // return new FileStreamResult(ms, "application/pdf") { FileDownloadName = "FormatoComision-" + "V" + result?.Oficina + "-" + result?.NoViat.ToString() + "-" + result?.Fecha.ToString("yy") + ".pdf" };
            }
            else

            {
                return null;
            }
        }
    
    
    }
}
