namespace Challenge_Prosegur.Entities
{
    public class OrdenesProductos
    {
        public Guid OrdenesID { get; set; }
        public Guid ProductosID { get; set; }
        public int Cantidad { get; set; }
        public Guid? UsuariosID { get; set; }
        public bool Finalizada { get; set; }
    }
}
