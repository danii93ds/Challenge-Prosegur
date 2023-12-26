using Challenge_Prosegur.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace Challenge_Prosegur.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdenesController : ControllerBase
    {
        private readonly CafeteriaContext dbContext;
        public OrdenesController(CafeteriaContext context)
        {
            this.dbContext = context;
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Guid usuariosID, List<OrdenesProductos> productos)
        {
            try
            {
                Ordenes orden = new Ordenes()
                {
                    ID = Guid.NewGuid(),
                    UsuariosID = usuariosID,
                    Baja = false
                };

                dbContext.Ordenes.Add(orden);

                foreach (var item in productos)
                {
                    item.OrdenesID = orden.ID;
                    dbContext.OrdenesProductos.Add(item);
                }

                dbContext.SaveChanges();

                return Ok(new { message = "Orden creada" });
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IEnumerable<Ordenes>> Consultar(Guid usuariosID)
        {
            var orden = from o in dbContext.Ordenes
                            join op in dbContext.OrdenesProductos
                                on o.ID equals op.OrdenesID
                            where
                                op.UsuariosID == usuariosID
                                && o.Baja == false
                            select o;

            return await ((DbSet<Ordenes>)orden).ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Preparar(Guid ordenesID, Guid productosID, Guid usuariosID)
        {
            try
            {
                var orden = dbContext.OrdenesProductos.Where(op => op.OrdenesID == ordenesID && op.ProductosID == productosID).First();
                orden.UsuariosID = usuariosID;
                dbContext.SaveChanges();

                return Ok(new { message = "Orden consultada" });
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
    }
}
