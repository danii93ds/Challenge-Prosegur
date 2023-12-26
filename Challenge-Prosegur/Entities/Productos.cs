namespace Challenge_Prosegur.Entities
{
    public class Productos
    {
        public Guid ID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public decimal PrecioUnitario { get; set; }
        public bool Baja { get; set; }
    }
}
