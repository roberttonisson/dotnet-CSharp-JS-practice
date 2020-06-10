using System.Collections.Generic;
using Domain.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class UserEditCreateViewModel
    {
        public AppUser AppUser { get; set; } = default!;
        public SelectList? RoleSelectList { get; set; }
        public string RoleId { get; set; } = default!;
    }
}