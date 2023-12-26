namespace Challenge_Prosegur.Entities
{
    public class Impuestos
    {
        public Guid ID { get; set; } 
        public Guid ProvinciasID { get; set; }
        public decimal Porcentaje { get; set; }
        public bool Baja { get; set; }
    }
}
