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
    public class PartyOrdersController : Controller
    {
        private readonly AppDbContext _context;

        public PartyOrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PartyOrders
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.PartyOrders.Include(p => p.Owner);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PartyOrders/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partyOrder = await _context.PartyOrders
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partyOrder == null)
            {
                return NotFound();
            }

            return View(partyOrder);
        }

        // GET: PartyOrders/Create
        public IActionResult Create()
        {
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: PartyOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Start,End,Address,InviteKey,OwnerId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] PartyOrder partyOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(partyOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", partyOrder.OwnerId);
            return View(partyOrder);
        }

        // GET: PartyOrders/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partyOrder = await _context.PartyOrders.FindAsync(id);
            if (partyOrder == null)
            {
                return NotFound();
            }
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", partyOrder.OwnerId);
            return View(partyOrder);
        }

        // POST: PartyOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Start,End,Address,InviteKey,OwnerId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] PartyOrder partyOrder)
        {
            if (id != partyOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partyOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartyOrderExists(partyOrder.Id))
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
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", partyOrder.OwnerId);
            return View(partyOrder);
        }

        // GET: PartyOrders/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partyOrder = await _context.PartyOrders
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partyOrder == null)
            {
                return NotFound();
            }

            return View(partyOrder);
        }

        // POST: PartyOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var partyOrder = await _context.PartyOrders.FindAsync(id);
            _context.PartyOrders.Remove(partyOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartyOrderExists(string id)
        {
            return _context.PartyOrders.Any(e => e.Id == id);
        }
    }
}
