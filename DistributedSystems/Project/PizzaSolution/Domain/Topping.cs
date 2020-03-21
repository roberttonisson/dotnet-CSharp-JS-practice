using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
    public class Topping : DomainEntity
    {
        [MaxLength(64)] [MinLength(1)] public string Name { get; set; } = default!;

        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; } = default!;
        
        public ICollection<DefaultTopping>? DefaultToppings { get; set; }
        
        public ICollection<AdditionalTopping>? AdditionalToppings { get; set; }
    }
}