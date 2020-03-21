using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class TransportCreateEditViewModel
    {
        public Transport Transport { get; set; } = default!;

        public SelectList? TransportSelectList { get; set; }
    }
}