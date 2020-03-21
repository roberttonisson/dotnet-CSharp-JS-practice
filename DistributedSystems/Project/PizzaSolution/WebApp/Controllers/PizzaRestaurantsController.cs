using System;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;

namespace WebApp.Controllers
{
    public class PizzaRestaurantsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPizzaRestaurantRepository _pizzaRestaurantRepository;

        public PizzaRestaurantsController(AppDbContext context)
        {
            _context = context;
            _pizzaRestaurantRepository = new PizzaRestaurantRepository(_context);
        }

        // GET: PizzaRestaurants
        public async Task<IActionResult> Index()
        {
            return View(await _pizzaRestaurantRepository.AllAsync());
        }

        // GET: PizzaRestaurants/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaRestaurant = await _pizzaRestaurantRepository.FindAsync(id);

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
        public async Task<IActionResult> Create(
            [Bind("Name,Address,CreatedBy,CreatedAt,CreatedBy,CreatedAt,Id")]
            PizzaRestaurant pizzaRestaurant)
        {
            if (ModelState.IsValid)
            {
                //pizzaRestaurant.Id = Guid.NewGuid();
                _pizzaRestaurantRepository.Add(pizzaRestaurant);
                await _pizzaRestaurantRepository.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(pizzaRestaurant);
        }

        // GET: PizzaRestaurants/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaRestaurant = await _pizzaRestaurantRepository.FindAsync(id);

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
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Name,Address,CreatedBy,CreatedAt,CreatedBy,CreatedAt,Id")]
            PizzaRestaurant pizzaRestaurant)
        {
            if (id != pizzaRestaurant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _pizzaRestaurantRepository.Update(pizzaRestaurant);
                await _pizzaRestaurantRepository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            return View(pizzaRestaurant);
        }

        // GET: PizzaRestaurants/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaRestaurant = await _pizzaRestaurantRepository.FindAsync(id);
            if (pizzaRestaurant == null)
            {
                return NotFound();
            }

            return View(pizzaRestaurant);
        }

        // POST: PizzaRestaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pizzaRestaurant = _pizzaRestaurantRepository.Remove(id);
            await _pizzaRestaurantRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}