using AutoMapper;
using CEA.Application.Common.Helpers;
using CEA.Application.Common.Mappings;
using CEA.Application.Interfaces.Repositories;
using CEA.Application.Interfaces.Repositories.Viaticos;
using CEA.Domain.Constants;
using CEA.Domain.Entities.Viaticos;
using CEA.Shared.Interfaces;
using MediatR;


namespace CEA.Application.Features.Viaticos.Commands.CreateViatico
{
    public record CreateViaticoCommand: IRequest<Result<int>>, IMapFrom<Viatico>
    {
        public int Oficina { get; set; }
        public int Ejercicio { get; set; }
        public int NoViat { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int NoEmp { get; set; }
        public int OrigenId { get; set; }
        public int DestinoId { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public DateTime FechaSal { get; set; } = DateTime.Now;
        public DateTime FechaReg { get; set; } = DateTime.Now;
        public int Dias { get; set; }
        public DateTime InforFecha { get; set; } = DateTime.Now;
        public string InforAct { get; set; } = string.Empty;
        public string? Nota { get; set; } = string.Empty;
        public int Estatus { get; set; }
        public DateTime? FechaMod { get; set; } = DateTime.Now;
        public int? Pol { get; set; } = 0;
        public int? PolMes { get; set; } = 0;
        public int? Caja { get; set; } = 0;
        public int? CajaVale { get; set; } = 0;
        public int? CajaRepo { get; set; } = 0;
        public int NoEmpCrea { get; set; }
        public string InforResul { get; set; } = string.Empty;

    }

    internal class CreateViaticoCommandHandler : IRequestHandler<CreateViaticoCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IViaticoRepository _viaticoRepository;
        private readonly IMapper _mapper;

        public CreateViaticoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IViaticoRepository viaticoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _viaticoRepository = viaticoRepository;
        }
        public async Task<Result<int>> Handle(CreateViaticoCommand request, CancellationToken cancellationToken)
        {
            var numviatico = await _viaticoRepository.GetNoViat(request.Ejercicio, request.Oficina);
            var viatico = new Viatico()
            {
                Oficina = request.Oficina,
                Ejercicio = request.Ejercicio,
                NoViat = numviatico,
                Fecha = request.Fecha,
                NoEmp = request.NoEmp,
                OrigenId = request.OrigenId,
                DestinoId = request.DestinoId,
                Motivo = request.Motivo,
                FechaSal = request.FechaSal,
                FechaReg = request.FechaReg,
                Dias = request.Dias,
                InforFecha = request.InforFecha,
                InforAct = request.InforAct,
                Nota = request.Nota,
                Estatus = request.Estatus,
                FechaMod = request.FechaMod,
                Pol = request.Pol,
                PolMes = request.PolMes,
                Caja = request.Caja,
                CajaVale = request.CajaVale,
                CajaRepo = request.CajaRepo,
                NoEmpCrea = request.NoEmpCrea,
                InforResul = request.InforResul
            };


            //bool fueraEstado = request.OrigenId != request.DestinoId;

            //var importe = ViaticoImportePorDias.CalcularImportePorDias(request.Dias, ViaticoImporte.ImporteViaticoEmpleadoDentroEstado);

            //var viaticoPart =  new ViaticoPart()
            //{
            //    Oficina = request.Oficina,
            //    Ejercicio = request.Ejercicio,
            //    NoViat = numviatico,
            //    Partida = fueraEstado ? 37502 : 37501,
            //    Importe =  importe
            //};

            await _unitOfWork.Repository<Viatico>().AddAsync(viatico);
            //await _unitOfWork.Repository<ViaticoPart>().AddAsync(viaticoPart);
            viatico.AddDomainEvent(new ViaticoCreatedEvent(viatico));

            await _unitOfWork.Save(cancellationToken);
            return await Result<int>.SuccessAsync(viatico.NoViat, $"Viatico generado con folio: { viatico.NoViat}");
        }
    }
}
