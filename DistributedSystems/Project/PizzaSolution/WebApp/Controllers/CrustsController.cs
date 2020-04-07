using System;
using System.Threading.Tasks;
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

namespace WebApp.Controllers
{
    public class CrustsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public CrustsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Crusts
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Crusts.AllAsync());
        }

        // GET: Crusts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crust = await _uow.Crusts.FindAsync(id);

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
            var vm = new CrustCreateEditViewModel();
            return View(vm);
        }

        // POST: Crusts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CrustCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Crust.CreatedAt = DateTime.Now;
                vm.Crust.ChangedBy = _uow.Users.Find(User.UserGuidId()).UserName;
                vm.Crust.CreatedBy = vm.Crust.ChangedBy;
                vm.Crust.ChangedAt = DateTime.Now;
                _uow.Crusts.Add(vm.Crust);
                await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Crusts/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid? id, CrustCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.Crust = await _uow.Crusts.FindAsync(id);

            if (vm.Crust == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // POST: Crusts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id,
            CrustCreateEditViewModel vm)
        {
            if (id != vm.Crust.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                vm.Crust.ChangedBy = _uow.Users.Find(User.UserGuidId()).UserName;
                vm.Crust.ChangedAt = DateTime.Now;
                _uow.Crusts.Update(vm.Crust);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Crusts/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid? id, CrustCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crust = await _uow.Crusts.FindAsync(id);
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
            var crust = _uow.Crusts.Remove(id);
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}