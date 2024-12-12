using CEA.Application.DTOs;
using CEA.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers
{
    [Route("api/Departamentos")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {

        private readonly IDeptoRepository _repository;

        public DepartamentoController(IDeptoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeptoUeDto>>> GetDeptos()
        {
            return Ok(await _repository.GetDeptosAsync());
        }

       
        [HttpGet("GetDeptoById/{id}")]
        public async Task<ActionResult<DeptoUeDto>> GetDeptoById(int id)
        {
            return Ok(await _repository.GetDeptoByIdAsync(id));
        }
    }
}
