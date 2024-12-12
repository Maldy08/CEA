using CEA.Application.DTOs.Viaticos;
using CEA.Application.Features.Viaticos.Commands.CreateViatico;
using CEA.Application.Features.Viaticos.Commands.UpdateViatico;
using CEA.Application.Features.Viaticos.Generates;
using CEA.Application.Features.Viaticos.Queries.GetAllByEjercicioDepto;
using CEA.Application.Features.Viaticos.Queries.GetAllByEjercicioOficinaNoviat;
using CEA.Application.Features.Viaticos.Queries.GetAllViaticosByEjercicioAndOficina;
using CEA.Application.Features.Viaticos.Queries.GetDetalleViaticoByEjercicioNoviatOficina;
using CEA.Application.Features.Viaticos.Queries.GetFormatoComisionByOficinaEjercicioNoviat;
using CEA.Application.Features.Viaticos.Queries.GetNoViat;
using CEA.Application.Features.Viaticos.Queries.ListaViaticosPorEmpleado;
using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Viaticos
{

  
    public class ViaticoController : ApiControllerBaseViaticos
    {
        private readonly IMediator _mediator;

        public ViaticoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllViaticosByEjercicioAndOficina")]
        public async Task<ActionResult<Result<List<GetAllViaticosDto>>>> GetAllViaticosByEjercicioAndOficina(int ejercicio, int oficina)
        {
            return await _mediator.Send(new GetAllViaticosByEjercicioAndOficinaQuery(ejercicio, oficina));
        }

        [HttpGet("GetNoViat/{ejercicio}/{oficina}")]
        public async Task<ActionResult<Result<int>>> GetNoViat(int ejercicio, int oficina)
        {
            return await _mediator.Send(new GetNoViatQuery(ejercicio, oficina));
        }

        [HttpGet("GetAllByEjercicioDepto")]
        public async Task<ActionResult<Result<List<GetAllViaticosDto>>>> GetAllByEjercicioDepto(int ejercicio, int empleado)
        {
            return await _mediator.Send(new GetAllByEjercicioDeptoQuery(ejercicio, empleado));
        }

        [HttpGet("ListaViaticosPorEmpleado/{ejercicio}/{empleado}")]
        public async Task<ActionResult<Result<List<ViaticosPorEmpleadoDto>>>> ListaViaticosPorEmpleado(int ejercicio, int empleado)
        {
            return await _mediator.Send(new ListaViatosPorEmpleadoQuery(ejercicio, empleado));
        }

        [HttpGet("GetAllByEjercicioOficinaNoviat/{ejercicio}/{oficina}/{noviat}")]
        public async Task<ActionResult<Result<GetAllViaticosDto>>> GetAllByEjercicioOficinaNoviat(int ejercicio, int oficina, int noviat)
        {
            return await _mediator.Send(new GetAllByEjercicioOficinaNoviatQuery(ejercicio, oficina, noviat));
        }

        [HttpGet("GetFormatoComisionByOficinaEjercicioNoviat")]
        public async Task<ActionResult<Result<FormatoComisionDto>>> GetFormatoComisionByOficinaEjercicioNoviat(int oficina, int ejercicio, int noViat)
        {
            return await _mediator.Send(new GetFormatoComisionByOficinaEjercicioNoviatQuery(oficina, ejercicio, noViat));
        }


        [HttpGet("FormatoComision/{oficina}/{ejercicio}/{noviat}")]
        public async Task<ActionResult> FormatoComision(int oficina, int ejercicio, int noviat)
        {
            var result = await _mediator.Send(new GenerateFormatoComisionPdf(oficina, ejercicio, noviat));
            return new FileStreamResult(result, "application/pdf") { FileDownloadName = "Viaticos.pdf" };

            // return await _mediator.Send(new GenerateFormatoComisionPdf(oficina, ejercicio,noviat));
        }

        [HttpGet("ReciboViatico/{ejercicio}/{oficina}/{noviat}")]

        public async Task<ActionResult> ReciboViatico(int ejercicio, int oficina, int noviat)
        {
            var result = await _mediator.Send(new GenerateReciboViaticoPdf(ejercicio, oficina, noviat));
            return new FileStreamResult(result, "application/pdf") { FileDownloadName = "ReciboViatico.pdf" };
        }

        [HttpGet("InformeActividades/{ejercicio}/{oficina}/{noviat}")]

        public async Task<ActionResult> InformeActividades(int ejercicio, int oficina, int noviat)
        {
            var result = await _mediator.Send(new GenerateInformeActividadesPdf(ejercicio, oficina, noviat));
            return new FileStreamResult(result, "application/pdf") { FileDownloadName = "InformeActividades.pdf" };
        }

        [HttpGet("TresFormatos/{ejercicio}/{oficina}/{noviat}")]

        public async Task<ActionResult> TresFormatos(int ejercicio, int oficina, int noviat)
        {
            var result = await _mediator.Send(new GenerateFormatosPdf(ejercicio, oficina, noviat));
            return new FileStreamResult(result, "application/pdf") { FileDownloadName = $"V{oficina}-{noviat}-{ejercicio}.pdf" };
        }

        [HttpPost]
        public async Task<ActionResult<Result<int>>> CreateViatico(CreateViaticoCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        public async Task<ActionResult<Result<int>>> UpdateViatico(UpdateViaticoCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("DetalleViatico/{ejercicio:int}/{noviat:int}/{oficina:int}")]

        public async Task<ActionResult<Result<ViaticoDetalleDto>>> GetAllViaticosByEjercicioAndNoviatAndOficina(int ejercicio, int noviat, int oficina)
        {
            return await _mediator.Send(new GetDetalleViaticoByEjercicioNoviatOficinaQuery(ejercicio, noviat, oficina));
        }



    }
}
