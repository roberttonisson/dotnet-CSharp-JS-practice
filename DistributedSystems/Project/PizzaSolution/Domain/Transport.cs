using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Transport : DomainEntityMetadata
    {
        public decimal Cost { get; set; } = default!;
        
        [MaxLength(256)] [MinLength(1)] public string Address { get; set; } = default!;
        
        public ICollection<Invoice>? Invoices { get; set; }
        
        public ICollection<AdditionalTopping>? AdditionalToppings { get; set; }
    }
}