using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.rotoni.pizzaApp.DAL.Base;


namespace DAL.App.DTO
{
    public class InvoiceLine : DomainEntityIdMetadata
    {
        [Display(Name = nameof(Total), ResourceType = typeof(Resources.Domain.Shared))]
        [NotMapped] public decimal? Total { get; set; }

        [Display(Name = nameof(Quantity), ResourceType = typeof(Resources.Domain.Shared))]
        public int Quantity { get; set; } = default;

        public Guid? PizzaInCartId { get; set; }
        public PizzaInCart? PizzaInCart { get; set; }

        public Guid? DrinkInCartId { get; set; }
        public DrinkInCart? DrinkInCart { get; set; }

        public Guid InvoiceId { get; set; } = default!;
        public Invoice? Invoice { get; set; }
    }
}