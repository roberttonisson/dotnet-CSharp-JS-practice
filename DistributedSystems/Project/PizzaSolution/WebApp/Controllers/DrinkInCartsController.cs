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
    public class DrinkInCartsController : Controller
    {
        private readonly AppDbContext _context;

        public DrinkInCartsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DrinkInCarts
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.DrinkInCarts.Include(d => d.Cart).Include(d => d.Drink);
            return View(await appDbContext.ToListAsync());
        }

        // GET: DrinkInCarts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkInCart = await _context.DrinkInCarts
                .Include(d => d.Cart)
                .Include(d => d.Drink)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drinkInCart == null)
            {
                return NotFound();
            }

            return View(drinkInCart);
        }

        // GET: DrinkInCarts/Create
        public IActionResult Create()
        {
            ViewData["CartId"] = new SelectList(_context.Carts, "Id", "Id");
            ViewData["DrinkId"] = new SelectList(_context.Drinks, "Id", "Id");
            return View();
        }

        // POST: DrinkInCarts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Quantity,DrinkId,CartId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] DrinkInCart drinkInCart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drinkInCart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(_context.Carts, "Id", "Id", drinkInCart.CartId);
            ViewData["DrinkId"] = new SelectList(_context.Drinks, "Id", "Id", drinkInCart.DrinkId);
            return View(drinkInCart);
        }

        // GET: DrinkInCarts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkInCart = await _context.DrinkInCarts.FindAsync(id);
            if (drinkInCart == null)
            {
                return NotFound();
            }
            ViewData["CartId"] = new SelectList(_context.Carts, "Id", "Id", drinkInCart.CartId);
            ViewData["DrinkId"] = new SelectList(_context.Drinks, "Id", "Id", drinkInCart.DrinkId);
            return View(drinkInCart);
        }

        // POST: DrinkInCarts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Quantity,DrinkId,CartId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] DrinkInCart drinkInCart)
        {
            if (id != drinkInCart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drinkInCart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrinkInCartExists(drinkInCart.Id))
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
            ViewData["CartId"] = new SelectList(_context.Carts, "Id", "Id", drinkInCart.CartId);
            ViewData["DrinkId"] = new SelectList(_context.Drinks, "Id", "Id", drinkInCart.DrinkId);
            return View(drinkInCart);
        }

        // GET: DrinkInCarts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkInCart = await _context.DrinkInCarts
                .Include(d => d.Cart)
                .Include(d => d.Drink)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drinkInCart == null)
            {
                return NotFound();
            }

            return View(drinkInCart);
        }

        // POST: DrinkInCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var drinkInCart = await _context.DrinkInCarts.FindAsync(id);
            _context.DrinkInCarts.Remove(drinkInCart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrinkInCartExists(string id)
        {
            return _context.DrinkInCarts.Any(e => e.Id == id);
        }
    }
}
