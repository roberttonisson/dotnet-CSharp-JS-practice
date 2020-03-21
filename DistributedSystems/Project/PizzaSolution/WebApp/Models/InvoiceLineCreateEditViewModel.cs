using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class InvoiceLineCreateEditViewModel
    {
        public InvoiceLine InvoiceLine { get; set; } = default!;

        public SelectList? PizzaInCartSelectList { get; set; }
        public SelectList? DrinkInCartSelectList { get; set; }
        public SelectList? InvoiceSelectList { get; set; }
        
    }
}