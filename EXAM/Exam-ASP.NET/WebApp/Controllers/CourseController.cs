using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using DAL.App.EF.Helpers;
using Domain;
using Domain.Identity;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CoursesController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses
                .Include(s => s.StudentCourses)
                .Include(s => s.AppUser)
                .ToListAsync();


            return View(new CourseViewModel()
            {
                Search = "",
                Courses = courses
            });
        }

        public async Task<IActionResult> AdminIndex()
        {
            return View(await _context.Courses.Include(s => s.AppUser).ToListAsync());
        }

        public async Task<IActionResult> Search(CourseViewModel vm)
        {
            vm.Search ??= "";
            var courses =
                await _context.Courses
                    .Include(s => s.StudentCourses)
                    .Include(s => s.AppUser)
                    .ToListAsync();

            courses = courses.Where(s => s.Name.ToLower().Contains(vm.Search.ToLower()) ||
                                           s.Code.ToLower().Contains(vm.Search.ToLower()) ||
                                           s.AppUser!.FirstName.ToLower().Contains(vm.Search.ToLower()) ||
                                           s.AppUser!.LastName.ToLower().Contains(vm.Search.ToLower()) ||
                                           s.AppUser!.FirstLastName.ToLower().Contains(vm.Search.ToLower())).ToList();
            var filtered = new CourseViewModel()
            {
                Search = vm.Search,
                Courses = courses
            };
            return View("Index", filtered);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> TeacherIndex(Guid id)
        {
            if (id != User.UserGuidId())
            {
                return NotFound();
            }

            var teacherCourses =
                await _context.Courses.Include(s => s.AppUser)
                    .Where(s => s.AppUserId == id)
                    .ToListAsync();

            return View(teacherCourses);
        }

        public async Task<IActionResult> StudentIndex()
        {
            var studentCourses =
                await _context.StudentCourses
                    .Include(s => s.Course)
                    .Where(s => s.AppUserId == User.UserGuidId() && s.Accepted == true)
                    .ToListAsync();

            var vm = new StudentCourseFilterViewModel()
            {
                StudentCourses = studentCourses
            };

            return View(vm);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public async Task<IActionResult> Create()
        {
            var vm = new CourseCreateEditViewModel()
            {
                TeacherSelectList =
                    new SelectList(await GetTeachers(), nameof(AppUser.Id), nameof(AppUser.FirstLastName))
            };
            return View(vm);
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseCreateEditViewModel courseCreateEditViewModel)
        {
            if (ModelState.IsValid)
            {
                courseCreateEditViewModel.Course.Id = Guid.NewGuid();
                _context.Courses.Add(courseCreateEditViewModel.Course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AdminIndex));
            }

            return View(courseCreateEditViewModel);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FirstOrDefaultAsync(s => s.Id == id);
            var teachers = await GetTeachers();

            var vm = new CourseCreateEditViewModel()
            {
                Course = course,
                TeacherSelectList = course.AppUserId == null
                    ? new SelectList(teachers, nameof(AppUser.Id), nameof(AppUser.FirstLastName))
                    : new SelectList(teachers, nameof(AppUser.Id), nameof(AppUser.FirstLastName), course.AppUserId)
            };


            if (course == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            var vm = new CourseCreateEditViewModel();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(AdminIndex));
            }

            return View(vm);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AdminIndex));
        }

        private bool CourseExists(Guid id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }

        private async Task<IEnumerable<AppUser>> GetTeachers()
        {
            return (await _userManager.Users.ToListAsync()).Where(u =>
                _userManager.IsInRoleAsync(u, RoleNames.RoleTeacher).Result);
        }

        public async Task<IActionResult> Filter(StudentCourseFilterViewModel vm)
        {
            var studentCourses = _context.StudentCourses
                .Include(s => s.Course)
                .Where(s => s.AppUserId == User.UserGuidId() && s.Accepted == true)
                .AsQueryable();

            if (vm.Year != 0)
            {
                studentCourses = studentCourses
                    .Where(s => s.Course!.Year == vm.Year)
                    .AsQueryable();
            }

            if (vm.Semester != null)
            {
                studentCourses = studentCourses
                    .Where(s => s.Course!.Semester == vm.Semester)
                    .AsQueryable();
            }


            var ret = new StudentCourseFilterViewModel()
            {
                StudentCourses = await studentCourses.ToListAsync(),
                Semester = vm.Semester,
                Year = vm.Year
            };

            return View("StudentIndex", ret);
        }
    }
}