using AutoMapper;
using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Queries.GetOficiosCppByEjercicioFolio
{
    public record GetOficiosCppByEjercicioFolioQuery : IRequest<Result<List<OficioCppDto>>>
    {
        public int Ejercicio { get; set; }
        public int Folio { get; set; }
        public GetOficiosCppByEjercicioFolioQuery(int ejercicio, int folio)
        {
            Ejercicio = ejercicio;
            Folio = folio;
        }
    }
    internal class GetOficiosCppByEjercicioFolioQueryHandler : IRequestHandler<GetOficiosCppByEjercicioFolioQuery, Result<List<OficioCppDto>>>
    {

        private readonly IOficioFunctions _oficioFunctions;
        private readonly IMapper _mapper;

        public GetOficiosCppByEjercicioFolioQueryHandler(IOficioFunctions oficioFunctions, IMapper mapper)
        {
            _oficioFunctions = oficioFunctions;
            _mapper = mapper;
        }


        public async Task<Result<List<OficioCppDto>>> Handle(GetOficiosCppByEjercicioFolioQuery request, CancellationToken cancellationToken)
        {
            var oficiosCpp = await _oficioFunctions.OficioCpp(request.Ejercicio, request.Folio);
            var oficiosCppDto = _mapper.Map<List<OficioCppDto>>(oficiosCpp);
            return await Result<List<OficioCppDto>>.SuccessAsync(oficiosCppDto);

        }
    }
}
