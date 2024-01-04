using Challenge_Prosegur.Entities;
using Challenge_Prosegur.Models;
using Challenge_Prosegur.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace Challenge_Prosegur.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdenesController : ControllerBase
    {
        private IOrdenesService OrdenesService;
        public OrdenesController(IOrdenesService ordenesService)
        {
            this.OrdenesService = ordenesService;
        }

        [HttpPost]
        public ActionResult Crear(OrdenesModel ordenModel)
        {
            try
            {
                OrdenesService.Crear(ordenModel);

                return Ok(new { message = "Orden creada" });
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IEnumerable<OrdenesModel>> Consultar()
        {
            return await OrdenesService.GetOrdenes();
        }

        [HttpPost]
        public async Task<IActionResult> Preparar(TrabajosModel trabajosModel)
        {
            try
            {
                await OrdenesService.Preparar(trabajosModel);

                return Ok(new { message = "Orden consultada" });
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
    }
}
