using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PartyOrderInvoicesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PartyOrderInvoicesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PartyOrderInvoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartyOrderInvoice>>> GetPartyOrderInvoices()
        {
            return await _context.PartyOrderInvoices.ToListAsync();
        }

        // GET: api/PartyOrderInvoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PartyOrderInvoice>> GetPartyOrderInvoice(Guid id)
        {
            var partyOrderInvoice = await _context.PartyOrderInvoices.FindAsync(id);

            if (partyOrderInvoice == null)
            {
                return NotFound();
            }

            return partyOrderInvoice;
        }

        // PUT: api/PartyOrderInvoices/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPartyOrderInvoice(Guid id, PartyOrderInvoice partyOrderInvoice)
        {
            if (id != partyOrderInvoice.Id)
            {
                return BadRequest();
            }

            _context.Entry(partyOrderInvoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartyOrderInvoiceExists(id))
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

        // POST: api/PartyOrderInvoices
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PartyOrderInvoice>> PostPartyOrderInvoice(PartyOrderInvoice partyOrderInvoice)
        {
            _context.PartyOrderInvoices.Add(partyOrderInvoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPartyOrderInvoice", new { id = partyOrderInvoice.Id }, partyOrderInvoice);
        }

        // DELETE: api/PartyOrderInvoices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PartyOrderInvoice>> DeletePartyOrderInvoice(Guid id)
        {
            var partyOrderInvoice = await _context.PartyOrderInvoices.FindAsync(id);
            if (partyOrderInvoice == null)
            {
                return NotFound();
            }

            _context.PartyOrderInvoices.Remove(partyOrderInvoice);
            await _context.SaveChangesAsync();

            return partyOrderInvoice;
        }

        private bool PartyOrderInvoiceExists(Guid id)
        {
            return _context.PartyOrderInvoices.Any(e => e.Id == id);
        }
    }
}
