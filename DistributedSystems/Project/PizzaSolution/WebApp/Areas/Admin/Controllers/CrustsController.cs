#pragma warning disable 1591
using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Crust = BLL.App.DTO.Crust;

namespace WebApp.Areas.Admin.Controllers

{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CrustsController : Controller
    {
        private readonly IAppBLL _bll;

        public CrustsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Crusts
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Crusts.GetAllAsync(null));
        }

        // GET: Crusts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crust = await _bll.Crusts.FirstOrDefaultAsync(id.Value);

            if (crust == null)
            {
                return NotFound();
            }

            return View(crust);
        }

        // GET: Crusts/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var crust = new Crust();
            return View(crust);
        }

        // POST: Crusts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(BLL.App.DTO.Crust crust)
        {
            if (ModelState.IsValid)
            {
                _bll.Crusts.Add(crust);
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(crust);
        }

        // GET: Crusts/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var crust = await _bll.Crusts.FirstOrDefaultAsync(id.Value);
            
            if (crust == null)
            {
                return NotFound();
            }

            return View(crust);
        }

        // POST: Crusts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id, Crust crust)
        {
            if (id != crust.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.Crusts.UpdateAsync(crust);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(crust);
        }

        // GET: Crusts/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crust = await _bll.Crusts.FirstOrDefaultAsync(id.Value);
            if (crust == null)
            {
                return NotFound();
            }

            return View(crust);
        }

        // POST: Crusts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Crusts.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}