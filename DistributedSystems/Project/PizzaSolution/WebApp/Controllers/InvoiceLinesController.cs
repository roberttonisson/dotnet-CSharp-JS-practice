using System;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;

namespace WebApp.Controllers
{
    public class InvoiceLinesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IInvoiceLineRepository _invoiceLineRepository;

        public InvoiceLinesController(AppDbContext context)
        {
            _context = context;
            _invoiceLineRepository = new InvoiceLineRepository(_context);
        }

        // GET: InvoiceLines
        public async Task<IActionResult> Index()
        {
            return View(await _invoiceLineRepository.AllAsync());
        }

        // GET: InvoiceLines/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceLine = await _invoiceLineRepository.FindAsync(id);

            if (invoiceLine == null)
            {
                return NotFound();
            }

            return View(invoiceLine);
        }

        // GET: InvoiceLines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InvoiceLines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Quantity,PizzaInCartId,DrinkInCartId,InvoiceId,CreatedBy,CreatedAt,CreatedBy,CreatedAt,Id")]
            InvoiceLine invoiceLine)
        {
            if (ModelState.IsValid)
            {
                //invoiceLine.Id = Guid.NewGuid();
                _invoiceLineRepository.Add(invoiceLine);
                await _invoiceLineRepository.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(invoiceLine);
        }

        // GET: InvoiceLines/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceLine = await _invoiceLineRepository.FindAsync(id);

            if (invoiceLine == null)
            {
                return NotFound();
            }

            return View(invoiceLine);
        }

        // POST: InvoiceLines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Quantity,PizzaInCartId,DrinkInCartId,InvoiceId,CreatedBy,CreatedAt,CreatedBy,CreatedAt,Id")]
            InvoiceLine invoiceLine)
        {
            if (id != invoiceLine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _invoiceLineRepository.Update(invoiceLine);
                await _invoiceLineRepository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            return View(invoiceLine);
        }

        // GET: InvoiceLines/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceLine = await _invoiceLineRepository.FindAsync(id);
            if (invoiceLine == null)
            {
                return NotFound();
            }

            return View(invoiceLine);
        }

        // POST: InvoiceLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var invoiceLine = _invoiceLineRepository.Remove(id);
            await _invoiceLineRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}