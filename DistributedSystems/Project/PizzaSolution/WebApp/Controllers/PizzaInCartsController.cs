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
using PizzaInCart = BLL.App.DTO.PizzaInCart;

namespace WebApp.Controllers

{
    [Authorize]
    public class PizzaInCartsController : Controller
    {
        private readonly IAppBLL _bll;

        public PizzaInCartsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: PizzaInCarts
        public async Task<IActionResult> Index()
        {
            return View(await _bll.PizzaInCarts.GetAllAsync(User.UserGuidId()));
        }

        // GET: PizzaInCarts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaInCart = await _bll.PizzaInCarts.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (pizzaInCart == null)
            {
                return NotFound();
            }

            return View(pizzaInCart);
        }

        // GET: PizzaInCarts/Create
        public async Task<IActionResult> Create()
        {
            var pizzaInCart = new PizzaInCart();
            pizzaInCart.PizzaTypeSelectList = new SelectList(await _bll.PizzaTypes.GetAllAsync(), nameof(PizzaType.Id), nameof(PizzaType.Name));
            pizzaInCart.CrustSelectList = new SelectList(await _bll.Crusts.GetAllAsync(), nameof(Crust.Id), nameof(Crust.Name));
            pizzaInCart.SizeSelectList = new SelectList(await _bll.Sizes.GetAllAsync(), nameof(Size.Id), nameof(Size.Name));
            pizzaInCart.CartSelectList = new SelectList(await _bll.Carts.GetAllAsync(User.UserGuidId()), nameof(Cart.Id), nameof(Cart.Id));
            return View(pizzaInCart);
        }

        // POST: PizzaInCarts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.PizzaInCart pizzaInCart)
        {
            if (ModelState.IsValid)
            {
                _bll.PizzaInCarts.Add(pizzaInCart);
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            pizzaInCart.PizzaTypeSelectList = new SelectList(await _bll.PizzaTypes.GetAllAsync(), nameof(PizzaType.Id), nameof(PizzaType.Name));
            pizzaInCart.CrustSelectList = new SelectList(await _bll.Crusts.GetAllAsync(), nameof(Crust.Id), nameof(Crust.Name));
            pizzaInCart.SizeSelectList = new SelectList(await _bll.Sizes.GetAllAsync(), nameof(Size.Id), nameof(Size.Name));
            pizzaInCart.CartSelectList = new SelectList(await _bll.Carts.GetAllAsync(User.UserGuidId()), nameof(Cart.Id), nameof(Cart.Id));
            return View(pizzaInCart);
        }

        // GET: PizzaInCarts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pizzaInCart = await _bll.PizzaInCarts.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (pizzaInCart == null)
            {
                return NotFound();
            }
            pizzaInCart.PizzaTypeSelectList = new SelectList(await _bll.PizzaTypes.GetAllAsync(), nameof(PizzaType.Id), nameof(PizzaType.Name));
            pizzaInCart.CrustSelectList = new SelectList(await _bll.Crusts.GetAllAsync(), nameof(Crust.Id), nameof(Crust.Name));
            pizzaInCart.SizeSelectList = new SelectList(await _bll.Sizes.GetAllAsync(), nameof(Size.Id), nameof(Size.Name));
            pizzaInCart.CartSelectList = new SelectList(await _bll.Carts.GetAllAsync(User.UserGuidId()), nameof(Cart.Id), nameof(Cart.Id));

            return View(pizzaInCart);
        }

        // POST: PizzaInCarts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PizzaInCart pizzaInCart)
        {
            if (id != pizzaInCart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.PizzaInCarts.UpdateAsync(pizzaInCart);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            pizzaInCart.PizzaTypeSelectList = new SelectList(await _bll.PizzaTypes.GetAllAsync(), nameof(PizzaType.Id), nameof(PizzaType.Name));
            pizzaInCart.CrustSelectList = new SelectList(await _bll.Crusts.GetAllAsync(), nameof(Crust.Id), nameof(Crust.Name));
            pizzaInCart.SizeSelectList = new SelectList(await _bll.Sizes.GetAllAsync(), nameof(Size.Id), nameof(Size.Name));
            pizzaInCart.CartSelectList = new SelectList(await _bll.Carts.GetAllAsync(User.UserGuidId()), nameof(Cart.Id), nameof(Cart.Id));
            return View(pizzaInCart);
        }

        // GET: PizzaInCarts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaInCart = await _bll.PizzaInCarts.FirstOrDefaultAsync(id.Value, User.UserGuidId());
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
            await _bll.PizzaInCarts.RemoveAsync(id, User.UserGuidId());
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}