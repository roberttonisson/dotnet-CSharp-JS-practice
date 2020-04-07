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
using WebApp.Models;

namespace WebApp.Controllers
{
    public class PizzaTypesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public PizzaTypesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: PizzaTypes
        public async Task<IActionResult> Index()
        {
            return View(await _uow.PizzaTypes.AllAsync());
        }

        // GET: PizzaTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaType = await _uow.PizzaTypes.FindAsync(id);

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
            var vm = new PizzaTypeCreateEditViewModel();
            return View(vm);
        }

        // POST: PizzaTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(PizzaTypeCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.PizzaType.CreatedAt = DateTime.Now;
                vm.PizzaType.ChangedBy = _uow.Users.Find(User.UserGuidId()).UserName;
                vm.PizzaType.CreatedBy = vm.PizzaType.ChangedBy;
                vm.PizzaType.ChangedAt = DateTime.Now;
                _uow.PizzaTypes.Add(vm.PizzaType);
                await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: PizzaTypes/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid? id, PizzaTypeCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.PizzaType = await _uow.PizzaTypes.FindAsync(id);

            if (vm.PizzaType == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // POST: PizzaTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id,
            PizzaTypeCreateEditViewModel vm)
        {
            if (id != vm.PizzaType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                vm.PizzaType.ChangedBy = _uow.Users.Find(User.UserGuidId()).UserName;
                vm.PizzaType.ChangedAt = DateTime.Now;
                _uow.PizzaTypes.Update(vm.PizzaType);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: PizzaTypes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaType = await _uow.PizzaTypes.FindAsync(id);
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
            var pizzaType = _uow.PizzaTypes.Remove(id);
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}