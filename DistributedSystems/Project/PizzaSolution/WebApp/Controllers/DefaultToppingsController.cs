using System;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;

namespace WebApp.Controllers
{
    public class DefaultToppingsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IDefaultToppingRepository _defaultToppingRepository;

        public DefaultToppingsController(AppDbContext context)
        {
            _context = context;
            _defaultToppingRepository = new DefaultToppingRepository(_context);
        }

        // GET: DefaultToppings
        public async Task<IActionResult> Index()
        {
            return View(await _defaultToppingRepository.AllAsync());
        }

        // GET: DefaultToppings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defaultTopping = await _defaultToppingRepository.FindAsync(id);

            if (defaultTopping == null)
            {
                return NotFound();
            }

            return View(defaultTopping);
        }

        // GET: DefaultToppings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DefaultToppings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ToppingId,PizzaTypeId,CreatedBy,CreatedAt,CreatedBy,CreatedAt,Id")]
            DefaultTopping defaultTopping)
        {
            if (ModelState.IsValid)
            {
                //defaultTopping.Id = Guid.NewGuid();
                _defaultToppingRepository.Add(defaultTopping);
                await _defaultToppingRepository.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(defaultTopping);
        }

        // GET: DefaultToppings/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defaultTopping = await _defaultToppingRepository.FindAsync(id);

            if (defaultTopping == null)
            {
                return NotFound();
            }

            return View(defaultTopping);
        }

        // POST: DefaultToppings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("ToppingId,PizzaTypeId,CreatedBy,CreatedAt,CreatedBy,CreatedAt,Id")]
            DefaultTopping defaultTopping)
        {
            if (id != defaultTopping.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _defaultToppingRepository.Update(defaultTopping);
                await _defaultToppingRepository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            return View(defaultTopping);
        }

        // GET: DefaultToppings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defaultTopping = await _defaultToppingRepository.FindAsync(id);
            if (defaultTopping == null)
            {
                return NotFound();
            }

            return View(defaultTopping);
        }

        // POST: DefaultToppings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var defaultTopping = _defaultToppingRepository.Remove(id);
            await _defaultToppingRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}