using System;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;

namespace WebApp.Controllers
{
    public class PartyOrdersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPartyOrderRepository _partyOrderRepository;

        public PartyOrdersController(AppDbContext context)
        {
            _context = context;
            _partyOrderRepository = new PartyOrderRepository(_context);
        }

        // GET: PartyOrders
        public async Task<IActionResult> Index()
        {
            return View(await _partyOrderRepository.AllAsync());
        }

        // GET: PartyOrders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partyOrder = await _partyOrderRepository.FindAsync(id);

            if (partyOrder == null)
            {
                return NotFound();
            }

            return View(partyOrder);
        }

        // GET: PartyOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PartyOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Start,End,Address,InviteKey,OwnerId,CreatedBy,CreatedAt,CreatedBy,CreatedAt,Id")]
            PartyOrder partyOrder)
        {
            if (ModelState.IsValid)
            {
                //partyOrder.Id = Guid.NewGuid();
                _partyOrderRepository.Add(partyOrder);
                await _partyOrderRepository.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(partyOrder);
        }

        // GET: PartyOrders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partyOrder = await _partyOrderRepository.FindAsync(id);

            if (partyOrder == null)
            {
                return NotFound();
            }

            return View(partyOrder);
        }

        // POST: PartyOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Start,End,Address,InviteKey,OwnerId,CreatedBy,CreatedAt,CreatedBy,CreatedAt,Id")]
            PartyOrder partyOrder)
        {
            if (id != partyOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _partyOrderRepository.Update(partyOrder);
                await _partyOrderRepository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            return View(partyOrder);
        }

        // GET: PartyOrders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partyOrder = await _partyOrderRepository.FindAsync(id);
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
            var partyOrder = _partyOrderRepository.Remove(id);
            await _partyOrderRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}