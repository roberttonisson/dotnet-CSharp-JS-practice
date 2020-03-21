using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class DrinkInCartCreateEditViewModel
    {
        public DrinkInCart DrinkInCart { get; set; } = default!;

        public SelectList? DrinkInCartSelectList { get; set; }
    }
}