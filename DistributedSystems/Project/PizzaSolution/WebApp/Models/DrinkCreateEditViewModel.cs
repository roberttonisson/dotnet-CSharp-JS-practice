using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class DrinkCreateEditViewModel
    {
        public Drink Drink { get; set; } = default!;

        public SelectList? DrinkSelectList { get; set; }
    }
}