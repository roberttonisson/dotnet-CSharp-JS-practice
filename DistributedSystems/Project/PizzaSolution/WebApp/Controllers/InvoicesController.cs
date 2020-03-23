using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public InvoicesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Invoices.AllAsync());
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _uow.Invoices.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
         // GET: Invoices/Create
        public IActionResult Create()
        {
            var vm =  new InvoiceCreateEditViewModel();
            vm.AppUserSelectList = new SelectList(_uow.Users.All(), nameof(AppUser.Id), nameof(AppUser.Email));
            vm.TransportSelectList = new SelectList(_uow.Transports.All(), nameof(Transport.Id), nameof(Transport.Id));
            return View(vm);
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InvoiceCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //invoice.Id = Guid.NewGuid();
                _uow.Invoices.Add(vm.Invoice);
                await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            vm.AppUserSelectList = new SelectList(_uow.Users.All(), nameof(AppUser.Id), nameof(AppUser.Email));
            vm.TransportSelectList = new SelectList(_uow.Transports.All(), nameof(Transport.Id), nameof(Transport.Id));
            return View(vm);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(Guid? id, InvoiceCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.Invoice = await _uow.Invoices.FindAsync(id);
            if (vm.Invoice == null)
            {
                return NotFound();
            }
            vm.AppUserSelectList = new SelectList(_uow.Users.All(), nameof(AppUser.Id), nameof(AppUser.Email));
            vm.TransportSelectList = new SelectList(_uow.Transports.All(), nameof(Transport.Id), nameof(Transport.Id));

            return View(vm);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, InvoiceCreateEditViewModel vm)
        {
            if (id != vm.Invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Invoices.Update(vm.Invoice);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            vm.AppUserSelectList = new SelectList(_uow.Users.All(), nameof(AppUser.Id), nameof(AppUser.Email));
            vm.TransportSelectList = new SelectList(_uow.Transports.All(), nameof(Transport.Id), nameof(Transport.Id));
            return View(vm);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _uow.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var invoice = _uow.Invoices.Remove(id);
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}