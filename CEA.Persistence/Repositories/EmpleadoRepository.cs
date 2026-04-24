

using CEA.Application.DTOs;
using CEA.Application.Interfaces.Repositories;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly ApplicationDbContext _context;

        public EmpleadoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmpleadoDto>> GetEmpleadosByDeptoComi(int id)
        {
          return await _context.Empleado
                .Where(a => a.DeptoComi == id)
                .Select(a => new EmpleadoDto
                {
                    Empleado = a.IdEmpleado,
                    Nombre = a.Nombre,
                    Paterno = a.Paterno,
                    Materno = a.Materno,
                    Activo = a.Activo,
                    DeptoComi = a.DeptoComi,
                    DeptoUe = a.DeptoUe,
                    DescripcionDepto = a.DescripcionDepto,
                    DescripcionPuesto = a.DescripcionPuesto,
                    IdPue = a.IdPue,
                    LugarTrab = a.LugarTrab,
                    Municipio = a.Municipio,
                    Nivel = a.Nivel,
                    NombreCompleto = a.NombreCompleto,  
                    Oficina = a.Oficina,
                    Correo = a.Correo,
                    DeptoPpto = a.DeptoPpto,
                    Obra = a.Obra
                }).Where(a => a.Activo == "V" || a.Activo == "C").OrderBy(a => a.Empleado).ToListAsync();
        }

        public async Task<List<EmpleadoDto>> GetEmpleadosByDeptoPpto(int id)
        {
           return await _context.Empleado.Where(a => a.DeptoUe == id)
                .Select(a => new EmpleadoDto
                {
                    Empleado = a.IdEmpleado,
                    Nombre = a.Nombre,
                    Paterno = a.Paterno,
                    Materno = a.Materno,
                    Activo = a.Activo,
                    DeptoComi = a.DeptoComi,
                    DeptoUe = a.DeptoUe,
                    DescripcionDepto = a.DescripcionDepto,
                    DescripcionPuesto = a.DescripcionPuesto,
                    IdPue = a.IdPue,
                    LugarTrab = a.LugarTrab,
                    Municipio = a.Municipio,
                    Nivel = a.Nivel,
                    NombreCompleto = a.NombreCompleto,  
                    Oficina = a.Oficina,
                    Correo = a.Correo,
                    DeptoPpto = a.DeptoPpto,
                    Obra = a.Obra
                }).Where(a => a.Activo == "V" || a.Activo == "C").OrderBy(a => a.Empleado).ToListAsync();
        }

        public async Task<EmpleadoDto> GetEmpleadoByIdAsync(int id)
        {
            
            return await _context.Empleado
                .Where(a => a.IdEmpleado == id)
                .Select(a => new EmpleadoDto
                {
                    Empleado = a.IdEmpleado,
                    Nombre = a.Nombre,
                    Paterno = a.Paterno,
                    Materno = a.Materno,
                    Activo = a.Activo,
                    DeptoComi = a.DeptoComi,
                    DeptoUe = a.DeptoUe,
                    DescripcionDepto = a.DescripcionDepto,
                    DescripcionPuesto = a.DescripcionPuesto,
                    IdPue = a.IdPue,
                    LugarTrab = a.LugarTrab,
                    Municipio = a.Municipio,
                    Nivel = a.Nivel,
                    NombreCompleto = a.NombreCompleto,  
                    Oficina = a.Oficina ,
                    Correo = a.Correo,
                    DeptoPpto = a.DeptoPpto,
                    Obra = a.Obra
                }).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EmpleadoDto>> GetEmpleadosAsync()
        {
            return await _context.Empleado
                .Select(a => new EmpleadoDto
                {
                    Empleado = a.IdEmpleado,
                    Frecuecia = a.Frecuencia,
                    Nombre = a.Nombre,
                    Paterno = a.Paterno,
                    Materno = a.Materno,
                    Activo = a.Activo,
                    DeptoComi = a.DeptoComi,
                    DeptoUe = a.DeptoUe,
                    DescripcionDepto = a.DescripcionDepto,
                    DescripcionPuesto = a.DescripcionPuesto,
                    IdPue = a.IdPue,
                    LugarTrab = a.LugarTrab,
                    Municipio = a.Municipio,
                    Nivel = a.Nivel,
                    NombreCompleto = a.NombreCompleto,  
                    Oficina = a.Oficina,
                    Correo = a.Correo,
                    DeptoPpto = a.DeptoPpto,
                    Obra = a.Obra
                }).Where(a => a.Activo == "V" || a.Activo == "C").ToListAsync();
        }

        public async Task<bool> EsEmpleadoResponsableAsync(int idEmpleado)
        {
            return await _context.EmpleadoResponsable
                .AnyAsync(e => e.IdEmpleado == idEmpleado);
        }
    }
}
