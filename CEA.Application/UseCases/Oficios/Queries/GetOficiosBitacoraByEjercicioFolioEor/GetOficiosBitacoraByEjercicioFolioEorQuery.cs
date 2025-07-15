

using AutoMapper;
using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Application.UseCases.GetUserByIdEmpleado;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.Features.Oficios.Queries.GetOficiosBitacoraByEjercicioFolioEor
{

    public record GetOficiosBitacoraByEjercicioFolioEorQuery : IRequest<Result<List<OficioBitacoraDto>>>
    {
        public int Ejercicio { get; set; }
        public int Folio { get; set; }
        public int Eor { get; set; }

        public GetOficiosBitacoraByEjercicioFolioEorQuery(int ejercicio, int folio, int eor)
        {
            Ejercicio = ejercicio;
            Folio = folio;
            Eor = eor;
        }
    }
    internal class GetOficiosBitacoraByEjercicioFolioEorQueryHandler : IRequestHandler<GetOficiosBitacoraByEjercicioFolioEorQuery, Result<List<OficioBitacoraDto>>>
    {
        private readonly IOficioBitacoraRepository _oficioBitacoraRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetOficiosBitacoraByEjercicioFolioEorQueryHandler(IOficioBitacoraRepository oficioBitacoraRepository, IMapper mapper, IMediator mediator)
        {
            _oficioBitacoraRepository = oficioBitacoraRepository;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<Result<List<OficioBitacoraDto>>> Handle(GetOficiosBitacoraByEjercicioFolioEorQuery request, CancellationToken cancellationToken)
        {
            var entities = await _oficioBitacoraRepository.GetOficioBitacoraByEjercicioFolioEor(request.Ejercicio, request.Folio, request.Eor);
            foreach (var entity in entities)
            {
                var user = await _mediator.Send(new GetUserByIdEmpleadoQuery(entity.IdEmpleado));
                entity.Usuario = user.Data.Nombre.Substring(0, 1) + user.Data.Paterno + user.Data.Materno.Substring(0, 1);

            }
            var mappedEntities = _mapper.Map<List<OficioBitacoraDto>>(entities);
            return await Result<List<OficioBitacoraDto>>.SuccessAsync(mappedEntities);
        }
    }
}
