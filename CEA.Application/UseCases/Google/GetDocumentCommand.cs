using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Application.Services;
using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CEA.Application.UseCases.Google
{

    public record GetDocumentCommand : IRequest<FileStreamResult>
    {
        public int Ejercicio { get; set; }
        public int Folio { get; set; }
        public int Eor { get; set; }


        public GetDocumentCommand(int ejercicio, int folio, int eor)
        {
            Ejercicio = ejercicio;
            Folio = folio;
            Eor = eor;
        }


    }
    internal class GetDocumentCommandHandler : IRequestHandler<GetDocumentCommand, FileStreamResult>
    {
        private readonly IOficioRepository _oficioRepository;
        private readonly IGoogleCloudService _googleCloudService;

        public GetDocumentCommandHandler(IOficioRepository oficioRepository, IGoogleCloudService googleCloudService)
        {
            _oficioRepository = oficioRepository;
            _googleCloudService = googleCloudService;
        }


        public async Task<FileStreamResult> Handle(GetDocumentCommand request, CancellationToken cancellationToken)
        {
            var oficio = await _oficioRepository.GetOficioByFolio(request.Ejercicio, request.Eor, request.Folio);


            var ms = _googleCloudService.DriveExportWord(oficio);
            ms.Position = 0;
            var fsr = new FileStreamResult(ms, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            return fsr;


        }
    }


}
