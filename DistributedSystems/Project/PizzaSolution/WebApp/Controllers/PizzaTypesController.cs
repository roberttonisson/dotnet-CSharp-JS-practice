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
    public class PizzaTypesController : Controller
    {
        private readonly AppDbContext _context;

        public PizzaTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PizzaTypes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.PizzaTypes.Include(p => p.PizzaRestaurant);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PizzaTypes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaType = await _context.PizzaTypes
                .Include(p => p.PizzaRestaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizzaType == null)
            {
                return NotFound();
            }

            return View(pizzaType);
        }

        // GET: PizzaTypes/Create
        public IActionResult Create()
        {
            ViewData["PizzaRestaurantId"] = new SelectList(_context.PizzaRestaurants, "Id", "Id");
            return View();
        }

        // POST: PizzaTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price,PizzaRestaurantId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] PizzaType pizzaType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pizzaType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PizzaRestaurantId"] = new SelectList(_context.PizzaRestaurants, "Id", "Id", pizzaType.PizzaRestaurantId);
            return View(pizzaType);
        }

        // GET: PizzaTypes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaType = await _context.PizzaTypes.FindAsync(id);
            if (pizzaType == null)
            {
                return NotFound();
            }
            ViewData["PizzaRestaurantId"] = new SelectList(_context.PizzaRestaurants, "Id", "Id", pizzaType.PizzaRestaurantId);
            return View(pizzaType);
        }

        // POST: PizzaTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Price,PizzaRestaurantId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] PizzaType pizzaType)
        {
            if (id != pizzaType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pizzaType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PizzaTypeExists(pizzaType.Id))
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
            ViewData["PizzaRestaurantId"] = new SelectList(_context.PizzaRestaurants, "Id", "Id", pizzaType.PizzaRestaurantId);
            return View(pizzaType);
        }

        // GET: PizzaTypes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaType = await _context.PizzaTypes
                .Include(p => p.PizzaRestaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizzaType == null)
            {
                return NotFound();
            }

            return View(pizzaType);
        }

        // POST: PizzaTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var pizzaType = await _context.PizzaTypes.FindAsync(id);
            _context.PizzaTypes.Remove(pizzaType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PizzaTypeExists(string id)
        {
            return _context.PizzaTypes.Any(e => e.Id == id);
        }
    }
}
