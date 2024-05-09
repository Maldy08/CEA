using AutoMapper;
using CEA.Application.Common.Mappings;
using CEA.Application.DTOs;
using CEA.Application.DTOs.Transparencia;
using CEA.Application.Interfaces.Repositories;
using CEA.Application.Services;
using CEA.Domain.Entities.Transparencia;
using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace CEA.Application.Features.Transparencia.Commands.CreateBitacora
{
    public record CreateBitacoraCommand : IRequest<Result<int>>, IMapFrom<BitacoraArchivoDto>
    {
        public int idBitacora { get; set; }
        public string codigo { get; set; } = string.Empty;
        public int idUsuario { get; set; }
        public int trimestre { get; set; }
        public int periodo { get; set; }
        public List<IFormFile> archivos { get; set; } = new List<IFormFile>();

    }
    internal class CreateBitacoraCommandHandler : IRequestHandler<CreateBitacoraCommand, Result<int>>
    {
        private readonly IUnitOfWorkSQL _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;


        public CreateBitacoraCommandHandler(IUnitOfWorkSQL unitOfWork, IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task<Result<int>> Handle(CreateBitacoraCommand request, CancellationToken cancellationToken)
        {
            var archivos = new List<FileUploadDto>();
            var bitacora = new BitacoraArchivo();

            foreach (var file in request.archivos)
            {
                if (file.Length > 0)
                {
                    var filename = request.trimestre + "-" + request.periodo + "-" + file.FileName ;
                    archivos.Add(new FileUploadDto() { File = file, FileName = file.FileName });
                    bitacora.RutaArchivo = request.codigo;
                    bitacora.NombreArchivo = file.FileName;
                    bitacora.NombreReporte = request.codigo;
                    bitacora.FechaSubido = DateTime.Now;
                    bitacora.FechaModificado = DateTime.Now;
                    bitacora.Hipervinculo = "http://www.ceabc.gob.mx/ceatransparencia/" + request.codigo + "/" + filename;
                    bitacora.IdUsuario = request.idUsuario;
                    bitacora.Periodo = request.periodo;
                   
                }

                await _fileService.PostMultiFileAsync(archivos);
                await _unitOfWork.Repository<BitacoraArchivo>().AddAsync(bitacora);
            }
           
            bitacora.AddDomainEvent(new BitacoraCreatedEvent(bitacora));
            await _unitOfWork.Save(cancellationToken);
            return Result<int>.Success(bitacora.Id, $" Bitacora generada con folio: ${bitacora.IdBitacora} ");
        }

    }
}
