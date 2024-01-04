using Challenge_Prosegur.Entities;
using Challenge_Prosegur.Models;
using System.Data.Entity;

namespace Challenge_Prosegur.Services
{
    public interface IStockService
    {
        Task<IEnumerable<StockModel>> Consultar();
        //Task<IEnumerable<Ordenes>> VerTrabajos(); 
        Task<IEnumerable<ProductosModel>> GetProductosDisponibles(Guid sucursalesID);
        Task Consumir(Guid productosID);
        Task Llenar(Guid materialesID, int cantidad);
    }

    public class StockService : IStockService
    {
        CafeteriaContext DbContext;

        public StockService(CafeteriaContext dbContext) { 
            this.DbContext = dbContext;
        }
        public async Task<IEnumerable<StockModel>> Consultar()
        {
            // Falta armar stock con precio calculado + impuestos.
            return await DbContext.Stock.Where(s => s.Baja == false).Select(c => new StockModel()).ToListAsync();
        }

        public async Task<IEnumerable<ProductosModel>> GetProductosDisponibles(Guid sucursalesID) 
        {
            var productos = from p in DbContext.Productos
                            join pm in DbContext.ProductosMateriales
                                on p.ID equals pm.ProductosID
                            join s in DbContext.Stock
                                on pm.MaterialesID equals s.MaterialesID
                            join su in DbContext.Sucursales
                                on s.SucursalesID equals sucursalesID
                            join i in DbContext.Impuestos
                                on su.ProvinciasID equals i.ProvinciasID
                            where
                                pm.Cantidad >= s.Disponible
                                && s.SucursalesID == sucursalesID
                                && p.Baja == false
                            select new ProductosModel() { 
                                // TODO
                            };

            return await (productos).ToListAsync();
        }

        public async Task Consumir(Guid productosID)
        {
            List<ProductosMateriales> consumos = DbContext.ProductosMateriales.Where(c => c.ProductosID == productosID).ToList();

            foreach (ProductosMateriales item in consumos)
            {
                var stockItem = DbContext.Stock.Where(c => c.MaterialesID == item.MaterialesID).First();

                if (stockItem != null)
                {
                    if (stockItem.Disponible >= item.Cantidad)
                        stockItem.Disponible -= item.Cantidad;
                    else
                        throw new Exception("No hay suficiente stock.");
                }
            }

            await DbContext.SaveChangesAsync();
        }

        public async Task Llenar(Guid materialesID, int cantidad)
        {
            var stock = GetStock(materialesID);

            stock.Disponible += cantidad;
            await DbContext.SaveChangesAsync();
        }

        private Stock GetStock(Guid materialesID)
        {
            if (materialesID == Guid.Empty)
                throw new KeyNotFoundException("MaterialesID no puede ser vacio.");

            var stock = DbContext.Stock.Where(s => s.MaterialesID == materialesID).First();

            if (stock == null)
                throw new KeyNotFoundException("Material no encontrado.");

            return stock;
        }
    }
}
