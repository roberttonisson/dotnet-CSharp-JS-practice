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
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
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
            return View(await _uow.PizzaInCarts.GetIncluded(User.UserGuidId()));
        }

        // GET: PizzaInCarts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaInCart = await _uow.PizzaInCarts.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (pizzaInCart == null)
            {
                return NotFound();
            }

            return View(pizzaInCart);
        }

               // GET: PizzaInCarts/Create
        public async Task<IActionResult> Create()
        {
            var vm =  new PizzaInCartCreateEditViewModel();
            vm.PizzaTypeSelectList = new SelectList(await _uow.PizzaTypes.AllAsync(), nameof(PizzaType.Id), nameof(PizzaType.Name));
            vm.CrustSelectList = new SelectList(await _uow.Crusts.AllAsync(), nameof(Crust.Id), nameof(Crust.Name));
            vm.SizeSelectList = new SelectList(await _uow.Sizes.AllAsync(), nameof(Size.Id), nameof(Size.Name));
            vm.CartSelectList = new SelectList(await _uow.Carts.AllAsync(User.UserGuidId()), nameof(Cart.Id), nameof(Cart.Id));
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
                vm.PizzaInCart.CreatedAt = DateTime.Now;
                vm.PizzaInCart.ChangedBy = _uow.Users.Find(User.UserGuidId()).UserName;
                vm.PizzaInCart.CreatedBy = vm.PizzaInCart.ChangedBy;
                vm.PizzaInCart.ChangedAt = DateTime.Now;
                _uow.PizzaInCarts.Add(vm.PizzaInCart);
                await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            vm.PizzaTypeSelectList = new SelectList(await _uow.PizzaTypes.AllAsync(), nameof(PizzaType.Id), nameof(PizzaType.Name));
            vm.CrustSelectList = new SelectList(await _uow.Crusts.AllAsync(), nameof(Crust.Id), nameof(Crust.Name));
            vm.SizeSelectList = new SelectList(await _uow.Sizes.AllAsync(), nameof(Size.Id), nameof(Size.Name));
            vm.CartSelectList = new SelectList(await _uow.Carts.AllAsync(User.UserGuidId()), nameof(Cart.Id), nameof(Cart.Id));
            return View(vm);
        }

        // GET: PizzaInCarts/Edit/5
        public async Task<IActionResult> Edit(Guid? id, PizzaInCartCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.PizzaInCart = await _uow.PizzaInCarts.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (vm.PizzaInCart == null)
            {
                return NotFound();
            }
            vm.PizzaTypeSelectList = new SelectList(await _uow.PizzaTypes.AllAsync(), nameof(PizzaType.Id), nameof(PizzaType.Name));
            vm.CrustSelectList = new SelectList(await _uow.Crusts.AllAsync(), nameof(Crust.Id), nameof(Crust.Name));
            vm.SizeSelectList = new SelectList(await _uow.Sizes.AllAsync(), nameof(Size.Id), nameof(Size.Name));
            vm.CartSelectList = new SelectList(await _uow.Carts.AllAsync(User.UserGuidId()), nameof(Cart.Id), nameof(Cart.Id));

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
                vm.PizzaInCart.ChangedBy = _uow.Users.Find(User.UserGuidId()).UserName;
                vm.PizzaInCart.ChangedAt = DateTime.Now;
                _uow.PizzaInCarts.Update(vm.PizzaInCart);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            vm.PizzaTypeSelectList = new SelectList(await _uow.PizzaTypes.AllAsync(), nameof(PizzaType.Id), nameof(PizzaType.Name));
            vm.CrustSelectList = new SelectList(await _uow.Crusts.AllAsync(), nameof(Crust.Id), nameof(Crust.Name));
            vm.SizeSelectList = new SelectList(await _uow.Sizes.AllAsync(), nameof(Size.Id), nameof(Size.Name));
            vm.CartSelectList = new SelectList(await _uow.Carts.AllAsync(User.UserGuidId()), nameof(Cart.Id), nameof(Cart.Id));
            return View(vm);
        }
        // GET: PizzaInCarts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaInCart = await _uow.PizzaInCarts.FirstOrDefaultAsync(id.Value, User.UserGuidId());
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
            await _uow.PizzaInCarts.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}