using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers
{
    public class PartyOrderInvoicesController : Controller
    {
        private readonly AppDbContext _context;

        public PartyOrderInvoicesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PartyOrderInvoices
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.PartyOrderInvoices.Include(p => p.Invoice).Include(p => p.PartyOrder);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PartyOrderInvoices/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partyOrderInvoice = await _context.PartyOrderInvoices
                .Include(p => p.Invoice)
                .Include(p => p.PartyOrder)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partyOrderInvoice == null)
            {
                return NotFound();
            }

            return View(partyOrderInvoice);
        }

        // GET: PartyOrderInvoices/Create
        public IActionResult Create()
        {
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "Id", "Id");
            ViewData["PartyOrderId"] = new SelectList(_context.PartyOrders, "Id", "Id");
            return View();
        }

        // POST: PartyOrderInvoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartyOrderId,InvoiceId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] PartyOrderInvoice partyOrderInvoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(partyOrderInvoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "Id", "Id", partyOrderInvoice.InvoiceId);
            ViewData["PartyOrderId"] = new SelectList(_context.PartyOrders, "Id", "Id", partyOrderInvoice.PartyOrderId);
            return View(partyOrderInvoice);
        }

        // GET: PartyOrderInvoices/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partyOrderInvoice = await _context.PartyOrderInvoices.FindAsync(id);
            if (partyOrderInvoice == null)
            {
                return NotFound();
            }
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "Id", "Id", partyOrderInvoice.InvoiceId);
            ViewData["PartyOrderId"] = new SelectList(_context.PartyOrders, "Id", "Id", partyOrderInvoice.PartyOrderId);
            return View(partyOrderInvoice);
        }

        // POST: PartyOrderInvoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PartyOrderId,InvoiceId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] PartyOrderInvoice partyOrderInvoice)
        {
            if (id != partyOrderInvoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partyOrderInvoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartyOrderInvoiceExists(partyOrderInvoice.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "Id", "Id", partyOrderInvoice.InvoiceId);
            ViewData["PartyOrderId"] = new SelectList(_context.PartyOrders, "Id", "Id", partyOrderInvoice.PartyOrderId);
            return View(partyOrderInvoice);
        }

        // GET: PartyOrderInvoices/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partyOrderInvoice = await _context.PartyOrderInvoices
                .Include(p => p.Invoice)
                .Include(p => p.PartyOrder)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partyOrderInvoice == null)
            {
                return NotFound();
            }

            return View(partyOrderInvoice);
        }

        // POST: PartyOrderInvoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var partyOrderInvoice = await _context.PartyOrderInvoices.FindAsync(id);
            _context.PartyOrderInvoices.Remove(partyOrderInvoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartyOrderInvoiceExists(string id)
        {
            return _context.PartyOrderInvoices.Any(e => e.Id == id);
        }
    }
}
