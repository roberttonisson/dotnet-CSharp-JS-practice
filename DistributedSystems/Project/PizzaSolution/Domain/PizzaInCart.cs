using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class PizzaInCart : DomainEntityIdMetadata
    {
        [Display(Name = nameof(Price), ResourceType = typeof(Resources.Domain.Shared))]
        [NotMapped] public decimal? Price { get; set; }

        [Display(Name = nameof(Quantity), ResourceType = typeof(Resources.Domain.Shared))]
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
        public ICollection<AdditionalTopping>? AdditionalToppings { get; set; }
    }
}