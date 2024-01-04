using Challenge_Prosegur.Entities;

namespace Challenge_Prosegur.Models
{
    public class OrdenesModel
    {
        public Guid ID { get; set; }
        public long Numero { get; set; }
        public Guid UsuariosID { get; set; }
        public string NombreCompleto { get; set; }
        public List<OrdenesProductos> Productos { get; set; }
        public bool Baja { get; set; }
    }
}
