using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Application.Services;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Commands.GetPdfOficio
{
    public record GetPdfOficioCommand : IRequest<MemoryStream>
    {
        public int Ejercicio { get; init; }
        public int Folio { get; init; }
        public int Eor { get; init; }

        public GetPdfOficioCommand(int ejercicio, int folio, int eor)
        {
            Ejercicio = ejercicio;
            Folio = folio;
            Eor = eor;
        }
    }

    internal class GetPdfOficioCommandHandler : IRequestHandler<GetPdfOficioCommand, MemoryStream>
    {

        private readonly IOficioRepository _oficioRepository;
        private readonly IFileService _fileService;
        public GetPdfOficioCommandHandler(IOficioRepository oficioRepository, IFileService fileService)
        {
            _oficioRepository = oficioRepository;
            _fileService = fileService;
        }

        public async Task<MemoryStream> Handle(GetPdfOficioCommand request, CancellationToken cancellationToken)
        {
            //var oficio = await _oficioRepository.GetOficio(request.Ejercicio, request.Folio, request.Eor);
            //if (oficio == null)
            //{
            //    throw new FileNotFoundException("No se encontró el oficio");
            //}

            var memoryStream = await _fileService.DownloadPdf(request.Ejercicio,request.Folio,request.Eor);
            if (memoryStream == null)
            {
                throw new FileNotFoundException("No se pudo descargar el PDF");
            }

            return memoryStream;
        }
    }
}
