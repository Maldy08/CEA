using CEA.Application.Interfaces.Repositories;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Application.Services;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Commands.UpdateOficioPdfPath
{

    public record UpdateOficioPdfPathCommand : IRequest<Result<int>>
    {
        public int Ejercicio { get; set; }
        public int Folio { get; set; }
        public int Eor { get; set; }
        public string PdfPath { get; set; } = string.Empty;

        //constructor
        public UpdateOficioPdfPathCommand(int ejercicio, int folio, int eor, string pdfPath)
        {
            Ejercicio = ejercicio;
            Folio = folio;
            Eor = eor;
            PdfPath = pdfPath;
        }

    }

    internal class UpdateOficioPdfPathCommandHandler : IRequestHandler<UpdateOficioPdfPathCommand, Result<int>>
    {

        private readonly IOficioRepository _oficioRepository;

        public UpdateOficioPdfPathCommandHandler(IOficioRepository oficioRepository)
        {
            _oficioRepository = oficioRepository;
        }

        public async Task<Result<int>> Handle(UpdateOficioPdfPathCommand request, CancellationToken cancellationToken)
        {
   
            await _oficioRepository.UpdateOficioPdf(request.Ejercicio, request.Folio, request.Eor, request.PdfPath);
            return Result<int>.Success("Pdf actualizado con exito");
        }
    }
}
