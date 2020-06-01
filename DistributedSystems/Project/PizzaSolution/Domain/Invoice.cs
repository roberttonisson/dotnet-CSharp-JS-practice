using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


using Domain.Identity;
using ee.itcollege.rotoni.pizzaApp.DAL.Base;

namespace Domain
{
    public class Invoice : DomainEntityIdMetadataUser<AppUser>
    {

        [Display(Name = nameof(Total), ResourceType = typeof(Resources.Domain.Shared))]
        [NotMapped] public decimal? Total { get; set; }

        [Display(Name = nameof(IsPaid), ResourceType = typeof(Resources.Domain.Shared))]
        public bool IsPaid { get; set; } = false;
        
        public DateTime? Estimated { get; set; }
        
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public Guid TransportId { get; set; } = default!;
        public Transport? Transport { get; set; }
        
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public Guid OrderStatusId { get; set; } = default!;
        public OrderStatus? OrderStatus { get; set; }

        public ICollection<InvoiceLine>? InvoiceLines { get; set; }

        public ICollection<PartyOrderInvoice>? PartyOrderInvoices { get; set; }
    }
}