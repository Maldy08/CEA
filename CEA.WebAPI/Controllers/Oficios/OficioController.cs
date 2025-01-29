
using CEA.Application.DTOs.Oficios;
using CEA.Application.Features.Oficios.Commands.CreateOficio;
using CEA.Application.Features.Oficios.Queries.GetAllOficios;
using CEA.Application.Features.Oficios.Queries.GetOficiosByEjercicioEorFolio;
using CEA.Application.Features.Oficios.Queries.GetOficiosByEjercicioEorIdEmpleadoIdDepto;
using CEA.Application.Features.Oficios.Queries.GetOficiosMcByEor;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Application.Services;
using CEA.Application.UseCases.Oficios.Commands.UpdateOficio;
using CEA.Application.UseCases.Oficios.Commands.UpdateOficioFolio;
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
        public async Task<ActionResult<Result<List<OficioDto>>>> GetAllOficios()
        {
            return await _mediator.Send(new GetAllOficiosQuery());
        }

        [HttpGet("GetOficiosMC/{eor}")]

        public async Task<ActionResult<Result<List<OficioDto>>>> GetOficiosMC(int eor)
        {
            return await _mediator.Send(new GetOficiosMcByEorQuery(eor));
        }

        [HttpGet("GetOficiosByEjercicioEorIdEmpleadoIdDepto/{ejercicio}/{eor}/{idEmpleado}/{idDepto}")]
        public async Task<ActionResult<Result<List<OficioDto>>>> GetOficiosByEjercicioEorIdEmpleadoIdDepto(int ejercicio, int eor, int idEmpleado, int idDepto)
        {
            return await _mediator.Send(new GetOficiosByEjercicioEorIdEmpleadoIdDeptoQuery(ejercicio, eor, idEmpleado, idDepto));
        }

        [HttpGet("GetOficioByEjercicioEorFolio/{ejercicio}/{eor}/{folio}")]
        public async Task<ActionResult<Result<OficioDto>>> GetOficioByEjercicioEorFolio(int ejercicio, int eor, int folio)
        {
            return await _mediator.Send(new GetOficiosByEjercicioEorFolioQuery(ejercicio, eor, folio));
        }

        //[HttpPost]
        //public async Task<ActionResult<Result<int>>> CreateOficio([FromBody] CreateOficioCommand command)
        //{
        //    return await _mediator.Send(command);
        //}

        [HttpPost]
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
                NoOficio = oficioDto.NoOficio,
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

        [HttpPut]
        public async Task<ActionResult<Result<int>>> UpdateOficio([FromForm] UpdateOficioCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("GetOficiosMCByEorAndEjercicio/{eor}/{ejercicio}")]

        public async Task<ActionResult<Result<List<OficioDto>>>> GetOficiosMCByEorAndEjercicio(int eor, int ejercicio)
        {
            //return await _oficioRepository.GetOficiosMCByEjercicio(eor, ejercicio);
            return await _mediator.Send(new GetOficiosMcByEorAndEjercicioQuery(eor, ejercicio));
        }

        [HttpGet("GetOficio/{ejercicio}/{folio}/{eor}")]

        public async Task<ActionResult<Result<Oficio>>> GetOficio(int ejercicio, int folio, int eor)
        {
            return await _mediator.Send(new GetOficiosMcByEorAndEjercicioAndFolioQuery(ejercicio, folio, eor));
        }

        [HttpGet("GetOficioFolio/{ejercicio}")]

        public async Task<ActionResult<Result<OficioParametroDto>>> GetFolioOficio(int ejercicio)
        {
            return await _mediator.Send(new GetOficioParametroByEjercicioQuery(ejercicio));
        }

        [HttpPut("ActualizarFolios")]
        public async Task<ActionResult<Result<int>>> ActualizarFolios([FromBody] UpdateOficioFolioCommand command)
        {
            return await _mediator.Send(command);
        }



        //endpoint para solicitarle el pdf de un oficio
        [Authorize]
        [HttpGet("GetPdfOficio/{ejercicio}/{folio}/{eor}")]
        public async Task<ActionResult> GetPdfOficio(int ejercicio, int folio, int eor)
        {
            var oficio = await _oficioRepository.GetOficio(ejercicio, folio, eor);
            if (oficio == null)
            {
                return NotFound();
            }
            var file = await _fileService.DownloadPdf(oficio.Pdfpath!);
            return File(file, "application/pdf");
        }

    }
}
