using System;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class PartyOrderInvoicesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPartyOrderInvoiceRepository _partyOrderInvoiceRepository;

        public PartyOrderInvoicesController(AppDbContext context)
        {
            _context = context;
            _partyOrderInvoiceRepository = new PartyOrderInvoiceRepository(_context);
        }

        // GET: PartyOrderInvoices
        public async Task<IActionResult> Index()
        {
            return View(await _partyOrderInvoiceRepository.AllAsync());
        }

        // GET: PartyOrderInvoices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partyOrderInvoice = await _partyOrderInvoiceRepository.FindAsync(id);

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
            vm.PartyOrderSelectList = new SelectList(_context.PartyOrders, nameof(PartyOrder.Id), nameof(PartyOrder.Id));
            vm.InvoiceSelectList = new SelectList(_context.Invoices, nameof(Invoice.Id), nameof(Invoice.Id));
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
                //partyOrderInvoice.Id = Guid.NewGuid();
                _partyOrderInvoiceRepository.Add(vm.PartyOrderInvoice);
                await _partyOrderInvoiceRepository.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            vm.PartyOrderSelectList = new SelectList(_context.PartyOrders, nameof(PartyOrder.Id), nameof(PartyOrder.Id));
            vm.InvoiceSelectList = new SelectList(_context.Invoices, nameof(Invoice.Id), nameof(Invoice.Id));
            return View(vm);
        }

        // GET: PartyOrderInvoices/Edit/5
        public async Task<IActionResult> Edit(Guid? id, PartyOrderInvoiceCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.PartyOrderInvoice = await _partyOrderInvoiceRepository.FindAsync(id);
            if (vm.PartyOrderInvoice == null)
            {
                return NotFound();
            }
            vm.PartyOrderSelectList = new SelectList(_context.PartyOrders, nameof(PartyOrder.Id), nameof(PartyOrder.Id));
            vm.InvoiceSelectList = new SelectList(_context.Invoices, nameof(Invoice.Id), nameof(Invoice.Id));

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
                _partyOrderInvoiceRepository.Update(vm.PartyOrderInvoice);
                await _partyOrderInvoiceRepository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            vm.PartyOrderSelectList = new SelectList(_context.PartyOrders, nameof(PartyOrder.Id), nameof(PartyOrder.Id));
            vm.InvoiceSelectList = new SelectList(_context.Invoices, nameof(Invoice.Id), nameof(Invoice.Id));
            return View(vm);
        }

        // GET: PartyOrderInvoices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partyOrderInvoice = await _partyOrderInvoiceRepository.FindAsync(id);
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
            var partyOrderInvoice = _partyOrderInvoiceRepository.Remove(id);
            await _partyOrderInvoiceRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}