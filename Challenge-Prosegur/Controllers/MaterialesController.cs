using Challenge_Prosegur.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace Challenge_Prosegur.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaterialesController : ControllerBase
    {
        private readonly CafeteriaContext dbContext;
        public MaterialesController(CafeteriaContext context)
        {
            this.dbContext = context;
        }

        [HttpPost]
        public async Task<IActionResult> Crear(string nombre)
        {
            Materiales material = new Materiales()
            {
                ID = Guid.NewGuid(),
                Material = nombre
            };

            dbContext.Materiales.Add(material);
            await dbContext.SaveChangesAsync();

            return Ok(new { message = "Material creado" });
        }

        [HttpGet]
        public async Task<IEnumerable<Materiales>> Consultar()
        {
            return await dbContext.Materiales.Where(p => p.Baja == false).ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(Guid MaterialesID)
        {
            var material = dbContext.Materiales.Find(MaterialesID);

            material.Baja = true;
            await dbContext.SaveChangesAsync();

            return Ok(new { message = "Material eliminado" });
        }
    }
}
