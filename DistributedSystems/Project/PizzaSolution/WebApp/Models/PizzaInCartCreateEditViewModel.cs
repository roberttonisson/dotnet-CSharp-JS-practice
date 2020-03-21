using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class PizzaInCartCreateEditViewModel
    {
        public PizzaInCart PizzaInCart { get; set; } = default!;

        public SelectList? PizzaTypeSelectList { get; set; }
        public SelectList? CrustSelectList { get; set; }
        public SelectList? SizeSelectList { get; set; }
        public SelectList? CartSelectList { get; set; }
    }
}