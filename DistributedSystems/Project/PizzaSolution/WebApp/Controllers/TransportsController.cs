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
    public class TransportsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public TransportsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Transports
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Transports.AllAsync());
        }

        // GET: Transports/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transport = await _uow.Transports.FindAsync(id);

            if (transport == null)
            {
                return NotFound();
            }

            return View(transport);
        }

        // GET: Transports/Create
        public IActionResult Create()
        {
            var vm = new TransportCreateEditViewModel();
            return View(vm);
        }

        // POST: Transports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransportCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //crust.Id = Guid.NewGuid();
                _uow.Transports.Add(vm.Transport);
                await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Transports/Edit/5
        public async Task<IActionResult> Edit(Guid? id, TransportCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.Transport = await _uow.Transports.FindAsync(id);

            if (vm.Transport == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // POST: Transports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            TransportCreateEditViewModel vm)
        {
            if (id != vm.Transport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Transports.Update(vm.Transport);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Transports/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transport = await _uow.Transports.FindAsync(id);
            if (transport == null)
            {
                return NotFound();
            }

            return View(transport);
        }

        // POST: Transports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var transport = _uow.Transports.Remove(id);
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}