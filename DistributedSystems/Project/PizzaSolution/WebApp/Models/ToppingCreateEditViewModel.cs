using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class ToppingCreateEditViewModel
    {
        public Topping Topping { get; set; } = default!;

        public SelectList? ToppingSelectList { get; set; }
    }
}