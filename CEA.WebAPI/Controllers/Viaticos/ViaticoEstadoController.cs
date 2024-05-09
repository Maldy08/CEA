using CEA.Application.Interfaces.Repositories;
using CEA.Domain.Entities.Viaticos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CEA.WebAPI.Controllers.Viaticos
{

    public class ViaticoEstadoController : ApiControllerBaseViaticos
    {

        private readonly IGenericRepository<ViaticoEstado> _repository;

        public ViaticoEstadoController(IGenericRepository<ViaticoEstado> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViaticoEstado>>> Get()
        {
            var viaticoEstados = await _repository.GetAllAsync();
            return Ok(viaticoEstados);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ViaticoEstado>> Get(int id)
        {
            var viaticoEstado = await _repository.GetByIdAsync(id);
            return Ok(viaticoEstado);
        }

        [HttpGet("GetByPais/{idPais}")]
        public async Task<ActionResult<IEnumerable<ViaticoEstado>>> GetByPais(int idPais)
        {
            var viaticoEstados = await _repository.Entities.Where(x => x.IdPais == idPais).ToListAsync();
            return Ok(viaticoEstados);
        }
    }
}
