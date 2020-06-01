#pragma warning disable 1591
using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaType = BLL.App.DTO.PizzaType;

namespace WebApp.Areas.Admin.Controllers

{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PizzaTypesController : Controller
    {
        private readonly IAppBLL _bll;

        public PizzaTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: PizzaTypes
        public async Task<IActionResult> Index()
        {
            return View(await _bll.PizzaTypes.GetAllAsync(null));
        }

        // GET: PizzaTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaType = await _bll.PizzaTypes.FirstOrDefaultAsync(id.Value);

            if (pizzaType == null)
            {
                return NotFound();
            }

            return View(pizzaType);
        }

        // GET: PizzaTypes/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var pizzaType = new PizzaType();
            return View(pizzaType);
        }

        // POST: PizzaTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(BLL.App.DTO.PizzaType pizzaType)
        {
            if (ModelState.IsValid)
            {
                _bll.PizzaTypes.Add(pizzaType);
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(pizzaType);
        }

        // GET: PizzaTypes/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pizzaType = await _bll.PizzaTypes.FirstOrDefaultAsync(id.Value);
            
            if (pizzaType == null)
            {
                return NotFound();
            }

            return View(pizzaType);
        }

        // POST: PizzaTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id, PizzaType pizzaType)
        {
            if (id != pizzaType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.PizzaTypes.UpdateAsync(pizzaType);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(pizzaType);
        }

        // GET: PizzaTypes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaType = await _bll.PizzaTypes.FirstOrDefaultAsync(id.Value);
            if (pizzaType == null)
            {
                return NotFound();
            }

            return View(pizzaType);
        }

        // POST: PizzaTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.PizzaTypes.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}