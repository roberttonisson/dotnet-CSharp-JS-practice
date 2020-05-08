using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;

namespace DAL.App.DTO
{
    public class PartyOrderInvoice : DomainEntityIdMetadata
    {
        public Guid PartyOrderId { get; set; } = default!;
        public PartyOrder? PartyOrder { get; set; }

        public Guid InvoiceId { get; set; } = default!;
        public Invoice? Invoice { get; set; }
    }
}