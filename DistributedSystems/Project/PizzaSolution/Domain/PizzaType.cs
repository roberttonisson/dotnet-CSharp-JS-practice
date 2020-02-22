using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class PizzaType : DomainEntityMetadata
    {
        [MaxLength(64)] [MinLength(1)] public string Name { get; set; } = default!;

        public decimal Price { get; set; } = default!;
        
        [MaxLength(36)]
        public string PizzaRestaurantId { get; set; } = default!;
        public PizzaRestaurant? PizzaRestaurant { get; set; }
        
        public ICollection<DefaultTopping>? DefaultToppings { get; set; }
        
    }
}