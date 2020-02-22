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
    public class PizzaInCartsController : Controller
    {
        private readonly AppDbContext _context;

        public PizzaInCartsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PizzaInCarts
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.PizzaInCarts.Include(p => p.Cart).Include(p => p.Crust).Include(p => p.PizzaType).Include(p => p.Size);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PizzaInCarts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaInCart = await _context.PizzaInCarts
                .Include(p => p.Cart)
                .Include(p => p.Crust)
                .Include(p => p.PizzaType)
                .Include(p => p.Size)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizzaInCart == null)
            {
                return NotFound();
            }

            return View(pizzaInCart);
        }

        // GET: PizzaInCarts/Create
        public IActionResult Create()
        {
            ViewData["CartId"] = new SelectList(_context.Carts, "Id", "Id");
            ViewData["CrustId"] = new SelectList(_context.Crusts, "Id", "Id");
            ViewData["PizzaTypeId"] = new SelectList(_context.PizzaTypes, "Id", "Id");
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Id");
            return View();
        }

        // POST: PizzaInCarts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Quantity,PizzaTypeId,CrustId,SizeId,CartId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] PizzaInCart pizzaInCart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pizzaInCart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(_context.Carts, "Id", "Id", pizzaInCart.CartId);
            ViewData["CrustId"] = new SelectList(_context.Crusts, "Id", "Id", pizzaInCart.CrustId);
            ViewData["PizzaTypeId"] = new SelectList(_context.PizzaTypes, "Id", "Id", pizzaInCart.PizzaTypeId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Id", pizzaInCart.SizeId);
            return View(pizzaInCart);
        }

        // GET: PizzaInCarts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaInCart = await _context.PizzaInCarts.FindAsync(id);
            if (pizzaInCart == null)
            {
                return NotFound();
            }
            ViewData["CartId"] = new SelectList(_context.Carts, "Id", "Id", pizzaInCart.CartId);
            ViewData["CrustId"] = new SelectList(_context.Crusts, "Id", "Id", pizzaInCart.CrustId);
            ViewData["PizzaTypeId"] = new SelectList(_context.PizzaTypes, "Id", "Id", pizzaInCart.PizzaTypeId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Id", pizzaInCart.SizeId);
            return View(pizzaInCart);
        }

        // POST: PizzaInCarts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Quantity,PizzaTypeId,CrustId,SizeId,CartId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] PizzaInCart pizzaInCart)
        {
            if (id != pizzaInCart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pizzaInCart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PizzaInCartExists(pizzaInCart.Id))
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
            ViewData["CartId"] = new SelectList(_context.Carts, "Id", "Id", pizzaInCart.CartId);
            ViewData["CrustId"] = new SelectList(_context.Crusts, "Id", "Id", pizzaInCart.CrustId);
            ViewData["PizzaTypeId"] = new SelectList(_context.PizzaTypes, "Id", "Id", pizzaInCart.PizzaTypeId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Id", pizzaInCart.SizeId);
            return View(pizzaInCart);
        }

        // GET: PizzaInCarts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaInCart = await _context.PizzaInCarts
                .Include(p => p.Cart)
                .Include(p => p.Crust)
                .Include(p => p.PizzaType)
                .Include(p => p.Size)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizzaInCart == null)
            {
                return NotFound();
            }

            return View(pizzaInCart);
        }

        // POST: PizzaInCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var pizzaInCart = await _context.PizzaInCarts.FindAsync(id);
            _context.PizzaInCarts.Remove(pizzaInCart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PizzaInCartExists(string id)
        {
            return _context.PizzaInCarts.Any(e => e.Id == id);
        }
    }
}
