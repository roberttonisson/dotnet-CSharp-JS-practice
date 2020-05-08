using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.App.DTO;
using DAL.Base;

namespace DAL.App.DTO
{
    
    public class DrinkInCart : DomainEntityIdMetadata
    {
        [Display(Name = nameof(Quantity), ResourceType = typeof(Resources.Domain.Shared))]
        public int Quantity { get; set; } = default;

        [Display(Name = nameof(Price), ResourceType = typeof(Resources.Domain.Shared))]
        [NotMapped] public decimal? Price { get; set; }

        public Guid DrinkId { get; set; } = default!;
        public Drink? Drink { get; set; }

        public Guid CartId { get; set; } = default!;
        public Cart? Cart { get; set; }

        public ICollection<InvoiceLine>? InvoiceLines { get; set; }
    }
}