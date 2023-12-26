using Challenge_Prosegur.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace Challenge_Prosegur.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        private readonly CafeteriaContext dbContext;
        public StockController(CafeteriaContext context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Stock>> Consultar()
        {
            return await dbContext.Stock.Where(s => s.Baja == false).ToListAsync();
        }

        [HttpGet]
        public async Task<IEnumerable<Productos>> Productos(Guid sucursalesID)
        {
            var productos = from p in dbContext.Productos
                            join pm in dbContext.ProductosMateriales
                                on p.ID equals pm.ProductosID
                            join s in dbContext.Stock
                                on pm.MaterialesID equals s.MaterialesID
                            where
                                pm.Cantidad >= s.Disponible
                                && s.SucursalesID == sucursalesID
                                && p.Baja == false
                            select p;

            return await ((DbSet<Productos>)productos).ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Consumir(Guid productosID)
        {
            try
            {
                List<ProductosMateriales> consumos = dbContext.ProductosMateriales.Where(c => c.ProductosID == productosID).ToList();

                foreach (ProductosMateriales item in consumos)
                {
                    var stockItem = dbContext.Stock.Where(c => c.MaterialesID == item.MaterialesID).First();

                    if (stockItem != null)
                    {
                        if (stockItem.Disponible >= item.Cantidad)
                            stockItem.Disponible -= item.Cantidad;
                        else
                            return Conflict(new { message = "No hay suficiente stock." });
                    }
                }

                dbContext.SaveChanges();

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
                if (materialesID == Guid.Empty)
                    return BadRequest(new { message = "MaterialesID no puede ser vacio." });

                var material = dbContext.Stock.Where(s => s.MaterialesID == materialesID).First();

                if (material == null)
                    return Conflict(new { message = "Material no encontrado." });

                material.Disponible += cantidad;
                dbContext.SaveChanges();

                return Ok(new { message = "Stock aumentado en " + cantidad });
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
    }
}
