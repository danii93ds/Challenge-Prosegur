using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Challenge_Prosegur.Entities;

namespace Challenge_Prosegur
{
    public class CafeteriaContext : DbContext
    {
        public CafeteriaContext() : base(nameof(CafeteriaContext))
        {
            Database.Connection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[nameof(CafeteriaContext)].ConnectionString;

            var objectContext = (this as IObjectContextAdapter).ObjectContext;
            objectContext.CommandTimeout = 1000;
        }

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Materiales> Materiales { get; set; }
        public DbSet<ProductosMateriales> ProductosMateriales { get; set; }
        public DbSet<Provincias> Provincias { get; set; }
        public DbSet<Sucursales> Sucursales { get; set; }
        public DbSet<Impuestos> Impuestos { get; set; }
        public DbSet<Ordenes> Ordenes { get; set; }
        public DbSet<OrdenesProductos> OrdenesProductos { get; set; }
    }
}
