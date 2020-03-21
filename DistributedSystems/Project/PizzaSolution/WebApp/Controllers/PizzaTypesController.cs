using System;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;

namespace WebApp.Controllers
{
    public class PizzaTypesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPizzaTypeRepository _pizzaTypeRepository;

        public PizzaTypesController(AppDbContext context)
        {
            _context = context;
            _pizzaTypeRepository = new PizzaTypeRepository(_context);
        }

        // GET: PizzaTypes
        public async Task<IActionResult> Index()
        {
            return View(await _pizzaTypeRepository.AllAsync());
        }

        // GET: PizzaTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaType = await _pizzaTypeRepository.FindAsync(id);

            if (pizzaType == null)
            {
                return NotFound();
            }

            return View(pizzaType);
        }

        // GET: PizzaTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PizzaTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Name,Price,PizzaRestaurantId,CreatedBy,CreatedAt,CreatedBy,CreatedAt,Id")] 
            PizzaType pizzaType)
        {
            if (ModelState.IsValid)
            {
                //pizzaType.Id = Guid.NewGuid();
                _pizzaTypeRepository.Add(pizzaType);
                await _pizzaTypeRepository.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(pizzaType);
        }

        // GET: PizzaTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaType = await _pizzaTypeRepository.FindAsync(id);

            if (pizzaType == null)
            {
                return NotFound();
            }

            return View(pizzaType);
        }

        // POST: PizzaTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Name,Price,PizzaRestaurantId,CreatedBy,CreatedAt,CreatedBy,CreatedAt,Id")] 
            PizzaType pizzaType)
        {
            if (id != pizzaType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _pizzaTypeRepository.Update(pizzaType);
                await _pizzaTypeRepository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            return View(pizzaType);
        }

        // GET: PizzaTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaType = await _pizzaTypeRepository.FindAsync(id);
            if (pizzaType == null)
            {
                return NotFound();
            }

            return View(pizzaType);
        }

        // POST: PizzaTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pizzaType = _pizzaTypeRepository.Remove(id);
            await _pizzaTypeRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}