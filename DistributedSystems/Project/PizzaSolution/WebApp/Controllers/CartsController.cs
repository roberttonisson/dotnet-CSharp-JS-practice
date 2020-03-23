using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CartsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public CartsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Carts.AllAsync());
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _uow.Carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            var vm =  new CartCreateEditViewModel();
            vm.AppUserSelectList = new SelectList(_uow.Users.All(), nameof(AppUser.Id), nameof(AppUser.Email));
            return View(vm);
        }

        // POST: Carts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CartCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //cart.Id = Guid.NewGuid();
                _uow.Carts.Add(vm.Cart);
                await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            vm.AppUserSelectList = new SelectList(_uow.Users.All(), nameof(AppUser.Id), nameof(AppUser.Email));
            return View(vm);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(Guid? id, CartCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.Cart = await _uow.Carts.FindAsync(id);
            if (vm.Cart == null)
            {
                return NotFound();
            }
            vm.AppUserSelectList = new SelectList(_uow.Users.All(), nameof(AppUser.Id), nameof(AppUser.Email));

            return View(vm);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CartCreateEditViewModel vm)
        {
            if (id != vm.Cart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Carts.Update(vm.Cart);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            vm.AppUserSelectList = new SelectList(_uow.Users.All(), nameof(AppUser.Id), nameof(AppUser.Email));
            return View(vm);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _uow.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cart = _uow.Carts.Remove(id);
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}