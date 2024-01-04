using Challenge_Prosegur.Entities;
using Challenge_Prosegur.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Threading.Tasks.Dataflow;

namespace Challenge_Prosegur.Services
{
    public interface IOrdenesService
    {
        Task<IEnumerable<OrdenesModel>> GetOrdenes();
        //Task<IEnumerable<Ordenes>> VerTrabajos(); 
        Task Crear(OrdenesModel orden);
        Task Preparar(TrabajosModel trabajosModel);
    }

    public class OrdenesService : IOrdenesService
    {
        private CafeteriaContext DbContext;

        private OrdenesService(CafeteriaContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public async Task<IEnumerable<OrdenesModel>> GetOrdenes()
        {
            var ordenes = from o in DbContext.Ordenes
                          where
                              o.Baja == false
                          select new OrdenesModel {
                              ID = o.ID,
                              Numero = o.Numero,
                              Baja = o.Baja,
                              UsuariosID = o.UsuariosID,
                              Productos = (
                                  from op in DbContext.OrdenesProductos
                                  where
                                      op.OrdenesID == o.ID
                                  select op
                              ).ToList()
                          };

            return await ordenes.ToListAsync();
        }

        //public async Task<IEnumerable<OrdenesModel>> Consultar()
        //{
        //    return await DbContext.Ordenes.Where(u => u.Baja == false).ToListAsync();
        //}

        public async Task Crear(OrdenesModel ordenesModel)
        {
            Ordenes orden = new Ordenes()
            {
                ID = Guid.NewGuid(),
                UsuariosID = ordenesModel.UsuariosID,
                Baja = false
            };

            DbContext.Ordenes.Add(orden);

            foreach (var item in ordenesModel.Productos)
            {
                OrdenesProductos ordenesProductos = new OrdenesProductos()
                {
                    OrdenesID = orden.ID,
                    ProductosID = item.ProductosID,
                    Finalizada = false,
                    Cantidad = item.Cantidad,
                    UsuariosID = null
                };
                
                DbContext.OrdenesProductos.Add(ordenesProductos);
            }

            await DbContext.SaveChangesAsync();
        }

        public async Task Preparar(TrabajosModel trabajosModel)
        {
            var ordenProducto = GetOrdenProducto(trabajosModel.OrdenesID, trabajosModel.ProductosID);

            if (ordenProducto == null)
                throw new KeyNotFoundException("Orden y producto no encontrado.");

            ordenProducto.UsuariosID = trabajosModel.UsuariosID;
            await DbContext.SaveChangesAsync();
        }

        public async Task Finalizar(TrabajosModel trabajosModel)
        {
            var ordenProducto = GetOrdenProducto(trabajosModel.OrdenesID, trabajosModel.ProductosID);

            if (ordenProducto == null)
                throw new KeyNotFoundException("Orden y producto no encontrado.");

            ordenProducto.Finalizada = true;
            await DbContext.SaveChangesAsync();
        }

        public OrdenesProductos? GetOrdenProducto(Guid ordenesID, Guid productosID)
        {
            return DbContext.OrdenesProductos.Where(op =>op.OrdenesID == ordenesID&& op.ProductosID == productosID).First();
        }
    }
}
