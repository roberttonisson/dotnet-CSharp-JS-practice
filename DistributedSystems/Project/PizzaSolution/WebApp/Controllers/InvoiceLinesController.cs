using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
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
using InvoiceLine = BLL.App.DTO.InvoiceLine;

namespace WebApp.Controllers

{
    [Authorize]
    public class InvoiceLinesController : Controller
    {
        private readonly IAppBLL _bll;

        public InvoiceLinesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: InvoiceLines
        public async Task<IActionResult> Index()
        {
            return View(await _bll.InvoiceLines.GetAllAsync(User.UserGuidId()));
        }

        // GET: InvoiceLines/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceLine = await _bll.InvoiceLines.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (invoiceLine == null)
            {
                return NotFound();
            }

            return View(invoiceLine);
        }

        // GET: InvoiceLines/Create
        public async Task<IActionResult> Create()
        {
            var invoiceLine = new InvoiceLine();
            invoiceLine.InvoiceSelectList = new SelectList(await _bll.Invoices.GetAllAsync(User.UserGuidId()), nameof(Invoice.Id), nameof(Invoice.Id));
            invoiceLine.DrinkInCartSelectList = new SelectList(await _bll.DrinkInCarts.GetAllAsync(User.UserGuidId()), nameof(DrinkInCart.Id), nameof(DrinkInCart.Id));
            invoiceLine.PizzaInCartSelectList = new SelectList(await _bll.PizzaInCarts.GetAllAsync(User.UserGuidId()), nameof(PizzaInCart.Id), nameof(PizzaInCart.Id));
            return View(invoiceLine);
        }

        // POST: InvoiceLines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.InvoiceLine invoiceLine)
        {
            if (ModelState.IsValid)
            {
                _bll.InvoiceLines.Add(invoiceLine);
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            invoiceLine.InvoiceSelectList = new SelectList(await _bll.Invoices.GetAllAsync(User.UserGuidId()), nameof(Invoice.Id), nameof(Invoice.Id));
            invoiceLine.DrinkInCartSelectList = new SelectList(await _bll.DrinkInCarts.GetAllAsync(User.UserGuidId()), nameof(DrinkInCart.Id), nameof(DrinkInCart.Id));
            invoiceLine.PizzaInCartSelectList = new SelectList(await _bll.PizzaInCarts.GetAllAsync(User.UserGuidId()), nameof(PizzaInCart.Id), nameof(PizzaInCart.Id));
            return View(invoiceLine);
        }

        // GET: InvoiceLines/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var invoiceLine = await _bll.InvoiceLines.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (invoiceLine == null)
            {
                return NotFound();
            }
            invoiceLine.InvoiceSelectList = new SelectList(await _bll.Invoices.GetAllAsync(User.UserGuidId()), nameof(Invoice.Id), nameof(Invoice.Id));
            invoiceLine.DrinkInCartSelectList = new SelectList(await _bll.DrinkInCarts.GetAllAsync(User.UserGuidId()), nameof(DrinkInCart.Id), nameof(DrinkInCart.Id));
            invoiceLine.PizzaInCartSelectList = new SelectList(await _bll.PizzaInCarts.GetAllAsync(User.UserGuidId()), nameof(PizzaInCart.Id), nameof(PizzaInCart.Id));
            return View(invoiceLine);
        }

        // POST: InvoiceLines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, InvoiceLine invoiceLine)
        {
            if (id != invoiceLine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.InvoiceLines.UpdateAsync(invoiceLine);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            invoiceLine.InvoiceSelectList = new SelectList(await _bll.Invoices.GetAllAsync(User.UserGuidId()), nameof(Invoice.Id), nameof(Invoice.Id));
            invoiceLine.DrinkInCartSelectList = new SelectList(await _bll.DrinkInCarts.GetAllAsync(User.UserGuidId()), nameof(DrinkInCart.Id), nameof(DrinkInCart.Id));
            invoiceLine.PizzaInCartSelectList = new SelectList(await _bll.PizzaInCarts.GetAllAsync(User.UserGuidId()), nameof(PizzaInCart.Id), nameof(PizzaInCart.Id));
            return View(invoiceLine);
        }

        // GET: InvoiceLines/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceLine = await _bll.InvoiceLines.FirstOrDefaultAsync(id.Value, User.UserGuidId());
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
            await _bll.InvoiceLines.RemoveAsync(id, User.UserGuidId());
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}