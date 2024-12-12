using CEA.Application.Interfaces.Repositories.Viaticos;
using Gehtsoft.PDFFlow.Builder;
using Gehtsoft.PDFFlow.Models.Enumerations;
using Gehtsoft.PDFFlow.Models.Shared;
using MediatR;
using System.Globalization;

namespace CEA.Application.Features.Viaticos.Generates
{

    public record GenerateInformeActividadesPdf : IRequest<Stream>
    {
        public int Ejercicio { get; set; }
        public int Oficina { get; set; }
        public int Noviat { get; set; }

        public GenerateInformeActividadesPdf(int ejercicio, int oficina, int noviat)
        {
            Ejercicio = ejercicio;
            Oficina = oficina;
            Noviat = noviat;
        }
    }
    internal class GenerateInformeActividadesPdfHandler : IRequestHandler<GenerateInformeActividadesPdf, Stream>
    {
        private readonly IFormatoComisionRepository _formatoComisionRepository;

        public GenerateInformeActividadesPdfHandler(IFormatoComisionRepository formatoComisionRepository)
        {
            _formatoComisionRepository = formatoComisionRepository;
        }

        public async Task<Stream> Handle(GenerateInformeActividadesPdf request, CancellationToken cancellationToken)
        {
            var result = await _formatoComisionRepository.GetFormatoComisionByOficinaEjercicioNoviat(request.Ejercicio, request.Oficina, request.Noviat);
            var myStream = new FileStream("wwwroot/files/InformeActividades.pdf", FileMode.Create);
            var imageDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets");
            var pdfPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "InformeActividades.pdf");
            var logoceaybc = Path.Combine(imageDir, "logobcycea.jpg");

            DocumentBuilder.New()

                //Encabezado
                .AddSection().
                SetSize(Gehtsoft.PDFFlow.Models.Enumerations.PaperSize.Letter)
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
                        .AddParagraphToCell("INFORME DE ACTIVIDADES")

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
                    .SetFontSize(9)
                        .AddCell()
                            .AddParagraph("")
                            .ToRow()
                        .AddCell()
                            .AddParagraph("")
                            .ToRow()
                        .AddCell()
                            .AddParagraph("FECHA DEL INFORME")
                            .ToCell()
                            .AddParagraph("NO. DE OFICIO: ").SetMarginBottom(10)
                            .ToRow()
                        .AddCell()
                            .SetHorizontalAlignment(HorizontalAlignment.Right)
                            .SetVerticalAlignment(VerticalAlignment.Center)
                            .SetPadding(2)
                            .AddParagraph(result?.Fecha.ToString("dd") + " DE " + result?.Fecha.ToString("MMMM", CultureInfo.GetCultureInfo("es-MX")).ToUpper() + " DE " + result?.Fecha.ToString("yyyy"))
                            .ToCell()
                             .AddParagraph("V" + result?.Oficina + "-" + result?.NoViat.ToString() + "/" + result?.Fecha.ToString("yy"))
                             .SetMarginBottom(10)
                                .SetBold()
                            .ToCell()
                .ToSection()


                //Tabla 2
                .AddTable()
                .SetMarginTop(10)
                .SetBorderStroke(Stroke.None)
                .AddColumnToTable("", XUnit.FromPercent(100))

                    .AddRow()
                        .AddCell()
                            .SetVerticalAlignment(VerticalAlignment.Center)
                            .AddParagraph("A QUIEN CORRESPONDA: ")
                            .SetBold()
                            .ToCell()
                            .ToRow()
                   .ToSection()
                 .AddParagraph("POR ESTE MEDIO ME PERMITO ENTREGAR A USTED CON FUNDAMENTO EN LA FRACCIÓN IX DEL ARTÍCULO 70 " +
                 "DE LA LEY GENERAL DE TRANSPARENCIA Y ACCESO A LA INFORMACIÓN PÚBLICA (LGT) PARA EL ESTADO DE " +
                 "BAJA CALIFORNIA, EL INFORME CORRESPONDIENTE A LA COMISION NO. V" + result?.Oficina + "-" + result?.NoViat.ToString() + "/" + result?.Fecha.ToString("yy") + ", EL CUAL SE ME FUE ASIGNADA CON FECHA DEL " + result?.FechaSal.ToString("dd") + " DE " + result?.FechaSal.ToString("MMMM", CultureInfo.GetCultureInfo("es-MX")).ToUpper() + " DEL PRESENTE AÑO.")
                 .SetMarginTop(25)
                 .ToSection()
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
                .AddColumnToTable("", XUnit.FromPercent(100 / 5))
                .AddColumnToTable("", XUnit.FromPercent(100 / 5))
                .AddColumnToTable("", XUnit.FromPercent(100 / 5))
                .AddColumnToTable("", XUnit.FromPercent(100 / 5))
                .AddColumnToTable("", XUnit.FromPercent(100 / 5))

