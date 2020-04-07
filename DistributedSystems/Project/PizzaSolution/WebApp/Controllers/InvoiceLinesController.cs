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
    [Authorize(Roles = "Admin")]
    public class InvoiceLinesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public InvoiceLinesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: InvoiceLines
        public async Task<IActionResult> Index()
        {
            return View(await _uow.InvoiceLines.GetIncluded(User.UserGuidId()));
        }

        // GET: InvoiceLines/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceLine = await _uow.InvoiceLines.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (invoiceLine == null)
            {
                return NotFound();
            }

            return View(invoiceLine);
        }

             // GET: InvoiceLines/Create
        public async Task<IActionResult> Create()
        {
            var vm =  new InvoiceLineCreateEditViewModel();
            vm.InvoiceSelectList = new SelectList(await _uow.Invoices.GetIncluded(User.UserGuidId()), nameof(Invoice.Id), nameof(Invoice.Id));
            vm.DrinkInCartSelectList = new SelectList(await _uow.DrinkInCarts.GetIncluded(User.UserGuidId()), nameof(DrinkInCart.Id), nameof(DrinkInCart.Id));
            vm.PizzaInCartSelectList = new SelectList(await _uow.PizzaInCarts.GetIncluded(User.UserGuidId()), nameof(PizzaInCart.Id), nameof(PizzaInCart.Id));
            return View(vm);
        }

        // POST: InvoiceLines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InvoiceLineCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //invoiceLine.Id = Guid.NewGuid();
                _uow.InvoiceLines.Add(vm.InvoiceLine);
                await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            vm.InvoiceSelectList = new SelectList(await _uow.Invoices.GetIncluded(User.UserGuidId()), nameof(Invoice.Id), nameof(Invoice.Id));
            vm.DrinkInCartSelectList = new SelectList(await _uow.DrinkInCarts.GetIncluded(User.UserGuidId()), nameof(DrinkInCart.Id), nameof(DrinkInCart.Id));
            vm.PizzaInCartSelectList = new SelectList(await _uow.PizzaInCarts.GetIncluded(User.UserGuidId()), nameof(PizzaInCart.Id), nameof(PizzaInCart.Id));
            return View(vm);
        }

        // GET: InvoiceLines/Edit/5
        public async Task<IActionResult> Edit(Guid? id, InvoiceLineCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.InvoiceLine = await _uow.InvoiceLines.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (vm.InvoiceLine == null)
            {
                return NotFound();
            }
            vm.InvoiceSelectList = new SelectList(await _uow.Invoices.GetIncluded(User.UserGuidId()), nameof(Invoice.Id), nameof(Invoice.Id));
            vm.DrinkInCartSelectList = new SelectList(await _uow.DrinkInCarts.GetIncluded(User.UserGuidId()), nameof(DrinkInCart.Id), nameof(DrinkInCart.Id));
            vm.PizzaInCartSelectList = new SelectList(await _uow.PizzaInCarts.GetIncluded(User.UserGuidId()), nameof(PizzaInCart.Id), nameof(PizzaInCart.Id));

            return View(vm);
        }

        // POST: InvoiceLines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, InvoiceLineCreateEditViewModel vm)
        {
            if (id != vm.InvoiceLine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.InvoiceLines.Update(vm.InvoiceLine);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            vm.InvoiceSelectList = new SelectList(await _uow.Invoices.GetIncluded(User.UserGuidId()), nameof(Invoice.Id), nameof(Invoice.Id));
            vm.DrinkInCartSelectList = new SelectList(await _uow.DrinkInCarts.GetIncluded(User.UserGuidId()), nameof(DrinkInCart.Id), nameof(DrinkInCart.Id));
            vm.PizzaInCartSelectList = new SelectList(await _uow.PizzaInCarts.GetIncluded(User.UserGuidId()), nameof(PizzaInCart.Id), nameof(PizzaInCart.Id));
            return View(vm);
        }

        // GET: InvoiceLines/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceLine = await _uow.InvoiceLines.FirstOrDefaultAsync(id.Value, User.UserGuidId());
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
            await _uow.InvoiceLines.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}