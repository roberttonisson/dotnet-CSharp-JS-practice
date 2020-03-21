using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class CrustCreateEditViewModel
    {
        public Crust Crust { get; set; } = default!;

        public SelectList? CrustSelectList { get; set; }
    }
}