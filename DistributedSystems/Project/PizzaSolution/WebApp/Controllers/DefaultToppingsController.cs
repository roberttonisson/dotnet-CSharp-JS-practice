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
using DefaultTopping = BLL.App.DTO.DefaultTopping;

namespace WebApp.Controllers

{
    public class DefaultToppingsController : Controller
    {
        private readonly IAppBLL _bll;

        public DefaultToppingsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: DefaultToppings
        public async Task<IActionResult> Index()
        {
            var x = await _bll.DefaultToppings.GetAllAsync(null);
            return View(await _bll.DefaultToppings.GetAllAsync(null));
        }

        // GET: DefaultToppings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defaultTopping = await _bll.DefaultToppings.FirstOrDefaultAsync(id.Value);

            if (defaultTopping == null)
            {
                return NotFound();
            }

            return View(defaultTopping);
        }

        // GET: DefaultToppings/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var defaultTopping = new DefaultTopping();
            defaultTopping.ToppingSelectList = new SelectList(await _bll.Toppings.GetAllAsync(), nameof(Topping.Id), nameof(Topping.Name));
            defaultTopping.PizzaTypeSelectList = new SelectList(await _bll.PizzaTypes.GetAllAsync(), nameof(PizzaType.Id), nameof(PizzaType.Name));
            return View(defaultTopping);
        }

        // POST: DefaultToppings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(BLL.App.DTO.DefaultTopping defaultTopping)
        {
            if (ModelState.IsValid)
            {
                _bll.DefaultToppings.Add(defaultTopping);
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            defaultTopping.ToppingSelectList = new SelectList(await _bll.Toppings.GetAllAsync(), nameof(Topping.Id), nameof(Topping.Name));
            defaultTopping.PizzaTypeSelectList = new SelectList(await _bll.PizzaTypes.GetAllAsync(), nameof(PizzaType.Id), nameof(PizzaType.Name));
            return View(defaultTopping);
        }

        // GET: DefaultToppings/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var defaultTopping = await _bll.DefaultToppings.FirstOrDefaultAsync(id.Value);
            
            if (defaultTopping == null)
            {
                return NotFound();
            }
            defaultTopping.ToppingSelectList = new SelectList(await _bll.Toppings.GetAllAsync(), nameof(Topping.Id), nameof(Topping.Name));
            defaultTopping.PizzaTypeSelectList = new SelectList(await _bll.PizzaTypes.GetAllAsync(), nameof(PizzaType.Id), nameof(PizzaType.Name));

            return View(defaultTopping);
        }

        // POST: DefaultToppings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id, DefaultTopping defaultTopping)
        {
            if (id != defaultTopping.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.DefaultToppings.UpdateAsync(defaultTopping);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            defaultTopping.ToppingSelectList = new SelectList(await _bll.Toppings.GetAllAsync(), nameof(Topping.Id), nameof(Topping.Name));
            defaultTopping.PizzaTypeSelectList = new SelectList(await _bll.PizzaTypes.GetAllAsync(), nameof(PizzaType.Id), nameof(PizzaType.Name));
            return View(defaultTopping);
        }

        // GET: DefaultToppings/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defaultTopping = await _bll.DefaultToppings.FirstOrDefaultAsync(id.Value);
            if (defaultTopping == null)
            {
                return NotFound();
            }

            return View(defaultTopping);
        }

        // POST: DefaultToppings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.DefaultToppings.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}