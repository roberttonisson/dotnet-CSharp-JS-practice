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
    public class InvoiceLinesController : Controller
    {
        private readonly AppDbContext _context;

        public InvoiceLinesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: InvoiceLines
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.InvoiceLines.Include(i => i.DrinkInCart).Include(i => i.Invoice).Include(i => i.PizzaInCart);
            return View(await appDbContext.ToListAsync());
        }

        // GET: InvoiceLines/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceLine = await _context.InvoiceLines
                .Include(i => i.DrinkInCart)
                .Include(i => i.Invoice)
                .Include(i => i.PizzaInCart)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceLine == null)
            {
                return NotFound();
            }

            return View(invoiceLine);
        }

        // GET: InvoiceLines/Create
        public IActionResult Create()
        {
            ViewData["DrinkInCartId"] = new SelectList(_context.DrinkInCarts, "Id", "Id");
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "Id", "Id");
            ViewData["PizzaInCartId"] = new SelectList(_context.PizzaInCarts, "Id", "Id");
            return View();
        }

        // POST: InvoiceLines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Quantity,PizzaInCartId,DrinkInCartId,InvoiceId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] InvoiceLine invoiceLine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoiceLine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DrinkInCartId"] = new SelectList(_context.DrinkInCarts, "Id", "Id", invoiceLine.DrinkInCartId);
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "Id", "Id", invoiceLine.InvoiceId);
            ViewData["PizzaInCartId"] = new SelectList(_context.PizzaInCarts, "Id", "Id", invoiceLine.PizzaInCartId);
            return View(invoiceLine);
        }

        // GET: InvoiceLines/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceLine = await _context.InvoiceLines.FindAsync(id);
            if (invoiceLine == null)
            {
                return NotFound();
            }
            ViewData["DrinkInCartId"] = new SelectList(_context.DrinkInCarts, "Id", "Id", invoiceLine.DrinkInCartId);
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "Id", "Id", invoiceLine.InvoiceId);
            ViewData["PizzaInCartId"] = new SelectList(_context.PizzaInCarts, "Id", "Id", invoiceLine.PizzaInCartId);
            return View(invoiceLine);
        }

        // POST: InvoiceLines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Quantity,PizzaInCartId,DrinkInCartId,InvoiceId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] InvoiceLine invoiceLine)
        {
            if (id != invoiceLine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoiceLine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceLineExists(invoiceLine.Id))
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
            ViewData["DrinkInCartId"] = new SelectList(_context.DrinkInCarts, "Id", "Id", invoiceLine.DrinkInCartId);
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "Id", "Id", invoiceLine.InvoiceId);
            ViewData["PizzaInCartId"] = new SelectList(_context.PizzaInCarts, "Id", "Id", invoiceLine.PizzaInCartId);
            return View(invoiceLine);
        }

        // GET: InvoiceLines/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceLine = await _context.InvoiceLines
                .Include(i => i.DrinkInCart)
                .Include(i => i.Invoice)
                .Include(i => i.PizzaInCart)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceLine == null)
            {
                return NotFound();
            }

            return View(invoiceLine);
        }

        // POST: InvoiceLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var invoiceLine = await _context.InvoiceLines.FindAsync(id);
            _context.InvoiceLines.Remove(invoiceLine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceLineExists(string id)
        {
            return _context.InvoiceLines.Any(e => e.Id == id);
        }
    }
}
