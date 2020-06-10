using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class HomeworkController : Controller
    {
        private readonly AppDbContext _context;

        public HomeworkController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Homework
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Homework.Include(h => h.Course);
            return View(await appDbContext.ToListAsync());
        }

        public async Task<IActionResult> StudentIndex(Guid id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(s => s.Id == id);
            var homework = await _context.Homework
                .Where(hw => hw.CourseId == id)
                .ToListAsync();
            
            var vm = new HomeworkIndexViewModel()
            {
                Homework = homework,
                Course = course
            };
            
            return View(vm);
        }

        public async Task<IActionResult> TeacherIndex(Guid id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(s => s.Id == id);
            var homework = await _context.Homework
                .Where(hw => hw.CourseId == id)
                .ToListAsync();
            
            var vm = new HomeworkIndexViewModel()
            {
                Homework = homework,
                Course = course
            };
            
            return View(vm);
        }

        // GET: Homework/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homework = await _context.Homework
                .Include(h => h.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homework == null)
            {
                return NotFound();
            }

            return View(homework);
        }

        // GET: Homework/Create
        public IActionResult Create(Guid id)
        {
            return View(new Homework()
            {
                CourseId = id
            });
        }

        // POST: Homework/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Homework homework)
        {
            if (ModelState.IsValid)
            {
                homework.Id = Guid.NewGuid();
                _context.Add(homework);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(TeacherIndex), new { id = homework.CourseId});
            }
            return View(homework);
        }

        // GET: Homework/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homework = await _context.Homework.FindAsync(id);
            if (homework == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Code", homework.CourseId);
            return View(homework);
        }

        // POST: Homework/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Homework homework)
        {
            if (id != homework.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homework);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeworkExists(homework.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Code", homework.CourseId);
            return View(homework);
        }

        // GET: Homework/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homework = await _context.Homework
                .Include(h => h.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homework == null)
            {
                return NotFound();
            }

            return View(homework);
        }

        // POST: Homework/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var homework = await _context.Homework.FindAsync(id);
            _context.Homework.Remove(homework);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeworkExists(Guid id)
        {
            return _context.Homework.Any(e => e.Id == id);
        }
    }
}
