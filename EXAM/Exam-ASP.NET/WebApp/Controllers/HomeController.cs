using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using DAL.App.EF.Helpers;
using Domain;
using Domain.Identity;
using Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, AppDbContext context)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new HomeIndexViewModel();
            if (User.IsInRole(RoleNames.RoleStudent))
            {
                vm = new HomeIndexViewModel
                {
                    StudentHomework = await _context.StudentHomework
                        .Include(x => x.AppUser)
                        .Include(x => x.Homework)
                        .ThenInclude(x => x!.Course)
                        .Where(x => x.AppUserId == User.UserGuidId() && x.Grade == null)
                        .ToListAsync(),
                    StudentCourses = await _context.StudentCourses
                        .Include(x => x.AppUser)
                        .Include(x => x.Course)
                        .ThenInclude(x => x!.Homework)
                        .ThenInclude(x => x.StudentHomework)
                        .Where(x => x.AppUserId == User.UserGuidId())
                        .ToListAsync()
                };
            }
            
            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
        
        
    }
}