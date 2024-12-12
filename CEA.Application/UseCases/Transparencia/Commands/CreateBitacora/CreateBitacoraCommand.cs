using CEA.Application.Common.Mappings;
using CEA.Application.DTOs;
using CEA.Application.DTOs.Transparencia;
using CEA.Application.Interfaces.Repositories;
using CEA.Application.Interfaces.Repositories.Transparencia;
using CEA.Application.Services;
using CEA.Domain.Entities.Transparencia;
using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace CEA.Application.Features.Transparencia.Commands.CreateBitacora
{
    public record CreateBitacoraCommand : IRequest<Result<List<BitacoraArchivoDto>>>, IMapFrom<BitacoraArchivoDto>
    {
        public int idBitacora { get; set; }
        public string codigo { get; set; } = string.Empty;
        public int idUsuario { get; set; }
        public int trimestre { get; set; }
        public int periodo { get; set; }
        public List<IFormFile> archivos { get; set; } = new List<IFormFile>();

    }
    internal class CreateBitacoraCommandHandler : IRequestHandler<CreateBitacoraCommand, Result<List<BitacoraArchivoDto>>>
    {
        private readonly IUnitOfWorkSQL _unitOfWork;
        private readonly IFileService _fileService;
        private readonly ITransparenciaFormatoRepository _transparenciaFormatoRepository;

        public CreateBitacoraCommandHandler(IUnitOfWorkSQL unitOfWork, IFileService fileService, ITransparenciaFormatoRepository transparenciaFormatoRepository)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _transparenciaFormatoRepository = transparenciaFormatoRepository;
        }
        public async Task<Result<List<BitacoraArchivoDto>>> Handle(CreateBitacoraCommand request, CancellationToken cancellationToken)
        {
            var archivos = new List<FileUploadDto>();
            var bitacora = new List<BitacoraArchivo>();
            var bitacoraArchivoDto = new List<BitacoraArchivoDto>();

            foreach (var file in request.archivos)
            {
                if (file.Length > 0)
                {
                    var filename = request.trimestre + "-" + request.periodo + "-" + file.FileName ;
                    var codigo = await _transparenciaFormatoRepository.GetCodigoByFormato(request.codigo);
                    
                    archivos.Add(new FileUploadDto() { File = file, FileName = filename, FolderName = codigo });
                    var data = new BitacoraArchivo()
                    {
                        RutaArchivo = "C:\\ceatransparencia\\" + codigo + "\\" + filename,
                        NombreArchivo = filename,
                        NombreReporte = request.codigo,
                        FechaSubido = DateTime.Now,
                        FechaModificado = DateTime.Now,
                        Hipervinculo = "http://www.ceabc.gob.mx/ceatransparencia/" + codigo + "/" + filename,
                        IdUsuario = request.idUsuario,
                        Periodo = request.periodo,
                    };
                    bitacora.Add(data);
                    bitacoraArchivoDto.Add(new BitacoraArchivoDto()
                    {
                        archivos = request.archivos,
                        codigo = request.codigo,
                        idBitacora = request.idBitacora,
                        idUsuario = request.idUsuario,
                        periodo = request.periodo,
                        trimestre = request.trimestre,
                        Hipervinculo = data.Hipervinculo

                    });
                    
                    await _fileService.PostMultiFileAsync(archivos);
                    await _unitOfWork.Repository<BitacoraArchivo>().AddAsync(data);
  

                }
            }
           
            await _unitOfWork.Save(cancellationToken);
            return Result<List<BitacoraArchivoDto>>.Success(bitacoraArchivoDto);
        }

    }
}
