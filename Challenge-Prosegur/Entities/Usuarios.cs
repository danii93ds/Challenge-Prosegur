using System.ComponentModel.DataAnnotations;

namespace Challenge_Prosegur.Entities
{
    public class Usuarios
    {
        [Required]
        public Guid ID { get; set; }
        [Required(ErrorMessage = "Debe ingresar apellido del usuario.")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "Debe ingresar nombre del usuario.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debe ingresar DNI del usuario.")]
        public string DNI { get; set; }
        [Required]
        public Guid RolesID { get; set; }
        public bool Baja { get; set; }
    }
}
