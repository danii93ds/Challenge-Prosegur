namespace Challenge_Prosegur.Entities
{
    public class ProductosMateriales
    {
        public Guid ProductosID { get; set; }
        public Guid MaterialesID { get; set; }
        public int Cantidad { get; set; }
        public bool Baja { get; set; }
    }
}
