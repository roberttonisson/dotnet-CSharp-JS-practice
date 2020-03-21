using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
    public class InvoiceLine : DomainEntity
    {
        [NotMapped] public decimal Total { get; set; }

        public int Quantity { get; set; } = default;

        public Guid? PizzaInCartId { get; set; }
        public PizzaInCart? PizzaInCart { get; set; }

        public Guid? DrinkInCartId { get; set; }
        public DrinkInCart? DrinkInCart { get; set; }

        public Guid InvoiceId { get; set; } = default!;
        public Invoice? Invoice { get; set; }
    }
}