                    .AddRow()
                    .SetFontSize(7)
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
                            .ToRow()
                        .AddCell()
                            .SetHorizontalAlignment(HorizontalAlignment.Center)
                            .SetVerticalAlignment(VerticalAlignment.Center)
                            .AddParagraph("FECHA INICIO")
                            .ToRow()
                        .AddCell()
                            .SetHorizontalAlignment(HorizontalAlignment.Center)
                            .SetVerticalAlignment(VerticalAlignment.Center)
                            .AddParagraph("FECHA TERMINO")
                            .ToRow()
                        .AddCell()
                            .SetHorizontalAlignment(HorizontalAlignment.Center)
                            .SetVerticalAlignment(VerticalAlignment.Center)
                            .AddParagraph("DIAS")
                            .ToCell()
                .ToSection()
                .AddTable().AddColumnToTable("", XUnit.FromPercent(100)).AddRow().AddCell()
                .AddTable()
                .SetBorderStroke(Stroke.None)
                .AddColumnToTable("", XUnit.FromPercent(100 / 5))
                .AddColumnToTable("", XUnit.FromPercent(100 / 5))
                .AddColumnToTable("", XUnit.FromPercent(100 / 5))
                .AddColumnToTable("", XUnit.FromPercent(100 / 5))
                .AddColumnToTable("", XUnit.FromPercent(100 / 5))
                    .AddRow()
                    .SetFontSize(9)
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
                .ToSection()

                .AddTable().AddColumnToTable("", XUnit.FromPercent(100)).AddRow().AddCell()
                .AddTable()
                .SetBorderStroke(Stroke.None)
                .AddColumnToTable("", XUnit.FromPercent(100))

                    .AddRow()
                        .AddCell()
                            .SetHorizontalAlignment(HorizontalAlignment.Center)
                            .SetVerticalAlignment(VerticalAlignment.Center)
                            .SetPadding(2)
                            .SetFontSize(7)

                            .AddParagraph("MOTIVO")
                            .ToRow()
                .ToSection()
                .AddTable().AddColumnToTable("", XUnit.FromPercent(100)).AddRow().AddCell()
                .AddTable()
                .SetBorderStroke(Stroke.None)
                .AddColumnToTable("", XUnit.FromPercent(100))

                    .AddRow()
                        .AddCell()
                            .SetPadding(8)
                            .SetFontSize(9)
                            .AddParagraph(result?.Motivo.PadRight(500).ToUpper())
                               .ToCell()
                .ToSection()

                .AddTable().AddColumnToTable("", XUnit.FromPercent(100)).AddRow().AddCell()
                .AddTable()
                .SetBorderStroke(Stroke.None)
                .AddColumnToTable("", XUnit.FromPercent(100))

                    .AddRow()
                        .AddCell()
                            .SetHorizontalAlignment(HorizontalAlignment.Center)
                            .SetVerticalAlignment(VerticalAlignment.Center)
                            .SetPadding(2)
                            .SetFontSize(7)

                            .AddParagraph("ACTIVIDADES")
                            .ToRow()
                .ToSection()
                .AddTable().AddColumnToTable("", XUnit.FromPercent(100)).AddRow().AddCell()
                .AddTable()
                .SetBorderStroke(Stroke.None)
                .AddColumnToTable("", XUnit.FromPercent(100))

                    .AddRow()
                        .AddCell()
                            .SetPadding(8)
                            .SetFontSize(9)
                            .AddParagraph(result?.InforAct.PadRight(500).ToUpper())
                               .ToCell()
                .ToSection()

                .AddTable().AddColumnToTable("", XUnit.FromPercent(100)).AddRow().AddCell()
                .AddTable()
                .SetBorderStroke(Stroke.None)
                .AddColumnToTable("", XUnit.FromPercent(100))

                    .AddRow()
                        .AddCell()
                            .SetHorizontalAlignment(HorizontalAlignment.Center)
                            .SetVerticalAlignment(VerticalAlignment.Center)
                            .SetPadding(2)
                            .SetFontSize(7)

                            .AddParagraph("CONCLUSIONES Y RESULTADOS")
                            .ToRow()
                .ToSection()
                .AddTable().AddColumnToTable("", XUnit.FromPercent(100)).AddRow().AddCell()
                .AddTable()
                .SetBorderStroke(Stroke.None)
                .AddColumnToTable("", XUnit.FromPercent(100))

                    .AddRow()
                        .AddCell()
                            .SetPadding(8)
                            .SetFontSize(9)
                            .AddParagraph(result?.InforResul.PadRight(500))
                               .ToCell()
                .ToSection()
            //Tabla 4 
            .AddTable()
            .SetBorderStroke(Stroke.None)
            .AddColumnToTable("", XUnit.FromPercent(100))
                .AddRow()
                .SetFontSize(9)
                    .AddCell()
                        .SetHorizontalAlignment(HorizontalAlignment.Center)
                        .SetVerticalAlignment(VerticalAlignment.Center)
                        .AddParagraph("A T E N T A M E N T E")
                        .SetMarginTop(20)
                        .ToCell()
                        .AddParagraph("___________________________________________________________________").SetBold()
                        .SetMarginTop(80)
                        .ToCell().SetPadding(4)
                        .AddParagraphToCell("").AddParagraph(result?.Nombre + " " + result?.Paterno + " " + result?.Materno + " " + "(" + result?.NoEmp + ")").SetBold()
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
            }
            else
            {
                return null;
            }
        }
    }
}
