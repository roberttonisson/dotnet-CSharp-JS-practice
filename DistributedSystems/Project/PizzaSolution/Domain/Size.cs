using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Size : DomainEntityMetadata
    {
        [MaxLength(64)] [MinLength(1)] public string Name { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public int SizeCm { get; set; } = default!;
        
        public ICollection<PizzaInCart>? PizzaInCarts { get; set; }
    }
}