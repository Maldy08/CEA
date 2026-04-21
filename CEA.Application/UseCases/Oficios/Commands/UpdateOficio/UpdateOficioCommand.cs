using CEA.Application.DTOs;
using CEA.Application.Interfaces.Repositories;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Application.Services;
using CEA.Domain.Entities.Oficios;
using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace CEA.Application.UseCases.Oficios.Commands.UpdateOficio
{

    public record UpdateOficioCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public int Ejercicio { get; set; }
        public int Folio { get; set; }
        public int Eor { get; set; }
        public int Tipo { get; set; }
        public string NoOficio { get; set; } = null!;
        public string? Pdfpath { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;//Fecha del documento
        public DateTime FechaCaptura { get; set; } = DateTime.Now;
        public DateTime? FechaAcuse { get; set; } //Opcional, siempre y cuando el oficio pida una fecha de respuesta y unicamente aplica a eor = 1
        public DateTime? FechaLimite { get; set; }  //Opcional, siempre y cuando el oficio pida uan fecha limite
        public string RemDepen { get; set; } = string.Empty;
        public string RemSiglas { get; set; } = string.Empty;
        public string RemNombre { get; set; } = string.Empty;
        public string RemCargo { get; set; } = string.Empty;
        public string? DestDepen { get; set; } = string.Empty;
        public string DestSiglas { get; set; } = string.Empty;
        public string DestNombre { get; set; } = string.Empty;
        public string DestCargo { get; set; } = string.Empty;
        public string Tema { get; set; } = string.Empty;
        public int Estatus { get; set; } = 1;
        public int? Empqentrega { get; set; }
        public string? Relacionoficio { get; set; }
        public int Depto { get; set; }
        public int DeptoRespon { get; set; }

        public int idClasificacion { get; set; }
        // public IFormFile? archivo { get; set; }
    }
    internal class UpdateOficioCommandHandler : IRequestHandler<UpdateOficioCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private readonly IOficioRepository _oficioRepository;

        public UpdateOficioCommandHandler(IUnitOfWork unitOfWork, IFileService fileService, IOficioRepository oficioRepository)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _oficioRepository = oficioRepository;

        }

        public async Task<Result<int>> Handle(UpdateOficioCommand request, CancellationToken cancellationToken)
        {

            var oficioDto = await _oficioRepository.GetOficio(request.Ejercicio, request.Folio, request.Eor);
            if(oficioDto == null  )
            {
                return await Result<int>.FailureAsync($"No se encontró el oficio con el folio {request.Folio}");

            }

            oficioDto.NoOficio = request.NoOficio;
            oficioDto.Fecha = request.Fecha;
            oficioDto.FechaCaptura = request.FechaCaptura;
            oficioDto.FechaAcuse = request.FechaAcuse;
            oficioDto.FechaLimite = request.FechaLimite;
            oficioDto.RemDepen = request.RemDepen;
            oficioDto.RemSiglas = request.RemSiglas;
            oficioDto.RemNombre = request.RemNombre;
            oficioDto.RemCargo = request.RemCargo;
            oficioDto.DestDepen = string.IsNullOrWhiteSpace(request.DestDepen) ? "  " : request.DestDepen;
            oficioDto.DestSiglas = request.DestSiglas;
            oficioDto.DestNombre = request.DestNombre;
            oficioDto.DestCargo = request.DestCargo;
            oficioDto.Tema = request.Tema;
            oficioDto.Estatus = request.Estatus;
            oficioDto.Empqentrega = request.Empqentrega;
            oficioDto.Relacionoficio = request.Relacionoficio;
            oficioDto.Pdfpath = request.Pdfpath;
            oficioDto.Tipo = request.Tipo;
            oficioDto.DeptoRespon = request.DeptoRespon;
            oficioDto.idClasificacion = request.idClasificacion;


            //if (request.archivo != null)
            //{

            //    var fileDto = new FileUploadDto()
            //    {
            //        File = request.archivo!,
            //        FileName = request.Ejercicio + "-" + request.Eor + "-" + request.Folio + ".pdf",
            //        FolderName = request.Eor == 1 ? "oficios-expedidos" : "oficios-recibidos",
            //        FilePath = request.Eor == 1 ? "oficios-expedidos" : "oficios-recibidos"
            //    };

            //    oficioDto.Pdfpath = fileDto.FolderName + "/" + fileDto.FileName;
            //    await _fileService.PostFileAsync(fileDto);
            //}

            try
            {
                await _unitOfWork.Repository<Oficio>().UpdateAsync(oficioDto);

                if (!string.IsNullOrWhiteSpace(request.Relacionoficio))
                {
                    await ActualizarRelacionesBidireccionales(request.Ejercicio, request.Folio, request.Eor, request.Relacionoficio, cancellationToken);
                }

                await _unitOfWork.Save(cancellationToken);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
             return await Result<int>.SuccessAsync("Actualizacion correcta");

        }

        private async Task ActualizarRelacionesBidireccionales(int ejercicioActual, int folioActual, int eorActual, string relacionoficio, CancellationToken cancellationToken)
        {
            var oficiosRelacionados = relacionoficio.Split('|');
            var oficioActualString = $"{ejercicioActual}-{folioActual}-{eorActual}";

            foreach (var oficioRelacionado in oficiosRelacionados)
            {
                var partes = oficioRelacionado.Trim().Split('-');

                if (partes.Length == 3 &&
                    int.TryParse(partes[0], out int ejercicio) &&
                    int.TryParse(partes[1], out int folio) &&
                    int.TryParse(partes[2], out int eor))
                {
                    var oficio = await _oficioRepository.GetOficio(ejercicio, folio, eor);
                    if (oficio != null)
                    {
                        var relacionesActuales = string.IsNullOrWhiteSpace(oficio.Relacionoficio)
                            ? new List<string>()
                            : oficio.Relacionoficio.Split('|').Select(r => r.Trim()).ToList();

                        if (!relacionesActuales.Contains(oficioActualString))
                        {
                            relacionesActuales.Add(oficioActualString);
                            oficio.Relacionoficio = string.Join("|", relacionesActuales);
                            await _unitOfWork.Repository<Oficio>().UpdateAsync(oficio);
                        }
                    }
                }
            }
        }
    }
}
