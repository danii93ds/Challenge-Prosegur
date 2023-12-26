namespace Challenge_Prosegur.Entities
{
    public class Stock
    {
        public Guid ID { get; set; }
        public int Disponible { get; set; }
        public Guid MaterialesID { get; set; }
        public Guid SucursalesID { get; set; }

        public decimal PrecioUnitario { get; set; }
        public bool Baja { get; set; }
    }
}
