using Challenge_Prosegur.Entities;
using Challenge_Prosegur.Models;
using Challenge_Prosegur.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace Challenge_Prosegur.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private IUsuariosService usuariosService;
        public UsuariosController(IUsuariosService servicio)
        {
            this.usuariosService = servicio;
        }

        [HttpPost]
        public async Task<IActionResult> Crear(UsuariosModel usuariosModel)
        {
            try
            {
                await usuariosService.Crear(usuariosModel);

                return Ok(new { message = "Usuario creado" });
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Modificar(UsuariosModel usuariosModel)
        {
            try
            {
                await usuariosService.Modificar(usuariosModel);

                return Ok(new { message = "Usuario modificado" });
            }
            catch (KeyNotFoundException kex)
            {
                return BadRequest(new { message = kex.Message });
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
                await usuariosService.Eliminar(id);

                return Ok(new { message = "Usuario eliminado" });
            }
            catch (KeyNotFoundException kex)
            {
                return BadRequest(new { message = kex.Message });
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
        public async Task<IEnumerable<Usuarios>> Consultar()
        {
            return await usuariosService.GetUsuarios();
        }
    }
}
