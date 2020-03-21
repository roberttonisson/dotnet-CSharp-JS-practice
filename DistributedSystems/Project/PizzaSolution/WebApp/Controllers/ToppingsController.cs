using System;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;

namespace WebApp.Controllers
{
    public class ToppingsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IToppingRepository _toppingRepository;

        public ToppingsController(AppDbContext context)
        {
            _context = context;
            _toppingRepository = new ToppingRepository(_context);
        }

        // GET: Toppings
        public async Task<IActionResult> Index()
        {
            return View(await _toppingRepository.AllAsync());
        }

        // GET: Toppings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topping = await _toppingRepository.FindAsync(id);

            if (topping == null)
            {
                return NotFound();
            }

            return View(topping);
        }

        // GET: Toppings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Toppings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Name,Price,CreatedBy,CreatedAt,CreatedBy,CreatedAt,Id")]
            Topping topping)
        {
            if (ModelState.IsValid)
            {
                //topping.Id = Guid.NewGuid();
                _toppingRepository.Add(topping);
                await _toppingRepository.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(topping);
        }

        // GET: Toppings/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topping = await _toppingRepository.FindAsync(id);

            if (topping == null)
            {
                return NotFound();
            }

            return View(topping);
        }

        // POST: Toppings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Name,Price,CreatedBy,CreatedAt,CreatedBy,CreatedAt,Id")]
            Topping topping)
        {
            if (id != topping.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _toppingRepository.Update(topping);
                await _toppingRepository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            return View(topping);
        }

        // GET: Toppings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topping = await _toppingRepository.FindAsync(id);
            if (topping == null)
            {
                return NotFound();
            }

            return View(topping);
        }

        // POST: Toppings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var topping = _toppingRepository.Remove(id);
            await _toppingRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}