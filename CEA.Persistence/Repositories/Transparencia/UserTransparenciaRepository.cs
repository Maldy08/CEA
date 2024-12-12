

using CEA.Application.DTOs.Transparencia;
using CEA.Application.Interfaces.Repositories.Transparencia;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories.Transparencia
{
    public class UserTransparenciaRepository : IUserTransparenciaRepository
    {
        private readonly ApplicationDbContextSQL _context;

        public UserTransparenciaRepository(ApplicationDbContextSQL context)
        {
            _context = context;
        }

        public async Task<UserDtoTransparencia> GetUserByCredentials(string username, string password)
        {
            return await _context.Usuarios.Where(a => a.Usuario1.ToUpper() == username && a.Password.ToUpper() == password)
                .Select(a => new UserDtoTransparencia
                { 
                    IdUsuario = a.IdUsuario,
                    Usuario = a.Usuario1,
                    Password = a.Password,
                    Descripcion = a.Descripcion,
                    IdNivel = a.IdNivel,
                    Activo = a.Activo,
                    IdDepto = a.IdDepto,
                    IdPuesto = a.IdPuesto
                }).FirstOrDefaultAsync();
        }


        public async Task<UserDtoTransparencia> GetUserById(int id)
        {
           return await _context.Usuarios.Where(a => a.IdUsuario == id)
                .Select(a => new UserDtoTransparencia
                {
                    IdUsuario = a.IdUsuario,
                    Usuario = a.Usuario1,
                    Password = a.Password,
                    Descripcion = a.Descripcion,
                    IdNivel = a.IdNivel,
                    Activo = a.Activo,
                    IdDepto = a.IdDepto,
                    IdPuesto = a.IdPuesto
                }).FirstOrDefaultAsync();
        }
    }
}
