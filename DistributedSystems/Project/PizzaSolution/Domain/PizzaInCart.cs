using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
    public class PizzaInCart : DomainEntity
    {
        [NotMapped] public decimal Price { get; set; }

        public int Quantity { get; set; } = default;

        public Guid PizzaTypeId { get; set; } = default!;
        public PizzaType? PizzaType { get; set; }

        public Guid CrustId { get; set; } = default!;
        public Crust? Crust { get; set; }

        public Guid SizeId { get; set; } = default!;
        public Size? Size { get; set; }

        public Guid CartId { get; set; } = default!;
        public Cart? Cart { get; set; }

        public ICollection<InvoiceLine>? InvoiceLines { get; set; }
    }
}