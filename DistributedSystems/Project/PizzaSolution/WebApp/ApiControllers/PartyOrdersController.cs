using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartyOrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PartyOrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PartyOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartyOrder>>> GetPartyOrders()
        {
            return await _context.PartyOrders.ToListAsync();
        }

        // GET: api/PartyOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PartyOrder>> GetPartyOrder(Guid id)
        {
            var partyOrder = await _context.PartyOrders.FindAsync(id);

            if (partyOrder == null)
            {
                return NotFound();
            }

            return partyOrder;
        }

        // PUT: api/PartyOrders/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPartyOrder(Guid id, PartyOrder partyOrder)
        {
            if (id != partyOrder.Id)
            {
                return BadRequest();
            }

            _context.Entry(partyOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartyOrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PartyOrders
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PartyOrder>> PostPartyOrder(PartyOrder partyOrder)
        {
            _context.PartyOrders.Add(partyOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPartyOrder", new { id = partyOrder.Id }, partyOrder);
        }

        // DELETE: api/PartyOrders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PartyOrder>> DeletePartyOrder(Guid id)
        {
            var partyOrder = await _context.PartyOrders.FindAsync(id);
            if (partyOrder == null)
            {
                return NotFound();
            }

            _context.PartyOrders.Remove(partyOrder);
            await _context.SaveChangesAsync();

            return partyOrder;
        }

        private bool PartyOrderExists(Guid id)
        {
            return _context.PartyOrders.Any(e => e.Id == id);
        }
    }
}
