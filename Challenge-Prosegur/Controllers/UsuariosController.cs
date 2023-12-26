using Challenge_Prosegur.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace Challenge_Prosegur.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly CafeteriaContext dbContext;
        public UsuariosController(CafeteriaContext context)
        {
            this.dbContext = context;
        }

        [HttpPost]
        public async Task<IActionResult> Crear(string apellido, string nombre, string dni, Guid rolesID)
        {
            try
            {
                Usuarios usuario = new Usuarios()
                {
                    ID = Guid.NewGuid(),
                    Apellido = apellido,
                    Nombre = nombre,
                    DNI = dni,
                    RolesID = rolesID,
                    Baja = false
                };

                dbContext.Usuarios.Add(usuario);
                dbContext.SaveChanges();

                return Ok(new { message = "Usuario creado" });
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Modificar(Guid id, string apellido, string nombre, string dni, Guid rolesID)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest(new { message = "ID no puede ser vacio." });

                Usuarios usuario = dbContext.Usuarios.Find(id);
                if (usuario == null)
                    return Conflict(new { message = "Usuario no encontrado." });

                usuario.Apellido = apellido;
                usuario.Nombre = nombre;
                usuario.DNI = dni;
                usuario.RolesID = rolesID;
                dbContext.SaveChanges();

                return Ok(new { message = "Usuario modificado" });
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            try
            {
                Usuarios usuario = dbContext.Usuarios.Find(id);

                usuario.Baja = true;
                dbContext.SaveChanges();

                return Ok(new { message = "Usuario eliminado" });
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
        public async Task<IEnumerable<Usuarios>> Consultar()
        {
            return await dbContext.Usuarios.Where(u => u.Baja == false).ToListAsync();
        }
    }
}
