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
    public class ToppingsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ToppingsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }


        // GET: Toppings
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Toppings.AllAsync());
        }

        // GET: Toppings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topping = await _uow.Toppings.FindAsync(id);

            if (topping == null)
            {
                return NotFound();
            }

            return View(topping);
        }

        // GET: Toppings/Create
        public IActionResult Create()
        {
            var vm = new ToppingCreateEditViewModel();
            return View(vm);
        }

        // POST: Toppings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ToppingCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //crust.Id = Guid.NewGuid();
                _uow.Toppings.Add(vm.Topping);
                await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Toppings/Edit/5
        public async Task<IActionResult> Edit(Guid? id, ToppingCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.Topping = await _uow.Toppings.FindAsync(id);

            if (vm.Topping == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // POST: Toppings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            ToppingCreateEditViewModel vm)
        {
            if (id != vm.Topping.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Toppings.Update(vm.Topping);
                await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Toppings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topping = await _uow.Toppings.FindAsync(id);
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
            var topping = _uow.Toppings.Remove(id);
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}