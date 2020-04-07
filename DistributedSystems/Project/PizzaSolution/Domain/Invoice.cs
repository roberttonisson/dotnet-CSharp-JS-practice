using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Invoice : DomainEntity
    {
        [Display(Name = nameof(Total), ResourceType = typeof(Resources.Domain.Shared))]
        [NotMapped] public decimal Total { get; set; }

        [Display(Name = nameof(IsPaid), ResourceType = typeof(Resources.Domain.Shared))]
        public bool IsPaid { get; set; } = false;

        [ForeignKey("AppUser")]
        public Guid UserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        
        public Guid TransportId { get; set; } = default!;
        public Transport? Transport { get; set; }

        public ICollection<InvoiceLine>? InvoiceLines { get; set; }

        public ICollection<PartyOrderInvoice>? PartyOrderInvoices { get; set; }
    }
}