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
    public class DefaultToppingsController : Controller
    {
        private readonly AppDbContext _context;

        public DefaultToppingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DefaultToppings
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.DefaultToppings.Include(d => d.PizzaType).Include(d => d.Topping);
            return View(await appDbContext.ToListAsync());
        }

        // GET: DefaultToppings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defaultTopping = await _context.DefaultToppings
                .Include(d => d.PizzaType)
                .Include(d => d.Topping)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (defaultTopping == null)
            {
                return NotFound();
            }

            return View(defaultTopping);
        }

        // GET: DefaultToppings/Create
        public IActionResult Create()
        {
            ViewData["PizzaTypeId"] = new SelectList(_context.PizzaTypes, "Id", "Id");
            ViewData["ToppingId"] = new SelectList(_context.Toppings, "Id", "Id");
            return View();
        }

        // POST: DefaultToppings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ToppingId,PizzaTypeId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] DefaultTopping defaultTopping)
        {
            if (ModelState.IsValid)
            {
                _context.Add(defaultTopping);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PizzaTypeId"] = new SelectList(_context.PizzaTypes, "Id", "Id", defaultTopping.PizzaTypeId);
            ViewData["ToppingId"] = new SelectList(_context.Toppings, "Id", "Id", defaultTopping.ToppingId);
            return View(defaultTopping);
        }

        // GET: DefaultToppings/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defaultTopping = await _context.DefaultToppings.FindAsync(id);
            if (defaultTopping == null)
            {
                return NotFound();
            }
            ViewData["PizzaTypeId"] = new SelectList(_context.PizzaTypes, "Id", "Id", defaultTopping.PizzaTypeId);
            ViewData["ToppingId"] = new SelectList(_context.Toppings, "Id", "Id", defaultTopping.ToppingId);
            return View(defaultTopping);
        }

        // POST: DefaultToppings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ToppingId,PizzaTypeId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] DefaultTopping defaultTopping)
        {
            if (id != defaultTopping.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(defaultTopping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DefaultToppingExists(defaultTopping.Id))
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
            ViewData["PizzaTypeId"] = new SelectList(_context.PizzaTypes, "Id", "Id", defaultTopping.PizzaTypeId);
            ViewData["ToppingId"] = new SelectList(_context.Toppings, "Id", "Id", defaultTopping.ToppingId);
            return View(defaultTopping);
        }

        // GET: DefaultToppings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defaultTopping = await _context.DefaultToppings
                .Include(d => d.PizzaType)
                .Include(d => d.Topping)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (defaultTopping == null)
            {
                return NotFound();
            }

            return View(defaultTopping);
        }

        // POST: DefaultToppings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var defaultTopping = await _context.DefaultToppings.FindAsync(id);
            _context.DefaultToppings.Remove(defaultTopping);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DefaultToppingExists(string id)
        {
            return _context.DefaultToppings.Any(e => e.Id == id);
        }
    }
}
