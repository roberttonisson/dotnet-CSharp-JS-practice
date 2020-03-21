using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
    public class Drink : DomainEntity
    {
        [MaxLength(64)] [MinLength(1)] public string Name { get; set; } = default!;

        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; } = default!;
        [Column(TypeName = "decimal(4,3)")]
        public decimal Size { get; set; } = default!;

        public ICollection<DrinkInCart>? DrinkInCarts{ get; set; }
        
    }
}