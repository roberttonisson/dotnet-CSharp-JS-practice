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
using AdditionalTopping = BLL.App.DTO.AdditionalTopping;

namespace WebApp.Controllers

{
    [Authorize]
    public class AdditionalToppingsController : Controller
    {
        private readonly IAppBLL _bll;

        public AdditionalToppingsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: AdditionalToppings
        public async Task<IActionResult> Index()
        {
            return View(await _bll.AdditionalToppings.GetAllAsync(User.UserGuidId()));
        }

        // GET: AdditionalToppings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalTopping = await _bll.AdditionalToppings.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (additionalTopping == null)
            {
                return NotFound();
            }

            return View(additionalTopping);
        }

        // GET: AdditionalToppings/Create
        public async Task<IActionResult> Create()
        {
            var additionalTopping = new AdditionalTopping();
            additionalTopping.ToppingSelectList = new SelectList(await _bll.Toppings.GetAllAsync(), nameof(Topping.Id), nameof(Topping.Name));
            additionalTopping.PizzaInCartSelectList = new SelectList(await _bll.PizzaInCarts.GetAllAsync(User.UserGuidId()), nameof(PizzaInCart.Id), nameof(PizzaInCart.Id));
            return View(additionalTopping);
        }

        // POST: AdditionalToppings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.AdditionalTopping additionalTopping)
        {
            if (ModelState.IsValid)
            {
                _bll.AdditionalToppings.Add(additionalTopping);
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            additionalTopping.ToppingSelectList = new SelectList(await _bll.Toppings.GetAllAsync(), nameof(Topping.Id), nameof(Topping.Name));
            additionalTopping.PizzaInCartSelectList = new SelectList(await _bll.PizzaInCarts.GetAllAsync(User.UserGuidId()), nameof(PizzaInCart.Id), nameof(PizzaInCart.Id));
            return View(additionalTopping);
        }

        // GET: AdditionalToppings/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var additionalTopping = await _bll.AdditionalToppings.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (additionalTopping == null)
            {
                return NotFound();
            }
            additionalTopping.ToppingSelectList = new SelectList(await _bll.Toppings.GetAllAsync(), nameof(Topping.Id), nameof(Topping.Name));
            additionalTopping.PizzaInCartSelectList = new SelectList(await _bll.PizzaInCarts.GetAllAsync(User.UserGuidId()), nameof(PizzaInCart.Id), nameof(PizzaInCart.Id));

            return View(additionalTopping);
        }

        // POST: AdditionalToppings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, AdditionalTopping additionalTopping)
        {
            if (id != additionalTopping.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.AdditionalToppings.UpdateAsync(additionalTopping);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            additionalTopping.ToppingSelectList = new SelectList(await _bll.Toppings.GetAllAsync(), nameof(Topping.Id), nameof(Topping.Name));
            additionalTopping.PizzaInCartSelectList = new SelectList(await _bll.PizzaInCarts.GetAllAsync(User.UserGuidId()), nameof(PizzaInCart.Id), nameof(PizzaInCart.Id));
            return View(additionalTopping);
        }

        // GET: AdditionalToppings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalTopping = await _bll.AdditionalToppings.FirstOrDefaultAsync(id.Value, User.UserGuidId());
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
            await _bll.AdditionalToppings.RemoveAsync(id, User.UserGuidId());
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}