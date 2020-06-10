using System.Collections.Generic;
using Domain;
using Domain.Identity;

namespace WebApp.ViewModels
{
    public class UsersViewModel
    {
        public IEnumerable<AppUser> Teachers { get; set; } = default!;
        public string? Search { get; set; }
    }
}