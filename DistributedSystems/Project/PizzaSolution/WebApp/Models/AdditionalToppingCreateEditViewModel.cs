using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class AdditionalToppingCreateEditViewModel
    {
        public AdditionalTopping AdditionalTopping { get; set; } = default!;

        public SelectList? PizzaInCartSelectList { get; set; }
        
        public SelectList? ToppingSelectList { get; set; }
    }
}