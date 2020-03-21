using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class PizzaTypeCreateEditViewModel
    {
        public PizzaType PizzaType { get; set; } = default!;
    }
}