using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge_Prosegur.Entities
{
    public class Ordenes
    {

        public Guid ID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Numero { get; set; }
        public Guid UsuariosID { get; set; }
        public decimal ImporteTotal { get; set; }
        public bool Baja { get; set; }
    }
}
