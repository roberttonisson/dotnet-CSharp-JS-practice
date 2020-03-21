using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class PartyOrderCreateEditViewModel
    {
        public PartyOrder PartyOrder { get; set; } = default!;

        public SelectList? PartyOrderSelectList { get; set; }
    }
}