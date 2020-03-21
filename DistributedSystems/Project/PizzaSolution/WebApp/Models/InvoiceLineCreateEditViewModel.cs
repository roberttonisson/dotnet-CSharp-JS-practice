using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class InvoiceLineCreateEditViewModel
    {
        public InvoiceLine InvoiceLine { get; set; } = default!;

        public SelectList? InvoiceLineSelectList { get; set; }
    }
}