using System;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using DAL.App.EF.Helpers;
using Domain;
using Domain.Identity;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class UsersController : Controller
    {
        
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public UsersController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        
        public async Task<IActionResult> Teachers()
        {
            var users = await _userManager.Users.ToListAsync();
            users = users.Where(IsTeacher).ToList();
            return View(new UsersViewModel
            {
                Teachers = users,
                Search = ""
            });
        }


        public bool IsTeacher(AppUser user)
        {
            var isTeacher = _userManager.IsInRoleAsync(user, RoleNames.RoleTeacher).Result;
            return isTeacher;
        }
        
        public async Task<IActionResult> Search(CourseViewModel vm)
        {
            vm.Search ??= "";
            var teachers = await _userManager.Users
                .Where(s => s.FirstName.ToLower().Contains(vm.Search.ToLower()) || s.LastName.ToLower().Contains(vm.Search.ToLower()))
                .ToListAsync();
            teachers = teachers.Where(IsTeacher).ToList();

            var filtered = new UsersViewModel()
            {
                Search = vm.Search,
                Teachers = teachers
            };
            return View("Teachers", filtered);
        }
        
        
    }
}