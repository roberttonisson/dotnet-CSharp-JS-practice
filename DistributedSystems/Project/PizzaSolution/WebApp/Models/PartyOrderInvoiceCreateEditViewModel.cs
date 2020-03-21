using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class PartyOrderInvoiceCreateEditViewModel
    {
        public PartyOrderInvoice PartyOrderInvoice { get; set; } = default!;

        public SelectList? PartyOrderInvoiceSelectList { get; set; }
    }
}