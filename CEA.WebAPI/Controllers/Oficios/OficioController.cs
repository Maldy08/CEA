
using CEA.Application.DTOs.Oficios;
using CEA.Application.Features.Oficios.Commands.CreateOficio;
using CEA.Application.Features.Oficios.Queries.GetAllOficios;
using CEA.Application.Features.Oficios.Queries.GetOficiosByEjercicioEorFolio;
using CEA.Application.Features.Oficios.Queries.GetOficiosByEjercicioEorIdEmpleadoIdDepto;
using CEA.Application.Features.Oficios.Queries.GetOficiosMcByEor;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Application.Services;
using CEA.Application.UseCases.Google;
using CEA.Application.UseCases.Oficios.Commands.CreateOficio;
using CEA.Application.UseCases.Oficios.Commands.FoliarOficioSp;
using CEA.Application.UseCases.Oficios.Commands.GetPdfOficio;
using CEA.Application.UseCases.Oficios.Commands.UpdateOficio;
using CEA.Application.UseCases.Oficios.Commands.UpdateOficioFolio;
using CEA.Application.UseCases.Oficios.Commands.UploadOficioPdf;
using CEA.Application.UseCases.Oficios.Queries.GetOficioParametroByEjercicio;
using CEA.Application.UseCases.Oficios.Queries.GetOficiosMcByEorAndEjercicio;
using CEA.Application.UseCases.Oficios.Queries.GetOficiosMcByEorAndEjercicioAndFolio;
using CEA.Domain.Entities.Oficios;
using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Oficios
{
    [Route("api/Oficios")]
    [ApiController]
    public class OficioController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IOficioRepository _oficioRepository;
        private readonly IFileService _fileService;

        public OficioController(IMediator mediator, IOficioRepository oficioRepository, IFileService fileService)
        {
            _mediator = mediator;
            _oficioRepository = oficioRepository;
            _fileService = fileService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Result<List<OficioDto>>>> GetAllOficios()
        {
            return await _mediator.Send(new GetAllOficiosQuery());
        }

        [HttpGet("GetOficiosMC/{eor}")]
        [Authorize]

        public async Task<ActionResult<Result<List<OficioDto>>>> GetOficiosMC(int eor)
        {
            return await _mediator.Send(new GetOficiosMcByEorQuery(eor));
        }

        [HttpGet("GetOficiosByEjercicioEorIdEmpleadoIdDepto/{ejercicio}/{eor}/{idEmpleado}/{idDepto}")]
        [Authorize]
        public async Task<ActionResult<Result<List<OficioDto>>>> GetOficiosByEjercicioEorIdEmpleadoIdDepto(int ejercicio, int eor, int idEmpleado, int idDepto)
        {
            return await _mediator.Send(new GetOficiosByEjercicioEorIdEmpleadoIdDeptoQuery(ejercicio, eor, idEmpleado, idDepto));
        }

        [HttpGet("GetOficioByEjercicioEorFolio/{ejercicio}/{eor}/{folio}")]
        [Authorize]
        public async Task<ActionResult<Result<OficioDto>>> GetOficioByEjercicioEorFolio(int ejercicio, int eor, int folio)
        {
            return await _mediator.Send(new GetOficiosByEjercicioEorFolioQuery(ejercicio, eor, folio));
        }

        [HttpPost("CreateOficioSP")]
        [Authorize]
        public async Task<ActionResult<Result<OficioSpInsertarResult>>> CreateOficio([FromBody] CreateOficioCommandSP command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Result<int>>> CreateOficio([FromForm] OficioDto oficioDto)
        {
            var command = new CreateOficioCommand()
            {

                Depto = oficioDto.Depto,
                Eor = oficioDto.Eor,
                DeptoRespon = oficioDto.DeptoRespon,
                DestCargo = oficioDto.DestCargo,
                DestDepen = oficioDto.DestDepen,
                DestNombre = oficioDto.DestNombre,
                DestSiglas = oficioDto.DestSiglas,
                Ejercicio = oficioDto.Ejercicio,
                Empqentrega = oficioDto.Empqentrega,
                Estatus = oficioDto.Estatus,
                Fecha = oficioDto.Fecha,
                FechaAcuse = oficioDto.FechaAcuse,
                FechaCaptura = oficioDto.FechaCaptura,
                FechaLimite = oficioDto.FechaLimite,
                Folio = oficioDto.Folio,
                NoOficio = oficioDto.NoOficio!,
                Pdfpath = oficioDto.Pdfpath,
                Relacionoficio = oficioDto.Relacionoficio,
                RemCargo = oficioDto.RemCargo,
                RemDepen = oficioDto.RemDepen,
                RemNombre = oficioDto.RemNombre,
                RemSiglas = oficioDto.RemSiglas,
                Tema = oficioDto.Tema,
                Tipo = oficioDto.Tipo,
                archivo = oficioDto.archivo,

            };

            var result = await _mediator.Send(command);

            return new JsonResult(result.Data);
        }

        [HttpPost("CreateOficioPDF")]
        [Authorize]
        public async Task<ActionResult<Result<int>>> CreateOficioPDF([FromForm] UploadOficioPdfCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<Result<int>>> UpdateOficio([FromForm] UpdateOficioCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("GetOficiosMCByEorAndEjercicio/{eor}/{ejercicio}")]
        [Authorize]

        public async Task<ActionResult<Result<List<OficioDto>>>> GetOficiosMCByEorAndEjercicio(int eor, int ejercicio)
        {
            //return await _oficioRepository.GetOficiosMCByEjercicio(eor, ejercicio);
            return await _mediator.Send(new GetOficiosMcByEorAndEjercicioQuery(eor, ejercicio));
        }

        [HttpGet("GetOficio/{ejercicio}/{folio}/{eor}")]
        [Authorize]

        public async Task<ActionResult<Result<Oficio>>> GetOficio(int ejercicio, int folio, int eor)
        {
            return await _mediator.Send(new GetOficiosMcByEorAndEjercicioAndFolioQuery(ejercicio, folio, eor));
        }

        [HttpGet("GetOficioFolio/{ejercicio}")]
        [Authorize]

        public async Task<ActionResult<Result<OficioParametroDto>>> GetFolioOficio(int ejercicio)
        {
            return await _mediator.Send(new GetOficioParametroByEjercicioQuery(ejercicio));
        }

        [HttpPut("ActualizarFolios")]
        [Authorize]
        public async Task<ActionResult<Result<int>>> ActualizarFolios([FromBody] UpdateOficioFolioCommand command)
        {
            return await _mediator.Send(command);
        }

        //endpoint para solicitarle el pdf de un oficio

        [HttpPost("GetPdfOficio")]
        [Authorize]
        public async Task<ActionResult> GetPdfOficio([FromBody] GetPdfOficioCommand command)
        {
            var file = await _mediator.Send(new GetPdfOficioCommand(command.Ejercicio, command.Folio, command.Eor));
            return File(file, "application/pdf", "Oficio.pdf");

        }

        [HttpGet("DownloadWord/{ejercicio}/{folio}/{eor}")]
        [Authorize]
        public async Task<IActionResult> Pruebas(int ejercicio, int folio, int eor)
        {
            var file = await _mediator.Send(new GetDocumentCommand(ejercicio, folio, eor));
            if (file == null)
            {
                return NotFound();
            }
            return new FileStreamResult(file, "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
            {
                FileDownloadName = "Oficio.docx",

            };

        }

        [HttpPost("FoliarOficioSp")]
        [Authorize]
        public async Task<ActionResult<Result<OficioSpFoliarResult>>> FoliarOficioSp([FromBody] FoliarOficioSpCommand command)
        {
            var result = await _mediator.Send(command);

            var response = new
            {
                folio_nuevo = result.Data?.FOLIO_NUEVO ?? 0,
                mensaje = result.Data?.MENSAJE ?? "Error desconocido"
            };

            return new JsonResult(response);

        }

    }
}
