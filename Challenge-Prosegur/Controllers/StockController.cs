using Challenge_Prosegur.Entities;
using Challenge_Prosegur.Models;
using Challenge_Prosegur.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace Challenge_Prosegur.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        private IStockService StockService;
        public StockController(IStockService service)
        {
            this.StockService = service;
        }

        [HttpGet]
        public async Task<IEnumerable<StockModel>> Consultar()
        {
            return await StockService.Consultar();
        }

        [HttpGet]
        public async Task<IEnumerable<ProductosModel>> Productos(Guid sucursalesID)
        {
            return await StockService.GetProductosDisponibles(sucursalesID);
        }

        [HttpPost]
        public async Task<IActionResult> Consumir(Guid productosID)
        {
            try
            {
                await StockService.Consumir(productosID);

                return Ok(new { message = "Stock consumido" });
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Llenar(Guid materialesID, int cantidad)
        {
            try
            {
                await StockService.Llenar(materialesID, cantidad);

                return Ok(new { message = "Stock aumentado en " + cantidad });
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
    }
}
