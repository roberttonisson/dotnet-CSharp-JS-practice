using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Topping : DomainEntityMetadata
    {
        [MaxLength(64)] [MinLength(1)] public string Name { get; set; } = default!;

        public decimal Price { get; set; } = default!;
        
        public ICollection<DefaultTopping>? DefaultToppings { get; set; }
        
        public ICollection<AdditionalTopping>? AdditionalToppings { get; set; }
    }
}