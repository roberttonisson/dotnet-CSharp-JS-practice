using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
    public class DrinkInCart : DomainEntityMetadata
    {
        public int Quantity { get; set; } = default;
        
        [NotMapped]
        public decimal Price { get; set; }
        
        [MaxLength(36)] public string DrinkId { get; set; } = default!;
        public Drink? Drink { get; set; }

        [MaxLength(36)] public string CartId { get; set; } = default!;
        public Cart? Cart { get; set; }
        
        public ICollection<InvoiceLine>? InvoiceLines { get; set; }
    }
}