using Challenge_Prosegur.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace Challenge_Prosegur.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly CafeteriaContext dbContext;
        public ProductosController(CafeteriaContext context)
        {
            this.dbContext = context;
        }

        [HttpPost]
        public async Task<IActionResult> Crear(string nombre, string descripcion)
        {
            try
            {
                Productos producto = new Productos()
                {
                    ID = Guid.NewGuid(),
                    Nombre = nombre,
                    Descripcion = descripcion
                };

                dbContext.Productos.Add(producto);
                await dbContext.SaveChangesAsync();

                return Ok(new { message = "Producto creado" });
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IEnumerable<Productos>> Consultar()
        {
            return await dbContext.Productos.Where(p => p.Baja == false).ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(Guid productosID)
        {
            try
            {
                var producto = dbContext.Productos.Find(productosID);

                if (producto == null)
                    return Conflict(new { message = "Usuario no encontrado." });

                producto.Baja = true;
                await dbContext.SaveChangesAsync();

                return Ok(new { message = "Producto eliminado" });
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
    }
}
