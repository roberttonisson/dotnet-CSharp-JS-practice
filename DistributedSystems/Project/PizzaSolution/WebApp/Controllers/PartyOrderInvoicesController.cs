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
    [Authorize]
    public class PartyOrderInvoicesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public PartyOrderInvoicesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: PartyOrderInvoices
        public async Task<IActionResult> Index()
        {
            return View(await _uow.PartyOrderInvoices.AllAsync());
        }

        // GET: PartyOrderInvoices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partyOrderInvoice = await _uow.PartyOrderInvoices.FindAsync(id);

            if (partyOrderInvoice == null)
            {
                return NotFound();
            }

            return View(partyOrderInvoice);
        }

          // GET: PartyOrderInvoices/Create
        public IActionResult Create()
        {
            var vm =  new PartyOrderInvoiceCreateEditViewModel();
            vm.PartyOrderSelectList = new SelectList(_uow.PartyOrders.All(), nameof(PartyOrder.Id), nameof(PartyOrder.Id));
            vm.InvoiceSelectList = new SelectList(_uow.Invoices.All(), nameof(Invoice.Id), nameof(Invoice.Id));
            return View(vm);
        }

        // POST: PartyOrderInvoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PartyOrderInvoiceCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.PartyOrderInvoice.CreatedAt = DateTime.Now;
                vm.PartyOrderInvoice.ChangedBy = _uow.Users.Find(User.UserGuidId()).UserName;
                vm.PartyOrderInvoice.CreatedBy = vm.PartyOrderInvoice.ChangedBy;
                vm.PartyOrderInvoice.ChangedAt = DateTime.Now;
                _uow.PartyOrderInvoices.Add(vm.PartyOrderInvoice);
                await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            vm.PartyOrderSelectList = new SelectList(_uow.PartyOrders.All(), nameof(PartyOrder.Id), nameof(PartyOrder.Id));
            vm.InvoiceSelectList = new SelectList(_uow.Invoices.All(), nameof(Invoice.Id), nameof(Invoice.Id));
            return View(vm);
        }

        // GET: PartyOrderInvoices/Edit/5
        public async Task<IActionResult> Edit(Guid? id, PartyOrderInvoiceCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.PartyOrderInvoice = await _uow.PartyOrderInvoices.FindAsync(id);
            if (vm.PartyOrderInvoice == null)
            {
                return NotFound();
            }
            vm.PartyOrderSelectList = new SelectList(_uow.PartyOrders.All(), nameof(PartyOrder.Id), nameof(PartyOrder.Id));
            vm.InvoiceSelectList = new SelectList(_uow.Invoices.All(), nameof(Invoice.Id), nameof(Invoice.Id));

            return View(vm);
        }

        // POST: PartyOrderInvoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PartyOrderInvoiceCreateEditViewModel vm)
        {
            if (id != vm.PartyOrderInvoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                vm.PartyOrderInvoice.ChangedBy = _uow.Users.Find(User.UserGuidId()).UserName;
                vm.PartyOrderInvoice.ChangedAt = DateTime.Now;
                _uow.PartyOrderInvoices.Update(vm.PartyOrderInvoice);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            vm.PartyOrderSelectList = new SelectList(_uow.PartyOrders.All(), nameof(PartyOrder.Id), nameof(PartyOrder.Id));
            vm.InvoiceSelectList = new SelectList(_uow.Invoices.All(), nameof(Invoice.Id), nameof(Invoice.Id));
            return View(vm);
        }

        // GET: PartyOrderInvoices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partyOrderInvoice = await _uow.PartyOrderInvoices.FindAsync(id);
            if (partyOrderInvoice == null)
            {
                return NotFound();
            }

            return View(partyOrderInvoice);
        }

        // POST: PartyOrderInvoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var partyOrderInvoice = _uow.PartyOrderInvoices.Remove(id);
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}