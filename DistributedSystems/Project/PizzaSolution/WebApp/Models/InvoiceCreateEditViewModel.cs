using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class InvoiceCreateEditViewModel
    {
        public Invoice Invoice { get; set; } = default!;

        public SelectList? InvoiceSelectList { get; set; }
    }
}