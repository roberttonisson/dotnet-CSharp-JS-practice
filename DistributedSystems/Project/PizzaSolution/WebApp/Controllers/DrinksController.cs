using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class DrinksController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public DrinksController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Drinks
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Drinks.AllAsync());
        }

        // GET: Drinks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _uow.Drinks.FindAsync(id);

            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }

          // GET: Drinks/Create
        public IActionResult Create()
        {
            var vm = new DrinkCreateEditViewModel();
            return View(vm);
        }

        // POST: Drinks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DrinkCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //crust.Id = Guid.NewGuid();
                _uow.Drinks.Add(vm.Drink);
                await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Drinks/Edit/5
        public async Task<IActionResult> Edit(Guid? id, DrinkCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.Drink = await _uow.Drinks.FindAsync(id);

            if (vm.Drink == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // POST: Drinks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            DrinkCreateEditViewModel vm)
        {
            if (id != vm.Drink.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Drinks.Update(vm.Drink);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Drinks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _uow.Drinks.FindAsync(id);
            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }

        // POST: Drinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var drink = _uow.Drinks.Remove(id);
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}