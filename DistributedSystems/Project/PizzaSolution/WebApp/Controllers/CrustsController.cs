using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers
{
    public class CrustsController : Controller
    {
        private readonly AppDbContext _context;

        public CrustsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Crusts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Crusts.ToListAsync());
        }

        // GET: Crusts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crust = await _context.Crusts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crust == null)
            {
                return NotFound();
            }

            return View(crust);
        }

        // GET: Crusts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Crusts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] Crust crust)
        {
            if (ModelState.IsValid)
            {
                _context.Add(crust);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(crust);
        }

        // GET: Crusts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crust = await _context.Crusts.FindAsync(id);
            if (crust == null)
            {
                return NotFound();
            }
            return View(crust);
        }

        // POST: Crusts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Price,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] Crust crust)
        {
            if (id != crust.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(crust);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CrustExists(crust.Id))
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
            return View(crust);
        }

        // GET: Crusts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crust = await _context.Crusts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crust == null)
            {
                return NotFound();
            }

            return View(crust);
        }

        // POST: Crusts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var crust = await _context.Crusts.FindAsync(id);
            _context.Crusts.Remove(crust);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CrustExists(string id)
        {
            return _context.Crusts.Any(e => e.Id == id);
        }
    }
}
