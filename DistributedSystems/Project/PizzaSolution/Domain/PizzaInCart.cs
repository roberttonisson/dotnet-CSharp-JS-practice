using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
    public class PizzaInCart : DomainEntityMetadata
    {
        [NotMapped]
        public decimal Price { get; set; }

        public int Quantity { get; set; } = default;
        
        [MaxLength(36)] public string PizzaTypeId { get; set; } = default!;
        public PizzaType? PizzaType { get; set; }
        
        [MaxLength(36)] public string CrustId { get; set; } = default!;
        public Crust? Crust { get; set; }
        
        [MaxLength(36)] public string SizeId { get; set; } = default!;
        public Size? Size { get; set; }
        
        [MaxLength(36)] public string CartId { get; set; } = default!;
        public Cart? Cart { get; set; }
        
        public ICollection<InvoiceLine>? InvoiceLines { get; set; }
    }
}