using AutoMapper;
using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Queries.GetOficiosRelacionados
{

    public record GetOficiosRelacionadosQuery : IRequest<Result<List<OficioDto>>>
    {
        public string relacionoficios { get; set; }

        public GetOficiosRelacionadosQuery(string relacionoficios)
        {
            this.relacionoficios = relacionoficios;
        }
    }
    internal class GetOficiosRelacionadosQueryHandler : IRequestHandler<GetOficiosRelacionadosQuery, Result<List<OficioDto>>>
    {

        private readonly IOficioRepository _oficioRepository;

        public GetOficiosRelacionadosQueryHandler(IOficioRepository oficioRepository)
        {
            _oficioRepository = oficioRepository;
        }

        public async Task<Result<List<OficioDto>>> Handle(GetOficiosRelacionadosQuery request, CancellationToken cancellationToken)
        {
            var oficios = await _oficioRepository.GetOficiosRelacionados(request.relacionoficios);
            return Result<List<OficioDto>>.Success(oficios);
        }
    }
}
