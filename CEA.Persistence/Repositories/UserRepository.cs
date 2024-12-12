using CEA.Application.DTOs;
using CEA.Application.Interfaces.Repositories;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async  Task<List<UserDto>> GetAllUsuarios()
        {
           return await _context.Usuarios
                .Select(a => new UserDto
                {
                    Login = a.Login,
                    Pass = a.Pass,
                    Activo = a.Activo,
                    Depto = a.Depto,
                    DeptoDescripcion = a.DeptoDescripcion,
                    Descripcion = a.Descripcion,
                    IdPue = a.IdPue,
                    NoEmpleado = a.NoEmpleado,
                    NombreCompleto = a.NombreCompleto,
                    Usuario = a.Usuario,
                    Municipio = a.Municipio,
                    Oficina = a.Oficina,
                    Activos = a.Activos,
                    ActivosNivel = a.ActivosNivel,
                    Almacen = a.Almacen,
                    AlmacenNivel = a.AlmacenNivel,
                    Bd = a.Bd ?? 0,
                    Caja = a.Caja,
                    CajaNivel = a.CajaNivel,
                    Compras = a.Compras,
                    ComprasNivel = a.ComprasNivel,
                    Contabilidad = a.Contabilidad,
                    ContabilidadNivel = a.ContabilidadNivel,
                    Nominas = a.Nominas,
                    NominasNivel = a.NominasNivel,
                    Polnom = a.Polnom,
                    Presupuestos = a.Presupuestos,
                    PresupuestosNivel = a.PresupuestosNivel,
                    Vales = a.Vales,
                    ValesNivel = a.ValesNivel,
                    Viaticos = a.Viaticos,
                    ViaticosNivel = a.ViaticosNivel
                }).ToListAsync();
        }

        public async Task<UserDto> GetUserByCredentials(string username, string password)
        {
            //var usuario = await _context.Usuarios.Where(u => u.Login == username.ToUpper() && u.Pass == password.ToUpper())
            //    .Select( a => new
            //    {
            //        a.Login,
            //        a.Activo,
            //        a.Viaticos,
            //        a.ViaticosNivel
            //    })
            //    .FirstOrDefaultAsync();


            return await _context.Usuarios
                .Where(a => a.Login.ToUpper() == username.ToUpper() && a.Pass.ToUpper() == password.ToUpper())
                .Select(a => new UserDto
                {
                    Login = a.Login,
                    Pass = a.Pass,
                    Activo = a.Activo,
                    Depto = a.Depto,
                    DeptoDescripcion = a.DeptoDescripcion,
                    Descripcion = a.Descripcion,
                    IdPue = a.IdPue,
                    NoEmpleado = a.NoEmpleado,
                    NombreCompleto = a.NombreCompleto,
                    Usuario = a.Usuario,
                    Municipio = a.Municipio,
                    Oficina = a.Oficina,
                    Activos = a.Activos,
                    ActivosNivel = a.ActivosNivel,
                    Almacen = a.Almacen,
                    AlmacenNivel = a.AlmacenNivel,
                    Bd = a.Bd ?? 0,
                    Caja = a.Caja,
                    CajaNivel = a.CajaNivel,
                    Compras = a.Compras,
                    ComprasNivel = a.ComprasNivel,
                    Contabilidad = a.Contabilidad,
                    ContabilidadNivel = a.ContabilidadNivel,
                    Nominas = a.Nominas,
                    NominasNivel = a.NominasNivel,
                    Polnom = a.Polnom,
                    Presupuestos = a.Presupuestos,
                    PresupuestosNivel = a.PresupuestosNivel,
                    Vales = a.Vales,
                    ValesNivel = a.ValesNivel,
                    Viaticos = a.Viaticos,
                    ViaticosNivel = a.ViaticosNivel,
                    Oficios = a.Oficios,
                    OficiosNivel = a.OficiosNivel
                }).FirstOrDefaultAsync();
        }

      

        public async Task<UserDto> GetUserById(int id)
        {
            return await _context.Usuarios
                .Where(a => a.Usuario == id)
                .Select(a => new UserDto
                {
                    Login = a.Login,
                    Pass = a.Pass,
                    Activo = a.Activo,
                    Depto = a.Depto,
                    DeptoDescripcion = a.DeptoDescripcion,
                    Descripcion = a.Descripcion,
                    IdPue = a.IdPue,
                    NoEmpleado = a.NoEmpleado,
                    NombreCompleto = a.NombreCompleto,
                    Usuario = a.Usuario,
                    Municipio = a.Municipio,
                    Oficina = a.Oficina,
                    Activos = a.Activos ?? false,
                    ActivosNivel = a.ActivosNivel ?? 0,
                    Almacen = a.Almacen ?? false,
                    AlmacenNivel = a.AlmacenNivel ?? 0,
                    Bd = a.Bd ?? 0,
                    Caja = a.Caja ?? false,
                    CajaNivel = a.CajaNivel ?? 0,
                    Compras = a.Compras ?? false,
                    ComprasNivel = a.ComprasNivel ?? 0,
                    Contabilidad = a.Contabilidad ?? false,
                    ContabilidadNivel = a.ContabilidadNivel ?? 0,
                    Nominas = a.Nominas ?? false,
                    NominasNivel = a.NominasNivel ?? 0,
                    Polnom = a.Polnom,
                    Presupuestos = a.Presupuestos ?? false,
                    PresupuestosNivel = a.PresupuestosNivel ?? 0,
                    Vales = a.Vales ?? false,
                    ValesNivel = a.ValesNivel ?? 0,
                    Viaticos = a.Viaticos ?? false,
                    ViaticosNivel = a.ViaticosNivel ?? 0

                }).FirstOrDefaultAsync();
        }

        public async Task<UserDto> GetUserByIdEmpleado(int idEmpleado)
        {
            return await _context.Usuarios
                 .Where(a => a.NoEmpleado == idEmpleado)
                 .Select(a => new UserDto
                 {
                     Login = a.Login,
                     Pass = a.Pass,
                     Activo = a.Activo,
                     Depto = a.Depto,
                     DeptoDescripcion = a.DeptoDescripcion,
                     Descripcion = a.Descripcion,
                     IdPue = a.IdPue,
                     NoEmpleado = a.NoEmpleado,
                     NombreCompleto = a.NombreCompleto,
                     Usuario = a.Usuario,
                     Municipio = a.Municipio,
                     Oficina = a.Oficina,
                     Activos = a.Activos ?? false,
                     ActivosNivel = a.ActivosNivel ?? 0,
                     Almacen = a.Almacen ?? false,
                     AlmacenNivel = a.AlmacenNivel ?? 0,
                     Bd = a.Bd ?? 0,
                     Caja = a.Caja ?? false,
                     CajaNivel = a.CajaNivel ?? 0,
                     Compras = a.Compras ?? false,
                     ComprasNivel = a.ComprasNivel ?? 0,
                     Contabilidad = a.Contabilidad ?? false,
                     ContabilidadNivel = a.ContabilidadNivel ?? 0,
                     Nominas = a.Nominas ?? false,
                     NominasNivel = a.NominasNivel ?? 0,
                     Polnom = a.Polnom,
                     Presupuestos = a.Presupuestos ?? false,
                     PresupuestosNivel = a.PresupuestosNivel ?? 0,
                     Vales = a.Vales ?? false,
                     ValesNivel = a.ValesNivel ?? 0,
                     Viaticos = a.Viaticos ?? false,
                     ViaticosNivel = a.ViaticosNivel ?? 0

                 }).FirstOrDefaultAsync();
        }
    }
}
