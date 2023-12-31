﻿using System.ComponentModel.DataAnnotations;

namespace Challenge_Prosegur.Entities
{
    public class Stock
    {
        [Required]
        public Guid ID { get; set; }
        public int Disponible { get; set; }
        public Guid MaterialesID { get; set; }
        public Guid SucursalesID { get; set; }
        public decimal PrecioUnitario { get; set; }
        public bool Baja { get; set; }
    }
}
