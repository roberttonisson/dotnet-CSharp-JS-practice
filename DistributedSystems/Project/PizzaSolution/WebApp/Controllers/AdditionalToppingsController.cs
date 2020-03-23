using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AdditionalToppingsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public AdditionalToppingsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: AdditionalToppings
        public async Task<IActionResult> Index()
        {
            return View(await _uow.AdditionalToppings.AllAsync());
        }

        // GET: AdditionalToppings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalTopping = await _uow.AdditionalToppings.FindAsync(id);

            if (additionalTopping == null)
            {
                return NotFound();
            }

            return View(additionalTopping);
        }

        // GET: AdditionalToppings/Create
        public IActionResult Create()
        {
            var vm =  new AdditionalToppingCreateEditViewModel();
            vm.ToppingSelectList = new SelectList(_uow.Toppings.All(), nameof(Topping.Id), nameof(Topping.Name));
            vm.PizzaInCartSelectList = new SelectList(_uow.Toppings.All(), nameof(PizzaInCart.Id), nameof(PizzaInCart.Id));
            return View(vm);
        }

        // POST: AdditionalToppings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdditionalToppingCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //additionalTopping.Id = Guid.NewGuid();
                _uow.AdditionalToppings.Add(vm.AdditionalTopping);
                await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            vm.ToppingSelectList = new SelectList(_uow.Toppings.All(), nameof(Topping.Id), nameof(Topping.Name));
            vm.PizzaInCartSelectList = new SelectList(_uow.Toppings.All(), nameof(PizzaInCart.Id), nameof(PizzaInCart.Id));
            return View(vm);
        }

        // GET: AdditionalToppings/Edit/5
        public async Task<IActionResult> Edit(Guid? id, AdditionalToppingCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.AdditionalTopping = await _uow.AdditionalToppings.FindAsync(id);
            if (vm.AdditionalTopping == null)
            {
                return NotFound();
            }
            vm.ToppingSelectList = new SelectList(_uow.Toppings.All(), nameof(Topping.Id), nameof(Topping.Name));
            vm.PizzaInCartSelectList = new SelectList(_uow.Toppings.All(), nameof(PizzaInCart.Id), nameof(PizzaInCart.Id));

            return View(vm);
        }

        // POST: AdditionalToppings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, AdditionalToppingCreateEditViewModel vm)
        {
            if (id != vm.AdditionalTopping.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.AdditionalToppings.Update(vm.AdditionalTopping);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            vm.ToppingSelectList = new SelectList(_uow.Toppings.All(), nameof(Topping.Id), nameof(Topping.Name));
            vm.PizzaInCartSelectList = new SelectList(_uow.Toppings.All(), nameof(PizzaInCart.Id), nameof(PizzaInCart.Id));
            return View(vm);
        }

        // GET: AdditionalToppings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalTopping = await _uow.AdditionalToppings.FindAsync(id);
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
            var additionalTopping = _uow.AdditionalToppings.Remove(id);
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}