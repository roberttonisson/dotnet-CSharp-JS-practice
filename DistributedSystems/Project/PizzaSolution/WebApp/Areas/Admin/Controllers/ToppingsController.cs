#pragma warning disable 1591
using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Topping = BLL.App.DTO.Topping;

namespace WebApp.Areas.Admin.Controllers

{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ToppingsController : Controller
    {
        private readonly IAppBLL _bll;

        public ToppingsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Toppings
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Toppings.GetAllAsync(null));
        }

        // GET: Toppings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topping = await _bll.Toppings.FirstOrDefaultAsync(id.Value);

            if (topping == null)
            {
                return NotFound();
            }

            return View(topping);
        }

        // GET: Toppings/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var topping = new Topping();
            return View(topping);
        }

        // POST: Toppings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(BLL.App.DTO.Topping topping)
        {
            if (ModelState.IsValid)
            {
                _bll.Toppings.Add(topping);
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(topping);
        }

        // GET: Toppings/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var topping = await _bll.Toppings.FirstOrDefaultAsync(id.Value);
            
            if (topping == null)
            {
                return NotFound();
            }

            return View(topping);
        }

        // POST: Toppings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id, Topping topping)
        {
            if (id != topping.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.Toppings.UpdateAsync(topping);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(topping);
        }

        // GET: Toppings/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topping = await _bll.Toppings.FirstOrDefaultAsync(id.Value);
            if (topping == null)
            {
                return NotFound();
            }

            return View(topping);
        }

        // POST: Toppings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Toppings.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}