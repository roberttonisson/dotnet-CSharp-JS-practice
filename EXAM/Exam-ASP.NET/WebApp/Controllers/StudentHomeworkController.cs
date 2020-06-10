using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize]
    public class StudentHomeworkController : Controller
    {
        private readonly AppDbContext _context;

        public StudentHomeworkController(AppDbContext context)
        {
            _context = context;
        }
        
        /* Single homework view */
        public async Task<IActionResult> StudentIndex(Guid id)
        {
            var shw = await _context.StudentHomework
                .Include(s => s.Homework)
                .FirstOrDefaultAsync(sh =>
                sh.HomeworkId == id && sh.AppUserId == User.UserGuidId());

            var vm = shw ?? new StudentHomework()
            {
                HomeworkId = id,
                Homework = await _context.Homework.FirstOrDefaultAsync(h => h.Id == id),
                AppUserId = User.UserGuidId(),
            };
            
            return View(vm);
        }
        /* Student responses to the homework */
        public async Task<IActionResult> HomeworkTeacherIndex(Guid id)
        {
            var shw = await _context.StudentHomework
                .Include(s => s.AppUser)
                .Where(s => s.HomeworkId == id)
                .ToListAsync();

            return View(shw);
        }

        // GET: StudentHomework/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentHomework = await _context.StudentHomework
                .Include(s => s.AppUser)
                .Include(s => s.Homework)
                .ThenInclude(ss => ss!.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentHomework == null)
            {
                return NotFound();
            }

            return View(studentHomework);
        }

        // POST: StudentHomework/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentHomework studentHomework)
        {
            if (ModelState.IsValid)
            {
                studentHomework.Id = Guid.NewGuid();
                _context.Add(studentHomework);
                await _context.SaveChangesAsync();
                return RedirectToAction("StudentIndex", new { id = studentHomework.HomeworkId });
            }
            
            return View(studentHomework);
        }

        // GET: StudentHomework/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentHomework = await _context.StudentHomework.FindAsync(id);
            if (studentHomework == null)
            {
                return NotFound();
            }
            return View(studentHomework);
        }

        // POST: StudentHomework/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, StudentHomework studentHomework)
        {
            if (id != studentHomework.Id)
            {
                return NotFound();
            }

            studentHomework.GradedAt = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentHomework);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentHomeworkExists(studentHomework.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(HomeworkTeacherIndex), new {id = studentHomework.HomeworkId});
            }
            return View(studentHomework);
        }

        // GET: StudentHomework/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentHomework = await _context.StudentHomework
                .Include(s => s.AppUser)
                .Include(s => s.Homework)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentHomework == null)
            {
                return NotFound();
            }

            return View(studentHomework);
        }

        // POST: StudentHomework/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var studentHomework = await _context.StudentHomework.FindAsync(id);
            _context.StudentHomework.Remove(studentHomework);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(HomeworkTeacherIndex), new {id = studentHomework.HomeworkId});
        }

        private bool StudentHomeworkExists(Guid id)
        {
            return _context.StudentHomework.Any(e => e.Id == id);
        }
    }
}
