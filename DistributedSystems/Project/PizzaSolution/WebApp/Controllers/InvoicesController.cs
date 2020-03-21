using System;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;

namespace WebApp.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoicesController(AppDbContext context)
        {
            _context = context;
            _invoiceRepository = new InvoiceRepository(_context);
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            return View(await _invoiceRepository.AllAsync());
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _invoiceRepository.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("IsPaid,UserId,TransportId,CreatedBy,CreatedAt,CreatedBy,CreatedAt,Id")]
            Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                //invoice.Id = Guid.NewGuid();
                _invoiceRepository.Add(invoice);
                await _invoiceRepository.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _invoiceRepository.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("IsPaid,UserId,TransportId,CreatedBy,CreatedAt,CreatedBy,CreatedAt,Id")]
            Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _invoiceRepository.Update(invoice);
                await _invoiceRepository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _invoiceRepository.FindAsync(id);
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
            var invoice = _invoiceRepository.Remove(id);
            await _invoiceRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}