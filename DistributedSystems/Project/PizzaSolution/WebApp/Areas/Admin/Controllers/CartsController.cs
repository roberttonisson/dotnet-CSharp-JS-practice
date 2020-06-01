#pragma warning disable 1591
using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Domain.Identity;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;
using Cart = BLL.App.DTO.Cart;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CartsController : Controller
    {
        private readonly IAppBLL _bll;

        public CartsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Carts.GetAllAsync(User.UserGuidId()));
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _bll.Carts.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts/Create
        public async Task<IActionResult> Create()
        {
            var cart = new BLL.App.DTO.Cart();
            cart.AppUserSelectList = new SelectList(await _bll.AppUsers.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.Email));
            return View(cart);
        }

        // POST: Carts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.Cart cart)
        {
            cart.AppUserId = User.UserGuidId();

            if (ModelState.IsValid)
            {
                _bll.Carts.Add(cart);
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            cart.AppUserSelectList = new SelectList(await _bll.AppUsers.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.Email));
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _bll.Carts.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (cart == null)
            {
                return NotFound();
            }

            cart.AppUserSelectList = new SelectList(await _bll.AppUsers.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.Email));

            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Cart cart)
        {
            if (id != cart.Id)
            {
                return NotFound();
            }
            cart.AppUserId = User.UserGuidId();

            if (ModelState.IsValid)
            {
                await _bll.Carts.UpdateAsync(cart);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            cart.AppUserSelectList = new SelectList(await _bll.AppUsers.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.Email));
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _bll.Carts.FirstOrDefaultAsync(id.Value, User.UserGuidId());
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
            await _bll.Carts.RemoveAsync(id, User.UserGuidId());
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}