namespace Challenge_Prosegur.Entities
{
    public class Sucursales
    {
        public Guid ID { get; set; }
        public int Codigo { get; set; }
        public Guid ProvinciasID { get; set; }
        public bool Baja { get; set; }
    }
}
