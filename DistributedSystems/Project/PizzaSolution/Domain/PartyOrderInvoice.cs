using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
    public class PartyOrderInvoice: DomainEntityMetadata
    {
        [MaxLength(36)] public string PartyOrderId { get; set; } = default!;
        public PartyOrder? PartyOrder { get; set; }
        
        [MaxLength(36)] public string InvoiceId { get; set; }  = default!;
        public Invoice? Invoice { get; set; }
    }
}