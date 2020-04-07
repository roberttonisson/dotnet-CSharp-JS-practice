using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;
using Domain.Identity;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
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
            return View(await _uow.Invoices.GetIncluded(User.UserGuidId()));
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _uow.Invoices.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
         // GET: Invoices/Create
        public async Task<IActionResult> Create()
        {
            var vm =  new InvoiceCreateEditViewModel();
            vm.TransportSelectList = new SelectList(await _uow.Transports.AllAsync(), nameof(Transport.Id), nameof(Transport.Address));
            return View(vm);
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InvoiceCreateEditViewModel vm)
        {
            vm.Invoice.UserId = User.UserGuidId();
            if (ModelState.IsValid)
            {
                _uow.Invoices.Add(vm.Invoice);
                await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            vm.TransportSelectList = new SelectList(await _uow.Transports.AllAsync(), nameof(Transport.Id), nameof(Transport.Address));
            return View(vm);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(Guid? id, InvoiceCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.Invoice = await _uow.Invoices.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (vm.Invoice == null)
            {
                return NotFound();
            }
            vm.TransportSelectList = new SelectList(await _uow.Transports.AllAsync(), nameof(Transport.Id), nameof(Transport.Address));
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
            vm.Invoice.UserId = User.UserGuidId();
            if (ModelState.IsValid)
            {
                _uow.Invoices.Update(vm.Invoice);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            vm.TransportSelectList = new SelectList(await _uow.Transports.AllAsync(), nameof(Transport.Id), nameof(Transport.Address));
            return View(vm);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _uow.Invoices.FirstOrDefaultAsync(id.Value, User.UserGuidId());
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
            await _uow.Invoices.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}