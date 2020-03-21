using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class SizeCreateEditViewModel
    {
        public Size Size { get; set; } = default!;

        public SelectList? SizeSelectList { get; set; }
    }
}