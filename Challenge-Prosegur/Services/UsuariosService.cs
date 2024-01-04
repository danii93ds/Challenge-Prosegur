using Challenge_Prosegur.Entities;
using Challenge_Prosegur.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace Challenge_Prosegur.Services
{
    public interface IUsuariosService
    {
        Task<IEnumerable<Usuarios>> GetUsuarios();

        Task Crear(UsuariosModel usuario);
        Task Modificar(UsuariosModel usuario);
        Task Eliminar(Guid id);
    }

    public class UsuariosService : IUsuariosService
    {
        private CafeteriaContext DbContext;

        UsuariosService(CafeteriaContext dbContext)
        {
            DbContext = dbContext;
        }

        UsuariosService() 
        {
            DbContext = new CafeteriaContext();
        }

        public async Task<IEnumerable<Usuarios>> GetUsuarios()
        {
            return await DbContext.Usuarios.Where(u => u.Baja == false).ToListAsync();
        }

        public async Task Crear(UsuariosModel usuarioModel) 
        {
            Usuarios usuario = new Usuarios()
            {
                ID = Guid.NewGuid(),
                Apellido = usuarioModel.Apellido,
                Nombre = usuarioModel.Nombre,
                DNI = usuarioModel.DNI,
                RolesID = usuarioModel.RolesID,
                Baja = false
            };

            DbContext.Usuarios.Add(usuario);
            await DbContext.SaveChangesAsync();
        }

        public async Task Modificar(UsuariosModel usuariosModel)
        {
            var usuario = GetUsuario(usuariosModel.ID);

            usuario.Apellido = usuariosModel.Apellido;
            usuario.Nombre = usuariosModel.Nombre;
            usuario.DNI = usuariosModel.DNI;
            usuario.RolesID = usuariosModel.RolesID;

            await DbContext.SaveChangesAsync();
        }

        public async Task Eliminar(Guid id) 
        {
            var usuario = GetUsuario(id);

            usuario.Baja = true;

            await DbContext.SaveChangesAsync();
        }

        private Usuarios GetUsuario(Guid id)
        {
            if (id == Guid.Empty)
                throw new KeyNotFoundException("ID no puede ser vacio.");

            var usuario = DbContext.Usuarios.Find(id);
            if (usuario == null)
                throw new KeyNotFoundException("Usuario no encontrado.");

            return usuario;
        }
    }
}
