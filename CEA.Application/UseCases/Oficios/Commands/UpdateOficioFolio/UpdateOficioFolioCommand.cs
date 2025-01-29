

using CEA.Application.Interfaces.Repositories;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Domain.Entities.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Commands.UpdateOficioFolio
{

    public record UpdateOficioFolioCommand : IRequest<Result<int>>
    {
        public int Ejercicio { get; set; }
        public int NextFRec { get; set; }
        public int NextFEnv { get; set; }
        public int NextFXexp { get; set; }
    }
    internal class UpdateOficioFolioCommandHandler : IRequestHandler<UpdateOficioFolioCommand, Result<int>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IOficioParametroRepository _parametroRepository;


        public UpdateOficioFolioCommandHandler(IUnitOfWork unitOfWork, IOficioParametroRepository oficioParametroRepository)
        {

            _unitOfWork = unitOfWork;
            _parametroRepository = oficioParametroRepository;
        }


        public async Task<Result<int>> Handle(UpdateOficioFolioCommand request, CancellationToken cancellationToken)
        {
            var parametros = await _parametroRepository.GetOficioParametroByEjercicio(request.Ejercicio);
            if (parametros == null)
            {
                return await Result<int>.FailureAsync("Error actualizando parametros");
            }

            var oficioParametro = new OficioParametro { 
                Ejercicio = request.Ejercicio,
                NextFEnv = request.NextFEnv,
                NextFXexp = request.NextFXexp,
                NextFRec = request.NextFRec,
                Id = parametros.Id,
            };

            await _unitOfWork.Repository<OficioParametro>().UpdateAsync(oficioParametro);
            await _unitOfWork.Save(cancellationToken);

            return await Result<int>.SuccessAsync("Actualizacion correcta");


            
        }
    }
}
