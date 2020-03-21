using System;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;

namespace WebApp.Controllers
{
    public class AdditionalToppingsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAdditionalToppingRepository _additionalToppingRepository;

        public AdditionalToppingsController(AppDbContext context)
        {
            _context = context;
            _additionalToppingRepository = new AdditionalToppingRepository(_context);
        }

        // GET: AdditionalToppings
        public async Task<IActionResult> Index()
        {
            return View(await _additionalToppingRepository.AllAsync());
        }

        // GET: AdditionalToppings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalTopping = await _additionalToppingRepository.FindAsync(id);

            if (additionalTopping == null)
            {
                return NotFound();
            }

            return View(additionalTopping);
        }

        // GET: AdditionalToppings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdditionalToppings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ToppingId,PizzaInCartId,CreatedBy,CreatedAt,CreatedBy,CreatedAt,Id")]
            AdditionalTopping additionalTopping)
        {
            if (ModelState.IsValid)
            {
                //additionalTopping.Id = Guid.NewGuid();
                _additionalToppingRepository.Add(additionalTopping);
                await _additionalToppingRepository.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(additionalTopping);
        }

        // GET: AdditionalToppings/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalTopping = await _additionalToppingRepository.FindAsync(id);

            if (additionalTopping == null)
            {
                return NotFound();
            }

            return View(additionalTopping);
        }

        // POST: AdditionalToppings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("ToppingId,PizzaInCartId,CreatedBy,CreatedAt,CreatedBy,CreatedAt,Id")]
            AdditionalTopping additionalTopping)
        {
            if (id != additionalTopping.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _additionalToppingRepository.Update(additionalTopping);
                await _additionalToppingRepository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            return View(additionalTopping);
        }

        // GET: AdditionalToppings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalTopping = await _additionalToppingRepository.FindAsync(id);
            if (additionalTopping == null)
            {
                return NotFound();
            }

            return View(additionalTopping);
        }

        // POST: AdditionalToppings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var additionalTopping = _additionalToppingRepository.Remove(id);
            await _additionalToppingRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}