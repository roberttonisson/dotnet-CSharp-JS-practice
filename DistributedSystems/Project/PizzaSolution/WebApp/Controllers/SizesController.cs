using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class SizesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public SizesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Sizes
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Sizes.AllAsync());
        }

        // GET: Sizes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var size = await _uow.Sizes.FindAsync(id);

            if (size == null)
            {
                return NotFound();
            }

            return View(size);
        }

      // GET: Sizes/Create
      [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var vm = new SizeCreateEditViewModel();
            return View(vm);
        }

        // POST: Sizes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(SizeCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //crust.Id = Guid.NewGuid();
                _uow.Sizes.Add(vm.Size);
                await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Sizes/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid? id, SizeCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.Size = await _uow.Sizes.FindAsync(id);

            if (vm.Size == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // POST: Sizes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id,
            SizeCreateEditViewModel vm)
        {
            if (id != vm.Size.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Sizes.Update(vm.Size);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Sizes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var size = await _uow.Sizes.FindAsync(id);
            if (size == null)
            {
                return NotFound();
            }

            return View(size);
        }

        // POST: Sizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var size = _uow.Sizes.Remove(id);
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}