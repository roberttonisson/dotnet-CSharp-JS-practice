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
    public class PizzaInCartsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public PizzaInCartsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: PizzaInCarts
        public async Task<IActionResult> Index()
        {
            return View(await _uow.PizzaInCarts.AllAsync());
        }

        // GET: PizzaInCarts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaInCart = await _uow.PizzaInCarts.FindAsync(id);

            if (pizzaInCart == null)
            {
                return NotFound();
            }

            return View(pizzaInCart);
        }

               // GET: PizzaInCarts/Create
        public IActionResult Create()
        {
            var vm =  new PizzaInCartCreateEditViewModel();
            vm.PizzaTypeSelectList = new SelectList(_uow.PizzaTypes.All(), nameof(PizzaType.Id), nameof(PizzaType.Name));
            vm.CrustSelectList = new SelectList(_uow.Crusts.All(), nameof(Crust.Id), nameof(Crust.Name));
            vm.SizeSelectList = new SelectList(_uow.Sizes.All(), nameof(Size.Id), nameof(Size.Name));
            vm.CartSelectList = new SelectList(_uow.Carts.All(), nameof(Cart.Id), nameof(Cart.Id));
            return View(vm);
        }

        // POST: PizzaInCarts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PizzaInCartCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //pizzaInCart.Id = Guid.NewGuid();
                _uow.PizzaInCarts.Add(vm.PizzaInCart);
                await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            vm.PizzaTypeSelectList = new SelectList(_uow.PizzaTypes.All(), nameof(PizzaType.Id), nameof(PizzaType.Name));
            vm.CrustSelectList = new SelectList(_uow.Crusts.All(), nameof(Crust.Id), nameof(Crust.Name));
            vm.SizeSelectList = new SelectList(_uow.Sizes.All(), nameof(Size.Id), nameof(Size.Name));
            vm.CartSelectList = new SelectList(_uow.Carts.All(), nameof(Cart.Id), nameof(Cart.Id));
            return View(vm);
        }

        // GET: PizzaInCarts/Edit/5
        public async Task<IActionResult> Edit(Guid? id, PizzaInCartCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.PizzaInCart = await _uow.PizzaInCarts.FindAsync(id);
            if (vm.PizzaInCart == null)
            {
                return NotFound();
            }
            vm.PizzaTypeSelectList = new SelectList(_uow.PizzaTypes.All(), nameof(PizzaType.Id), nameof(PizzaType.Name));
            vm.CrustSelectList = new SelectList(_uow.Crusts.All(), nameof(Crust.Id), nameof(Crust.Name));
            vm.SizeSelectList = new SelectList(_uow.Sizes.All(), nameof(Size.Id), nameof(Size.Name));
            vm.CartSelectList = new SelectList(_uow.Carts.All(), nameof(Cart.Id), nameof(Cart.Id));

            return View(vm);
        }

        // POST: PizzaInCarts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PizzaInCartCreateEditViewModel vm)
        {
            if (id != vm.PizzaInCart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.PizzaInCarts.Update(vm.PizzaInCart);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            vm.PizzaTypeSelectList = new SelectList(_uow.PizzaTypes.All(), nameof(PizzaType.Id), nameof(PizzaType.Name));
            vm.CrustSelectList = new SelectList(_uow.Crusts.All(), nameof(Crust.Id), nameof(Crust.Name));
            vm.SizeSelectList = new SelectList(_uow.Sizes.All(), nameof(Size.Id), nameof(Size.Name));
            vm.CartSelectList = new SelectList(_uow.Carts.All(), nameof(Cart.Id), nameof(Cart.Id));
            return View(vm);
        }
        // GET: PizzaInCarts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaInCart = await _uow.PizzaInCarts.FindAsync(id);
            if (pizzaInCart == null)
            {
                return NotFound();
            }

            return View(pizzaInCart);
        }

        // POST: PizzaInCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pizzaInCart = _uow.PizzaInCarts.Remove(id);
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}