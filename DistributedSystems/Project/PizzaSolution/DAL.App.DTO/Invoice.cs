using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;
using DAL.Base;

namespace DAL.App.DTO
{
    public class Invoice : DomainEntityIdMetadataUser<Domain.Identity.AppUser>
    {

        [Display(Name = nameof(Total), ResourceType = typeof(Resources.Domain.Shared))]
        [NotMapped] public decimal? Total { get; set; }

        [Display(Name = nameof(IsPaid), ResourceType = typeof(Resources.Domain.Shared))]
        public bool IsPaid { get; set; } = false;
        
        public Guid TransportId { get; set; } = default!;
        public Transport? Transport { get; set; }

        public ICollection<InvoiceLine>? InvoiceLines { get; set; }

        public ICollection<PartyOrderInvoice>? PartyOrderInvoices { get; set; }
    }
}