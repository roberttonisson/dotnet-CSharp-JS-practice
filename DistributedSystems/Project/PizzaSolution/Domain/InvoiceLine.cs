using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
    public class InvoiceLine : DomainEntityMetadata
    {
        [NotMapped]
        public decimal Total { get; set; }

        public int Quantity { get; set; } = default;
        
        [MaxLength(36)] public string? PizzaInCartId { get; set; }
        public PizzaInCart? PizzaInCart { get; set; }
        
        [MaxLength(36)] public string? DrinkInCartId { get; set; } 
        public DrinkInCart? DrinkInCart { get; set; }
        
        [MaxLength(36)] public string InvoiceId { get; set; } = default!;
        public Invoice? Invoice { get; set; }
        
    }
}