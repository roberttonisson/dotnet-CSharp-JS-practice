using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
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
using DrinkInCart = BLL.App.DTO.DrinkInCart;

namespace WebApp.Controllers

{
    [Authorize]
    public class DrinkInCartsController : Controller
    {
        private readonly IAppBLL _bll;

        public DrinkInCartsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: DrinkInCarts
        public async Task<IActionResult> Index()
        {
            return View(await _bll.DrinkInCarts.GetAllAsync(User.UserGuidId()));
        }

        // GET: DrinkInCarts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkInCart = await _bll.DrinkInCarts.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (drinkInCart == null)
            {
                return NotFound();
            }

            return View(drinkInCart);
        }

        // GET: DrinkInCarts/Create
        public async Task<IActionResult> Create()
        {
            var drinkInCart = new DrinkInCart();
            drinkInCart.DrinkSelectList = new SelectList(await _bll.Drinks.GetAllAsync(), nameof(Drink.Id), nameof(Drink.Name));
            drinkInCart.CartSelectList = new SelectList(await _bll.Carts.GetAllAsync(User.UserGuidId()), nameof(Cart.Id), nameof(Cart.AppUserId));
            return View(drinkInCart);
        }

        // POST: DrinkInCarts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.DrinkInCart drinkInCart)
        {
            if (ModelState.IsValid)
            {
                _bll.DrinkInCarts.Add(drinkInCart);
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            drinkInCart.DrinkSelectList = new SelectList(await _bll.Drinks.GetAllAsync(), nameof(Drink.Id), nameof(Drink.Name));
            drinkInCart.CartSelectList = new SelectList(await _bll.Carts.GetAllAsync(User.UserGuidId()), nameof(Cart.Id), nameof(Cart.AppUserId));
            return View(drinkInCart);
        }

        // GET: DrinkInCarts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var drinkInCart = await _bll.DrinkInCarts.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (drinkInCart == null)
            {
                return NotFound();
            }
            drinkInCart.DrinkSelectList = new SelectList(await _bll.Drinks.GetAllAsync(), nameof(Drink.Id), nameof(Drink.Name));
            drinkInCart.CartSelectList = new SelectList(await _bll.Carts.GetAllAsync(User.UserGuidId()), nameof(Cart.Id), nameof(Cart.AppUserId));
            return View(drinkInCart);
        }

        // POST: DrinkInCarts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, DrinkInCart drinkInCart)
        {
            if (id != drinkInCart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.DrinkInCarts.UpdateAsync(drinkInCart);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            drinkInCart.DrinkSelectList = new SelectList(await _bll.Drinks.GetAllAsync(), nameof(Drink.Id), nameof(Drink.Name));
            drinkInCart.CartSelectList = new SelectList(await _bll.Carts.GetAllAsync(User.UserGuidId()), nameof(Cart.Id), nameof(Cart.AppUserId));
            return View(drinkInCart);
        }

        // GET: DrinkInCarts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkInCart = await _bll.DrinkInCarts.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (drinkInCart == null)
            {
                return NotFound();
            }

            return View(drinkInCart);
        }

        // POST: DrinkInCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.DrinkInCarts.RemoveAsync(id, User.UserGuidId());
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}