using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Domain.Entities.Oficios;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace CEA.Persistence.Repositories.Oficios
{
    public class OficioRepository : IOficioRepository
    {

        private readonly ApplicationDbContext _context;

        public OficioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OficioDto>> GetAllOficios()
        {
            return await _context.OficioDto.ToListAsync();
        }

        public async Task<OficioDto> GetOficioByFolio(int ejercicio, int eor, int folio)
        {
            var oficio = await _context.OficioDto.Where(x => x.Eor == eor && x.Folio == folio && x.Ejercicio == ejercicio).FirstOrDefaultAsync();
            return await _context.OficioDto.Where(x => x.Eor == eor && x.Folio == folio && x.Ejercicio == ejercicio).FirstOrDefaultAsync();
        }

        public async Task<List<OficioDto>> GetOficiosMC(int eor)
        {
            return await _context.OficioDto.Where(x => x.Eor == eor).ToListAsync();
        }


        public async Task<List<OficioDto>> GetOficiosUsuarios(int ejercicio, int eor, int idEmpleado, int idDepto)
        {
            return await _context.OficioDto.Where(x => x.Ejercicio == ejercicio && x.Eor == eor && x.IdEmpleado == idEmpleado && x.Depto == idDepto).ToListAsync();
        }

        public async Task<List<OficioDto>> GetOficiosMCByEjercicio(int eor, int ejercicio)
        {
            return await _context.OficioDto.Where(x => x.Eor == eor && x.Ejercicio == ejercicio).ToListAsync();
        }

        public async Task<Oficio> GetOficio(int ejercicio, int folio, int eor)
        {
            return await _context.Oficio.Where(x => x.Ejercicio == ejercicio && x.Folio == folio && x.Eor == eor).FirstOrDefaultAsync();
        }

        public async Task<List<OficioDtoFunction>> GetListadoOficioFunction(int ejercicio, int eor, int idEmpleado)
        {
            return await _context.OficioDtoFunction.FromSqlInterpolated($"SELECT * FROM TABLE (F_LISTADOOFICIOS({ejercicio},{eor},{idEmpleado}))").ToListAsync();
        }

        public async Task<int> UpdateOficioPdf(int ejercicio, int folio, int eor, string pdfPath)
        {
            var ejercicioParam = new OracleParameter("P_EJERCICIO", OracleDbType.Int32)
            {
                Value = ejercicio
            };

            var folioParam = new OracleParameter("P_FOLIO", OracleDbType.Int32)
            {
                Value = folio
            };

            var eorParam = new OracleParameter("P_EOR", OracleDbType.Int32)
            {
                Value = eor
            };

            var pdfPathParam = new OracleParameter("P_PDFPATH", OracleDbType.Varchar2, 100)
            {
                Value = pdfPath
            };

            var resultadoParam = new OracleParameter("P_RESULTADO", OracleDbType.Varchar2, 100)
            {
                Direction = ParameterDirection.Output
            };

            var mensajeParam = new OracleParameter("P_MENSAJE", OracleDbType.Varchar2, 100)
            {
                Direction = ParameterDirection.Output
            };


            return await _context.Database.ExecuteSqlRawAsync("BEGIN SP_ACTUALIZAR_OFICIO_PDFPATH( :P_EJERCICIO, :P_EOR, :P_FOLIO, :P_PDFPATH, :P_RESULTADO, :P_MENSAJE); END;", ejercicioParam, eorParam, folioParam, pdfPathParam, resultadoParam, mensajeParam);
        }

        public async Task<List<OficioEstatusDto>> GetEstatusOficiosByEor(int eor)
        {
            return await _context.OficioEstatus.Where(x => x.Eor == eor).Select(x => new OficioEstatusDto
            {
                IdEstatus = x.IdEstatus,
                Nombre = x.Nombre,
                Eor = x.Eor
            }).OrderBy(x => x.IdEstatus).ToListAsync();
        }

        public async Task<List<OficioClasificacionDto>> GetOficioClasificacions()
        {
            return await _context.oficioClasificacions.Select(x => new OficioClasificacionDto
            {
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                Id = x.Id,
                Nombre = x.Nombre,
            }).OrderBy(x => x.Id).ToListAsync();
        }
    }
}
