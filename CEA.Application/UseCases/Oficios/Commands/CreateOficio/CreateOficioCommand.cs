using AutoMapper;
using CEA.Application.Common.Mappings;
using CEA.Application.DTOs;
using CEA.Application.Interfaces.Repositories;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Application.Services;
using CEA.Application.UseCases.Oficios.Commands.UpdateOficioFolio;
using CEA.Domain.Entities.Oficios;
using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CEA.Application.Features.Oficios.Commands.CreateOficio
{
    public record CreateOficioCommand : IRequest<Result<int>>, IMapFrom<Oficio>
    {
        public int Ejercicio { get; set; }
        public int Folio { get; set; }
        public int Eor { get; set; }
        public int Tipo { get; set; }
        public string NoOficio { get; set; } = null!;
        public string? Pdfpath { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;//Fecha del documento
        public DateTime? FechaCaptura { get; set; } = DateTime.Now;
        public DateTime? FechaAcuse { get; set; } //Opcional, siempre y cuando el oficio pida una fecha de respuesta y unicamente aplica a eor = 1
        public DateTime? FechaLimite { get; set; }  //Opcional, siempre y cuando el oficio pida uan fecha limite
        public string RemDepen { get; set; } = string.Empty;
        public string RemSiglas { get; set; } = string.Empty;
        public string RemNombre { get; set; } = string.Empty;
        public string RemCargo { get; set; } = string.Empty;
        public string DestDepen { get; set; } = string.Empty;
        public string DestSiglas { get; set; } = string.Empty;
        public string DestNombre { get; set; } = string.Empty;
        public string DestCargo { get; set; } = string.Empty;
        public string Tema { get; set; } = string.Empty;
        public int Estatus { get; set; } = 1;
        public int? Empqentrega { get; set; }
        public string? Relacionoficio { get; set; }
        public int Depto { get; set; }
        public int DeptoRespon { get; set; }
        public IFormFile? archivo { get; set; }

    }
    internal class CreateOficioCommandHandler : IRequestHandler<CreateOficioCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOficioRepository _oficioRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IOficioParametroRepository _parametroRepository;
        private readonly IMediator _mediator;

        public CreateOficioCommandHandler(
            IUnitOfWork unitOfWork,
            IOficioRepository oficioRepository,
            IMapper mapper,
            IFileService fileService,
            IOficioParametroRepository parametroRepository,
            IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _oficioRepository = oficioRepository;
            _mapper = mapper;
            _fileService = fileService;
            _parametroRepository = parametroRepository;
            _mediator = mediator;
        }

        public async Task<Result<int>> Handle(CreateOficioCommand request, CancellationToken cancellationToken)
        {
            var oficioParamtro = await _parametroRepository.GetOficioParametroByEjercicio(request.Ejercicio);
            int folio = 0;

            switch (request.Eor)
            {
                case 1:
                    folio = oficioParamtro.NextFEnv;
                    break;
                case 2:
                    folio = oficioParamtro.NextFRec;
                    break;
                case 3:
                    folio = oficioParamtro.NextFXexp;
                    break;
            }

            var oficio = new Oficio
            {
                Ejercicio = request.Ejercicio,
                Folio = folio,
                Eor = request.Eor,
                Tipo = request.Tipo,
                NoOficio = request.NoOficio,
                Pdfpath = request.Pdfpath,
                Fecha = request.Fecha,
                FechaCaptura = request.FechaCaptura,
                FechaAcuse = request.FechaAcuse,
                FechaLimite = request.FechaLimite,
                RemDepen = request.RemDepen,
                RemSiglas = request.RemSiglas,
                RemNombre = request.RemNombre,
                RemCargo = request.RemCargo,
                DestDepen = request.DestDepen,
                DestSiglas = request.DestSiglas,
                DestNombre = request.DestNombre,
                DestCargo = request.DestCargo,
                Tema = request.Tema,
                Estatus = request.Estatus,
                Empqentrega = request.Empqentrega,
                Relacionoficio = request.Relacionoficio,
                Depto = request.Depto,
                DeptoRespon = request.DeptoRespon
            };



            if (request.archivo != null)
            {

                var fileDto = new FileUploadDto()
                {
                    File = request.archivo!,
                    FileName = request.Ejercicio + "-" + request.Eor + "-" + oficio.Folio + ".pdf",
                    FolderName = request.Eor == 1 ? "oficios-expedidos" : "oficios-recibidos",
                    FilePath = request.Eor == 1 ? "oficios-expedidos" : "oficios-recibidos"

                };
                oficio.Pdfpath = fileDto.FolderName + "/" + fileDto.FileName;
                await _fileService.PostFileAsync(fileDto);
            }

            await _unitOfWork.Repository<Oficio>().AddAsync(oficio);

            var command = new UpdateOficioFolioCommand()
            {
                Ejercicio = request.Ejercicio,
                NextFEnv = request.Eor == 1 ? oficioParamtro.NextFEnv + 1 : oficioParamtro.NextFEnv,
                NextFRec = request.Eor == 2 ? oficioParamtro.NextFRec + 1 : oficioParamtro.NextFRec,
                NextFXexp = request.Eor == 3 ? oficioParamtro.NextFXexp + 1 : oficioParamtro.NextFXexp,
            };

            await _mediator.Send(command);

            oficio.AddDomainEvent(new OficioCreatedEvent(oficio));
            try
            {
                await _unitOfWork.Save(cancellationToken);
                return await Result<int>.SuccessAsync(oficio.Folio);
            }
            catch (Exception ex)
            {
                return await Result<int>.FailureAsync(ex.Message);

            }
        }

    }
}
