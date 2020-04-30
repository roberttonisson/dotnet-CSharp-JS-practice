using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BLL.App.DTO
{
    public class PartyOrderInvoice : DomainEntityIdMetadata
    {
        public Guid PartyOrderId { get; set; } = default!;
        public Domain. PartyOrder? PartyOrder { get; set; }

        public Guid InvoiceId { get; set; } = default!;
        public Domain.Invoice? Invoice { get; set; }
        
        public SelectList? PartyOrderSelectList { get; set; }
        public SelectList? InvoiceSelectList { get; set; }
        
    }
}