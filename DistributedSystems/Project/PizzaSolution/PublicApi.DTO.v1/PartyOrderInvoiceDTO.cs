using System;
using DAL.Base;
using Domain;

namespace PublicApi.DTO.v1
{
    public class PartyOrderInvoiceDTO : DomainEntityId
    {
        public Guid PartyOrderId { get; set; } = default!;
        public PartyOrderDTO? PartyOrder { get; set; }

        public Guid InvoiceId { get; set; } = default!;
        public InvoiceDTO? Invoice { get; set; }
    }
}