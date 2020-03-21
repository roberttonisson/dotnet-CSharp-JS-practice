using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
    public class PartyOrderInvoice : DomainEntity
    {
        public Guid PartyOrderId { get; set; } = default!;
        public PartyOrder? PartyOrder { get; set; }

        public Guid InvoiceId { get; set; } = default!;
        public Invoice? Invoice { get; set; }
    }
}