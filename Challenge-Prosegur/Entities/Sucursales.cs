using System.ComponentModel.DataAnnotations;

namespace Challenge_Prosegur.Entities
{
    public class Sucursales
    {
        [Required]
        public Guid ID { get; set; }
        [Required(ErrorMessage = "Debe ingresar codigo de sucursal.")]
        public int Codigo { get; set; }
        [Required]
        public Guid ProvinciasID { get; set; }
        public bool Baja { get; set; }
    }
}
