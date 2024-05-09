using CEA.Application.Interfaces.Repositories;
using CEA.Domain.Entities.Viaticos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Viaticos
{
    public class ViaticoCiudadController : ApiControllerBaseViaticos
    {

        private readonly IGenericRepository<ViaticoCiudad> _viaticoCiudadRepository;

        public ViaticoCiudadController(IGenericRepository<ViaticoCiudad> viaticoCiudadRepository)
        {
            _viaticoCiudadRepository = viaticoCiudadRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ViaticoCiudad>>> GetAll()
        {
            var viaticoCiudades = await _viaticoCiudadRepository.GetAllAsync();
            return Ok(viaticoCiudades);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ViaticoCiudad>> GetById(int id)
        {
            var viaticoCiudad = await _viaticoCiudadRepository.GetByIdAsync(id);
            return Ok(viaticoCiudad);
        }
    }
}
