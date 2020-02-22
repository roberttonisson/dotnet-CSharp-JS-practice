using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class PizzaRestaurant : DomainEntityMetadata
    {
        [MaxLength(64)] [MinLength(1)] public string Name { get; set; } = default!;

        [MaxLength(256)] [MinLength(1)] public string Address { get; set; } = default!;
        
        public ICollection<PizzaType>? PizzaTypes { get; set; }
        
    }
}