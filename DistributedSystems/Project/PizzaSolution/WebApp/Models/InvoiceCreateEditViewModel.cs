using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class InvoiceCreateEditViewModel
    {
        public Invoice Invoice { get; set; } = default!;

        public SelectList? AppUserSelectList { get; set; }
        public SelectList? TransportSelectList { get; set; }
    }
}