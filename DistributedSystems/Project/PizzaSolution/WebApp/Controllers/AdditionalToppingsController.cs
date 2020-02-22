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
    public class AdditionalToppingsController : Controller
    {
        private readonly AppDbContext _context;

        public AdditionalToppingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AdditionalToppings
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.AdditionalToppings.Include(a => a.PizzaInCart).Include(a => a.Topping);
            return View(await appDbContext.ToListAsync());
        }

        // GET: AdditionalToppings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalTopping = await _context.AdditionalToppings
                .Include(a => a.PizzaInCart)
                .Include(a => a.Topping)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (additionalTopping == null)
            {
                return NotFound();
            }

            return View(additionalTopping);
        }

        // GET: AdditionalToppings/Create
        public IActionResult Create()
        {
            ViewData["PizzaInCartId"] = new SelectList(_context.PizzaInCarts, "Id", "Id");
            ViewData["ToppingId"] = new SelectList(_context.Toppings, "Id", "Id");
            return View();
        }

        // POST: AdditionalToppings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ToppingId,PizzaInCartId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] AdditionalTopping additionalTopping)
        {
            if (ModelState.IsValid)
            {
                _context.Add(additionalTopping);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PizzaInCartId"] = new SelectList(_context.PizzaInCarts, "Id", "Id", additionalTopping.PizzaInCartId);
            ViewData["ToppingId"] = new SelectList(_context.Toppings, "Id", "Id", additionalTopping.ToppingId);
            return View(additionalTopping);
        }

        // GET: AdditionalToppings/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalTopping = await _context.AdditionalToppings.FindAsync(id);
            if (additionalTopping == null)
            {
                return NotFound();
            }
            ViewData["PizzaInCartId"] = new SelectList(_context.PizzaInCarts, "Id", "Id", additionalTopping.PizzaInCartId);
            ViewData["ToppingId"] = new SelectList(_context.Toppings, "Id", "Id", additionalTopping.ToppingId);
            return View(additionalTopping);
        }

        // POST: AdditionalToppings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ToppingId,PizzaInCartId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] AdditionalTopping additionalTopping)
        {
            if (id != additionalTopping.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(additionalTopping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdditionalToppingExists(additionalTopping.Id))
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
            ViewData["PizzaInCartId"] = new SelectList(_context.PizzaInCarts, "Id", "Id", additionalTopping.PizzaInCartId);
            ViewData["ToppingId"] = new SelectList(_context.Toppings, "Id", "Id", additionalTopping.ToppingId);
            return View(additionalTopping);
        }

        // GET: AdditionalToppings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalTopping = await _context.AdditionalToppings
                .Include(a => a.PizzaInCart)
                .Include(a => a.Topping)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (additionalTopping == null)
            {
                return NotFound();
            }

            return View(additionalTopping);
        }

        // POST: AdditionalToppings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var additionalTopping = await _context.AdditionalToppings.FindAsync(id);
            _context.AdditionalToppings.Remove(additionalTopping);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdditionalToppingExists(string id)
        {
            return _context.AdditionalToppings.Any(e => e.Id == id);
        }
    }
}
