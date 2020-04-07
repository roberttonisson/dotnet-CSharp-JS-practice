using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class PartyOrdersController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public PartyOrdersController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: PartyOrders
        public async Task<IActionResult> Index()
        {
            return View(await _uow.PartyOrders.AllAsync());
        }

        // GET: PartyOrders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partyOrder = await _uow.PartyOrders.FindAsync(id);

            if (partyOrder == null)
            {
                return NotFound();
            }

            return View(partyOrder);
        }

        // GET: PartyOrders/Create
            // GET: PartyOrders/Create
        public IActionResult Create()
        {
            var vm =  new PartyOrderCreateEditViewModel();
            vm.AppUserSelectList = new SelectList(_uow.Users.All(), nameof(AppUser.Id), nameof(AppUser.Email));
            return View(vm);
        }

        // POST: PartyOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PartyOrderCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //partyOrder.Id = Guid.NewGuid();
                _uow.PartyOrders.Add(vm.PartyOrder);
                await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            vm.AppUserSelectList = new SelectList(_uow.Users.All(), nameof(AppUser.Id), nameof(AppUser.Email));
            return View(vm);
        }

        // GET: PartyOrders/Edit/5
        public async Task<IActionResult> Edit(Guid? id, PartyOrderCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.PartyOrder = await _uow.PartyOrders.FindAsync(id);
            if (vm.PartyOrder == null)
            {
                return NotFound();
            }
            vm.AppUserSelectList = new SelectList(_uow.Users.All(), nameof(AppUser.Id), nameof(AppUser.Email));

            return View(vm);
        }

        // POST: PartyOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PartyOrderCreateEditViewModel vm)
        {
            if (id != vm.PartyOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.PartyOrders.Update(vm.PartyOrder);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            vm.AppUserSelectList = new SelectList(_uow.Users.All(), nameof(AppUser.Id), nameof(AppUser.Email));
            return View(vm);
        }

        // GET: PartyOrders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partyOrder = await _uow.PartyOrders.FindAsync(id);
            if (partyOrder == null)
            {
                return NotFound();
            }

            return View(partyOrder);
        }

        // POST: PartyOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var partyOrder = _uow.PartyOrders.Remove(id);
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}