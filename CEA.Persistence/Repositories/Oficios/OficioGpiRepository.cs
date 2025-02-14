using AutoMapper;
using AutoMapper.QueryableExtensions;
using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories.Oficios
{
    public class OficioGpiRepository : IOficioGpiRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OficioGpiRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<OficioGpiDto>> GetAllOficioGpi()
        {
            return await _context.OficioGpi.ProjectTo<OficioGpiDto>(_mapper.ConfigurationProvider).ToListAsync();

        }
    }
}
