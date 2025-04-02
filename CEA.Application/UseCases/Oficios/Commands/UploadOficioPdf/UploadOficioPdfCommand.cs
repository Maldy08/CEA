using CEA.Application.Common.Mappings;
using CEA.Application.DTOs;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Application.Services;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Commands.UploadOficioPdf
{

    public record UploadOficioPdfCommand : IRequest<Result<int>>, IMapFrom<FileUploadDto>
    {
        public FileUploadDto FileUploadDto { get; set; } = new FileUploadDto();
    }

    internal class UploadOficioPdfCommandHandler : IRequestHandler<UploadOficioPdfCommand, Result<int>>
    {

        private readonly IFileService _fileService;
        private readonly IOficioRepository _oficioRepository;

        public UploadOficioPdfCommandHandler(IFileService fileService, IOficioRepository oficioRepository)
        {
            _fileService = fileService;
            _oficioRepository = oficioRepository;
        }


        public async Task<Result<int>> Handle(UploadOficioPdfCommand request, CancellationToken cancellationToken)
        {
            if (request.FileUploadDto.File == null)
            {
                return Result<int>.Failure("No se ha seleccionado un archivo");
            }

            var fileDto = new FileUploadDto()
            {
                File = request.FileUploadDto.File!,
                FileName = request.FileUploadDto.Ejercicio + "-" + request.FileUploadDto.Eor + "-" + request.FileUploadDto.Folio + ".pdf",
                FolderName = request.FileUploadDto.Eor == 1 ? "OFICIOS-EXPEDIDOS" : "OFICIOS-RECIBIDOS",
                FilePath = request.FileUploadDto.Eor == 1 ? "OFICIOS-EXPEDIDOS" : "OFICIOS-RECIBIDOS"

            };

            try
            {
                await _fileService.PostFileAsync(fileDto);
                //esperarse 5 segundos
               // await Task.Delay(5000);
                await _oficioRepository.UpdateOficioPdf(request.FileUploadDto.Ejercicio, request.FileUploadDto.Folio, request.FileUploadDto.Eor, fileDto.FilePath + "/"  + fileDto.FileName);
            }
            catch (Exception ex)
            {
                return Result<int>.Failure(ex.Message);
            }
            
            return Result<int>.Success("archivo guardado con exito");

        }
    }
}
