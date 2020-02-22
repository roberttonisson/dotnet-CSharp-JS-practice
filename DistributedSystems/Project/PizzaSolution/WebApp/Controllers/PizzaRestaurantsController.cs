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
    public class PizzaRestaurantsController : Controller
    {
        private readonly AppDbContext _context;

        public PizzaRestaurantsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PizzaRestaurants
        public async Task<IActionResult> Index()
        {
            return View(await _context.PizzaRestaurants.ToListAsync());
        }

        // GET: PizzaRestaurants/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaRestaurant = await _context.PizzaRestaurants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizzaRestaurant == null)
            {
                return NotFound();
            }

            return View(pizzaRestaurant);
        }

        // GET: PizzaRestaurants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PizzaRestaurants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Address,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] PizzaRestaurant pizzaRestaurant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pizzaRestaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pizzaRestaurant);
        }

        // GET: PizzaRestaurants/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaRestaurant = await _context.PizzaRestaurants.FindAsync(id);
            if (pizzaRestaurant == null)
            {
                return NotFound();
            }
            return View(pizzaRestaurant);
        }

        // POST: PizzaRestaurants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Address,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] PizzaRestaurant pizzaRestaurant)
        {
            if (id != pizzaRestaurant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pizzaRestaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PizzaRestaurantExists(pizzaRestaurant.Id))
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
            return View(pizzaRestaurant);
        }

        // GET: PizzaRestaurants/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaRestaurant = await _context.PizzaRestaurants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizzaRestaurant == null)
            {
                return NotFound();
            }

            return View(pizzaRestaurant);
        }

        // POST: PizzaRestaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var pizzaRestaurant = await _context.PizzaRestaurants.FindAsync(id);
            _context.PizzaRestaurants.Remove(pizzaRestaurant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PizzaRestaurantExists(string id)
        {
            return _context.PizzaRestaurants.Any(e => e.Id == id);
        }
    }
}
