using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Domain.Identity;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
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
            return View(await _uow.Carts.Include(User.UserGuidId()));
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _uow.Carts.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            var vm = new CartCreateEditViewModel();
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
            vm.Cart = new Cart();
            var id = User.UserGuidId();
            vm.Cart.UserId = id;
            vm.Cart.CreatedAt = DateTime.Now;
            vm.Cart.ChangedBy = _uow.Users.Find(id).UserName;
            vm.Cart.CreatedBy = vm.Cart.ChangedBy;
            vm.Cart.ChangedAt = DateTime.Now;
            _uow.Carts.Add(vm.Cart);
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(Guid? id, CartCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.Cart = await _uow.Carts.FirstOrDefaultAsync(id.Value, User.UserGuidId());
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
            vm.Cart.UserId = User.UserGuidId();

            if (ModelState.IsValid)
            {
                try
                {
                    vm.Cart.ChangedBy = _uow.Users.Find(User.UserGuidId()).UserName;
                    vm.Cart.ChangedAt = DateTime.Now;
                    _uow.Carts.Update(vm.Cart);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.Carts.ExistsAsync(id, User.UserGuidId()))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
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

            var cart = await _uow.Carts.FirstOrDefaultAsync(id.Value, User.UserGuidId());
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
            await _uow.Carts.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}