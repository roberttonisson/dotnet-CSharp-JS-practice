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
using Invoice = BLL.App.DTO.Invoice;

namespace WebApp.Controllers

{
    [Authorize]
    public class InvoicesController : Controller
    {
        private readonly IAppBLL _bll;

        public InvoicesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Invoices.GetAllAsync(User.UserGuidId()));
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _bll.Invoices.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public async Task<IActionResult> Create()
        {
            var invoice = new Invoice();
            invoice.TransportSelectList = new SelectList(await _bll.Transports.GetAllAsync(), nameof(Transport.Id), nameof(Transport.Address));
            return View(invoice);
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                invoice.AppUserId = User.UserGuidId();
                _bll.Invoices.Add(invoice);
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            invoice.TransportSelectList = new SelectList(await _bll.Transports.GetAllAsync(), nameof(Transport.Id), nameof(Transport.Address));
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var invoice = await _bll.Invoices.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (invoice == null)
            {
                return NotFound();
            }
            invoice.TransportSelectList = new SelectList(await _bll.Transports.GetAllAsync(), nameof(Transport.Id), nameof(Transport.Address));

            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                invoice.AppUserId = User.UserGuidId();
                await _bll.Invoices.UpdateAsync(invoice);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            invoice.TransportSelectList = new SelectList(await _bll.Transports.GetAllAsync(), nameof(Transport.Id), nameof(Transport.Address));
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _bll.Invoices.FirstOrDefaultAsync(id.Value, User.UserGuidId());
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
            await _bll.Invoices.RemoveAsync(id, User.UserGuidId());
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}