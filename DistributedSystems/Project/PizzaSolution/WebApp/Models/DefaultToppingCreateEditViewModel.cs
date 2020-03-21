using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class DefaultToppingCreateEditViewModel
    {
        public DefaultTopping DefaultTopping { get; set; } = default!;

        public SelectList? DefaultToppingSelectList { get; set; }
    }
}