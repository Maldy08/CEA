using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Application.Services;
using MediatR;

namespace CEA.Application.UseCases.Google
{

    public record GetDocumentCommand : IRequest<MemoryStream>
    {
        public int Ejercicio { get; set; }
        public int Folio { get; set; }
        public int Eor { get; set; }
        public int? IdPuesto { get; set; }
        public int? IdDepto { get; set; }


        public GetDocumentCommand(int ejercicio, int folio, int eor, int? idPuesto = null, int? idDepto = null)
        {
            Ejercicio = ejercicio;
            Folio = folio;
            Eor = eor;
            IdPuesto = idPuesto;
            IdDepto = idDepto;
        }


    }
    internal class GetDocumentCommandHandler : IRequestHandler<GetDocumentCommand, MemoryStream>
    {
        private readonly IOficioRepository _oficioRepository;
        private readonly IFileService _fileService;
        // private readonly IGoogleCloudService _googleCloudService;


        public GetDocumentCommandHandler(IOficioRepository oficioRepository, IFileService fileService)
        {
            _oficioRepository = oficioRepository;
            _fileService = fileService;
            // _googleCloudService = googleCloudService;
        }



        public async Task<MemoryStream> Handle(GetDocumentCommand request, CancellationToken cancellationToken)
        {
            var oficio = await _oficioRepository.GetOficioByFolio(request.Ejercicio, request.Eor, request.Folio);
            if (oficio == null)
            {
                throw new Exception("No se encontró el oficio");
            }

            var file = await _fileService.DownloadWord(oficio, request.IdPuesto, request.IdDepto);
            if (file == null)
            {
                throw new Exception("No se encontró el archivo");
            }
            return file;

        }
    }


}
