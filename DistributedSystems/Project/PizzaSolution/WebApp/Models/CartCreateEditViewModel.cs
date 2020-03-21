using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class CartCreateEditViewModel
    {
        public Cart Cart { get; set; } = default!;

        public SelectList? AppUserSelectList { get; set; }
    }
}