
using CEA.Application.DTOs;
using CEA.Application.Features.Transparencia.Commands.CreateBitacora;
using CEA.Application.Interfaces.Repositories;
using CEA.Application.Services;
using CEA.Domain.Entities.Transparencia;
using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CEA.Application.Features.Transparencia.Commands.UpdateBitacora
{

    public record UpdateBitacoraCommand: IRequest<Result<int>>
    {
        public int IdBitacora { get; set; }
        public int trimestre { get; set; }  
        public int periodo { get; set; }
        public string codigo { get; set; } = string.Empty;

        public int idUsuario { get; set; }

        public List<IFormFile> Archivos { get; set; } = new List<IFormFile>();
    }

    internal class UpdateBitacoraCommandHandler : IRequestHandler<UpdateBitacoraCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public UpdateBitacoraCommandHandler(IUnitOfWork unitOfWork, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public async Task<Result<int>> Handle(UpdateBitacoraCommand request, CancellationToken cancellationToken)
        {
            var bitacora = await _unitOfWork.Repository<BitacoraArchivo>().GetByIdAsync(request.IdBitacora);
            var archivos = new List<FileUploadDto>();

            if (bitacora == null)
            {
                return await Result<int>.FailureAsync($"No se encontró la bitácora con el id {request.IdBitacora}");
            }

            foreach (var file in request.Archivos)
            {
                if (file.Length > 0)
                {
                    var filename = request.trimestre + "-" + request.periodo + "-" + file.FileName;
                    archivos.Add(new FileUploadDto() { File = file, FileName = file.FileName, FolderName = request.codigo });
                    bitacora.RutaArchivo = "C:\\ceatransparencia\\" + request.codigo + "\\" + filename;
                    bitacora.NombreArchivo = filename;
                    bitacora.NombreReporte = request.codigo;
                    bitacora.FechaSubido = DateTime.Now;
                    bitacora.FechaModificado = DateTime.Now;
                    bitacora.Hipervinculo = "http://www.ceabc.gob.mx/ceatransparencia/" + request.codigo + "/" + filename;
                    bitacora.IdUsuario = request.idUsuario;
                    bitacora.Periodo = request.periodo;



                    await _fileService.PostMultiFileAsync(archivos);
                    await _unitOfWork.Repository<BitacoraArchivo>().AddAsync(bitacora);


                }

            }

            bitacora.AddDomainEvent(new BitacoraUpdatedEvent(bitacora));
            await _unitOfWork.Save(cancellationToken);
            return Result<int>.Success(bitacora.Id, $" Bitacora actualizada con folio: ${bitacora.IdBitacora} ");
        }
    }
}
