namespace Challenge_Prosegur.Models
{
    public class StockModel
    {
        public Guid ID { get; set; }
        public int Disponible { get; set; }
        public Guid MaterialesID { get; set; }
        public Guid SucursalesID { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioFinal { get; set; }
    }
}
