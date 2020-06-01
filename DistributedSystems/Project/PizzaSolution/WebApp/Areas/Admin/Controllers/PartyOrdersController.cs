#pragma warning disable 1591
using System;
using System.Threading.Tasks;
using BLL.App.DTO.Identity;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PartyOrder = BLL.App.DTO.PartyOrder;

namespace WebApp.Areas.Admin.Controllers

{
    [Authorize(Roles = "Admin")][Area("Admin")]
    public class PartyOrdersController : Controller
    {
        private readonly IAppBLL _bll;

        public PartyOrdersController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: PartyOrders
        public async Task<IActionResult> Index()
        {
            return View(await _bll.PartyOrders.GetAllAsync(User.UserGuidId()));
        }

        // GET: PartyOrders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partyOrder = await _bll.PartyOrders.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (partyOrder == null)
            {
                return NotFound();
            }

            return View(partyOrder);
        }

        // GET: PartyOrders/Create
        public async Task<IActionResult> Create()
        {
            var partyOrder = new PartyOrder();
            partyOrder.AppUserSelectList = new SelectList(await _bll.AppUsers.GetAllAsync(), nameof(AppUser.Id), nameof(Domain.Identity.AppUser.Email));
            return View(partyOrder);
        }

        // POST: PartyOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.PartyOrder partyOrder)
        {
            if (ModelState.IsValid)
            {
                _bll.PartyOrders.Add(partyOrder);
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            partyOrder.AppUserSelectList = new SelectList(await _bll.AppUsers.GetAllAsync(), nameof(AppUser.Id), nameof(Domain.Identity.AppUser.Email));
            return View(partyOrder);
        }

        // GET: PartyOrders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var partyOrder = await _bll.PartyOrders.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (partyOrder == null)
            {
                return NotFound();
            }
            partyOrder.AppUserSelectList = new SelectList(await _bll.AppUsers.GetAllAsync(), nameof(AppUser.Id), nameof(Domain.Identity.AppUser.Email));

            return View(partyOrder);
        }

        // POST: PartyOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PartyOrder partyOrder)
        {
            if (id != partyOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.PartyOrders.UpdateAsync(partyOrder);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            partyOrder.AppUserSelectList = new SelectList(await _bll.AppUsers.GetAllAsync(), nameof(AppUser.Id), nameof(Domain.Identity.AppUser.Email));
            return View(partyOrder);
        }

        // GET: PartyOrders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partyOrder = await _bll.PartyOrders.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (partyOrder == null)
            {
                return NotFound();
            }

            return View(partyOrder);
        }

        // POST: PartyOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.PartyOrders.RemoveAsync(id, User.UserGuidId());
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}