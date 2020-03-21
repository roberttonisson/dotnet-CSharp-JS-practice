using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
    public class Transport : DomainEntity
    {
        [Column(TypeName = "decimal(6,2)")] 
        public decimal Cost { get; set; } = default!;
        
        [MaxLength(256)] [MinLength(1)] public string Address { get; set; } = default!;
        
        public ICollection<Invoice>? Invoices { get; set; }
        
        public ICollection<AdditionalTopping>? AdditionalToppings { get; set; }
    }
}