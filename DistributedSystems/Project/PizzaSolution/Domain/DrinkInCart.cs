using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
    public class DrinkInCart : DomainEntity
    {
        public int Quantity { get; set; } = default;

        [NotMapped] public decimal Price { get; set; }

        public Guid DrinkId { get; set; } = default!;
        public Drink? Drink { get; set; }

        public Guid CartId { get; set; } = default!;
        public Cart? Cart { get; set; }

        public ICollection<InvoiceLine>? InvoiceLines { get; set; }
    }
}