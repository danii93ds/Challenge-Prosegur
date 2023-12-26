namespace Challenge_Prosegur.Entities
{
    public class Usuarios
    {
        public Guid ID { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string DNI { get; set; }
        public Guid RolesID { get; set; }
        public bool Baja { get; set; }
    }
}
