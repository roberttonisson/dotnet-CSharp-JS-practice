using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Drink : DomainEntityMetadata
    {
        [MaxLength(64)] [MinLength(1)] public string Name { get; set; } = default!;

        public decimal Price { get; set; } = default!;
        public decimal Size { get; set; } = default!;

        public ICollection<DrinkInCart>? DrinkInCarts{ get; set; }
        
    }
}