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
using PartyOrderInvoice = BLL.App.DTO.PartyOrderInvoice;

namespace WebApp.Controllers

{
    [Authorize]
    public class PartyOrderInvoicesController : Controller
    {
        private readonly IAppBLL _bll;

        public PartyOrderInvoicesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: PartyOrderInvoices
        public async Task<IActionResult> Index()
        {
            return View(await _bll.PartyOrderInvoices.GetAllAsync(User.UserGuidId()));
        }

        // GET: PartyOrderInvoices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partyOrderInvoice = await _bll.PartyOrderInvoices.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (partyOrderInvoice == null)
            {
                return NotFound();
            }

            return View(partyOrderInvoice);
        }

        // GET: PartyOrderInvoices/Create
        public async Task<IActionResult> Create()
        {
            var partyOrderInvoice = new PartyOrderInvoice();
            partyOrderInvoice.PartyOrderSelectList = new SelectList(await _bll.PartyOrders.GetAllAsync(User.UserGuidId()), nameof(PartyOrder.Id), nameof(PartyOrder.Id));
            partyOrderInvoice.InvoiceSelectList = new SelectList(await _bll.Invoices.GetAllAsync(User.UserGuidId()), nameof(Invoice.Id), nameof(Invoice.Id));
            return View(partyOrderInvoice);
        }

        // POST: PartyOrderInvoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.PartyOrderInvoice partyOrderInvoice)
        {
            if (ModelState.IsValid)
            {
                _bll.PartyOrderInvoices.Add(partyOrderInvoice);
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            partyOrderInvoice.PartyOrderSelectList = new SelectList(await _bll.PartyOrders.GetAllAsync(User.UserGuidId()), nameof(PartyOrder.Id), nameof(PartyOrder.Id));
            partyOrderInvoice.InvoiceSelectList = new SelectList(await _bll.Invoices.GetAllAsync(User.UserGuidId()), nameof(Invoice.Id), nameof(Invoice.Id));
            return View(partyOrderInvoice);
        }

        // GET: PartyOrderInvoices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var partyOrderInvoice = await _bll.PartyOrderInvoices.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (partyOrderInvoice == null)
            {
                return NotFound();
            }
            partyOrderInvoice.PartyOrderSelectList = new SelectList(await _bll.PartyOrders.GetAllAsync(User.UserGuidId()), nameof(PartyOrder.Id), nameof(PartyOrder.Id));
            partyOrderInvoice.InvoiceSelectList = new SelectList(await _bll.Invoices.GetAllAsync(User.UserGuidId()), nameof(Invoice.Id), nameof(Invoice.Id));

            return View(partyOrderInvoice);
        }

        // POST: PartyOrderInvoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PartyOrderInvoice partyOrderInvoice)
        {
            if (id != partyOrderInvoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.PartyOrderInvoices.UpdateAsync(partyOrderInvoice);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            partyOrderInvoice.PartyOrderSelectList = new SelectList(await _bll.PartyOrders.GetAllAsync(User.UserGuidId()), nameof(PartyOrder.Id), nameof(PartyOrder.Id));
            partyOrderInvoice.InvoiceSelectList = new SelectList(await _bll.Invoices.GetAllAsync(User.UserGuidId()), nameof(Invoice.Id), nameof(Invoice.Id));
            return View(partyOrderInvoice);
        }

        // GET: PartyOrderInvoices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partyOrderInvoice = await _bll.PartyOrderInvoices.FirstOrDefaultAsync(id.Value, User.UserGuidId());
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
            await _bll.PartyOrderInvoices.RemoveAsync(id, User.UserGuidId());
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}