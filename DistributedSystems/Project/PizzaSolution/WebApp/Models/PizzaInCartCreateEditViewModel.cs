using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class PizzaInCartCreateEditViewModel
    {
        public PizzaInCart PizzaInCart { get; set; } = default!;

        public SelectList? PizzaInCartSelectList { get; set; }
    }
}