using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authorization;
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
          [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(DrinkCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Drink.CreatedAt = DateTime.Now;
                vm.Drink.ChangedBy = _uow.Users.Find(User.UserGuidId()).UserName;
                vm.Drink.CreatedBy = vm.Drink.ChangedBy;
                vm.Drink.ChangedAt = DateTime.Now;
                _uow.Drinks.Add(vm.Drink);
                await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Drinks/Edit/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id,
            DrinkCreateEditViewModel vm)
        {
            if (id != vm.Drink.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                vm.Drink.ChangedBy = _uow.Users.Find(User.UserGuidId()).UserName;
                vm.Drink.ChangedAt = DateTime.Now;
                _uow.Drinks.Update(vm.Drink);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Drinks/Delete/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var drink = _uow.Drinks.Remove(id);
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}