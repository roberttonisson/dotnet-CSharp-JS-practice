using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class DefaultToppingsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public DefaultToppingsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: DefaultToppings
        public async Task<IActionResult> Index()
        {
            return View(await _uow.DefaultToppings.GetIncluded());
        }

        // GET: DefaultToppings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defaultTopping = await _uow.DefaultToppings.FirstOrDefaultAsync(id.Value);

            if (defaultTopping == null)
            {
                return NotFound();
            }

            return View(defaultTopping);
        }

        // GET: DefaultToppings/Create
           // GET: DefaultToppings/Create
           [Authorize(Roles = "Admin")]
           public IActionResult Create()
        {
            var vm =  new DefaultToppingCreateEditViewModel();
            vm.ToppingSelectList = new SelectList(_uow.Toppings.All(), nameof(Topping.Id), nameof(Topping.Name));
            vm.PizzaTypeSelectList = new SelectList(_uow.PizzaTypes.All(), nameof(PizzaType.Id), nameof(PizzaType.Name));
            return View(vm);
        }

        // POST: DefaultToppings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken][Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(DefaultToppingCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //defaultTopping.Id = Guid.NewGuid();
                _uow.DefaultToppings.Add(vm.DefaultTopping);
                await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            vm.ToppingSelectList = new SelectList(_uow.Toppings.All(), nameof(Topping.Id), nameof(Topping.Name));
            vm.PizzaTypeSelectList = new SelectList(_uow.PizzaTypes.All(), nameof(PizzaType.Id), nameof(PizzaType.Name));
            return View(vm);
        }

        // GET: DefaultToppings/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid? id, DefaultToppingCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.DefaultTopping = await _uow.DefaultToppings.FindAsync(id);
            if (vm.DefaultTopping == null)
            {
                return NotFound();
            }
            vm.ToppingSelectList = new SelectList(_uow.Toppings.All(), nameof(Topping.Id), nameof(Topping.Name));
            vm.PizzaTypeSelectList = new SelectList(_uow.PizzaTypes.All(), nameof(PizzaType.Id), nameof(PizzaType.Name));

            return View(vm);
        }

        // POST: DefaultToppings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id, DefaultToppingCreateEditViewModel vm)
        {
            if (id != vm.DefaultTopping.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.DefaultToppings.Update(vm.DefaultTopping);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            vm.ToppingSelectList = new SelectList(_uow.Toppings.All(), nameof(Topping.Id), nameof(Topping.Name));
            vm.PizzaTypeSelectList = new SelectList(_uow.PizzaTypes.All(), nameof(PizzaType.Id), nameof(PizzaType.Name));
            return View(vm);
        }

        // GET: DefaultToppings/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defaultTopping = await _uow.DefaultToppings.FirstOrDefaultAsync(id.Value);
            if (defaultTopping == null)
            {
                return NotFound();
            }

            return View(defaultTopping);
        }

        // POST: DefaultToppings/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _uow.DefaultToppings.Remove(id);
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}