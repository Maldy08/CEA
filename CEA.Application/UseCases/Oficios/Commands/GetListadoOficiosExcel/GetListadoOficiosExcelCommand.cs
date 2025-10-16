using CEA.Application.Services;
using CEA.Application.UseCases.Oficios.Queries.GetListadoOficiosFunctionToExcel;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Commands.GetListadoOficiosExcel
{

    public record GetListadoOficiosExcelCommand : IRequest<MemoryStream>
    {
        public int Ejercicio { get; set; }
        public int IdEmpleado { get; set; }
        public GetListadoOficiosExcelCommand(int ejercicio, int idEmpleado)
        {
            Ejercicio = ejercicio;
            IdEmpleado = idEmpleado;
        }
    }
    internal class GetListadoOficiosExcelCommandHandler : IRequestHandler<GetListadoOficiosExcelCommand, MemoryStream>
    {
        private readonly IFileService _fileService;
        private readonly IMediator _mediator;


        public GetListadoOficiosExcelCommandHandler(IFileService fileService, IMediator mediator)
        {
            _fileService = fileService;
            _mediator = mediator;
        }
        public async Task<MemoryStream> Handle(GetListadoOficiosExcelCommand request, CancellationToken cancellationToken)
        {
            var entidades = await _mediator.Send(new GetListadoOficiosFunctionToExcelQuery(request.Ejercicio, request.IdEmpleado));
            var file = await _fileService.DownloadExcel(entidades.Data);
            return file;

        }
    }
}
