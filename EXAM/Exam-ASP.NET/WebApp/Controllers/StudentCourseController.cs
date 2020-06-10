using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class StudentCourseController : Controller
    {
        private readonly AppDbContext _context;

        public StudentCourseController(AppDbContext context)
        {
            _context = context;
        }

        // GET: StudentCourse
        //View for teachers
        public async Task<IActionResult> Index(Guid id)
        {
            var courses = await _context
                .StudentCourses
                .Include(s => s.AppUser)
                .Include(s => s.Course)
                .Where(ss => ss.CourseId == id)
                .ToListAsync();

            var vm = new StudentCourseViewModel()
            {
                StudentCourses = courses,
                CourseId = id
            };

            return View(vm);
        }

        public async Task<IActionResult> Search(StudentCourseViewModel vm)
        {
            vm.Search ??= "";

            var courses = await _context
                .StudentCourses
                .Include(s => s.AppUser)
                .Include(s => s.Course)
                .Where(ss => ss.CourseId == vm.CourseId
                             && string.Concat(ss.AppUser.FirstName, ss.AppUser.LastName).Trim().ToLower()
                                 .Contains(vm.Search.Trim().ToLower()))
                .ToListAsync();

            var vmReturn = new StudentCourseViewModel()
            {
                Search = vm.Search,
                CourseId = vm.CourseId,
                StudentCourses = courses
            };

            return View("Index", vmReturn);
        }

        // GET: StudentCourse/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentCourse = await _context.StudentCourses
                .Include(s => s.AppUser)
                .Include(s => s.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentCourse == null)
            {
                return NotFound();
            }

            return View(studentCourse);
        }

        // GET: StudentCourse/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentCourse/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AppUserId,CourseId,Accepted,Grade")]
            StudentCourse studentCourse)
        {
            if (ModelState.IsValid)
            {
                studentCourse.Id = Guid.NewGuid();
                _context.Add(studentCourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(studentCourse);
        }

        // GET: StudentCourse/Edit/5
        public async Task<IActionResult> EditStudentCourse(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentCourse = await _context.StudentCourses.FindAsync(id);
            if (studentCourse == null)
            {
                return NotFound();
            }

            return View(studentCourse);
        }

        // POST: StudentCourse/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStudentCourse(Guid id, StudentCourse studentCourse)
        {
            if (id != studentCourse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentCourseExists(studentCourse.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index), new {id = studentCourse.CourseId});
            }

            return View(studentCourse);
        }


        // POST: StudentCourse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var studentCourse = await _context.StudentCourses.FindAsync(id);
            _context.StudentCourses.Remove(studentCourse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /*If student wants to enroll, redirected to this controller */
        public async Task<IActionResult> Enroll(Guid id)
        {
            _context.Add(new StudentCourse()
            {
                Accepted = null,
                AppUserId = User.UserGuidId(),
                CourseId = id
            });
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Courses");
        }

        private bool StudentCourseExists(Guid id)
        {
            return _context.StudentCourses.Any(e => e.Id == id);
        }

        /* When teacher accepts/denies student's enrollment */
        public async Task<IActionResult> ProcessRequest(Guid id, string status, Guid courseId)
        {
            var ss = await _context.StudentCourses.FindAsync(id);
            if (ss == null)
            {
                return NotFound();
            }

            if (status == "true")
            {
                ss.Accepted = true;
            }
            else
            {
                ss.Accepted = false;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { id = courseId });
        }

        
    }
}