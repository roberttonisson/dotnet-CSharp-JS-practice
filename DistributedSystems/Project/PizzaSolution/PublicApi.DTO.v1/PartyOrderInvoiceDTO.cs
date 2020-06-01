using System;

using Domain;
using ee.itcollege.rotoni.pizzaApp.DAL.Base;

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