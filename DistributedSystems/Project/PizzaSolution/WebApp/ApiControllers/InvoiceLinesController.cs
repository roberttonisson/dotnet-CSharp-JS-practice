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
    public class InvoiceLinesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InvoiceLinesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/InvoiceLines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceLine>>> GetInvoiceLines()
        {
            return await _context.InvoiceLines.ToListAsync();
        }

        // GET: api/InvoiceLines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceLine>> GetInvoiceLine(Guid id)
        {
            var invoiceLine = await _context.InvoiceLines.FindAsync(id);

            if (invoiceLine == null)
            {
                return NotFound();
            }

            return invoiceLine;
        }

        // PUT: api/InvoiceLines/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoiceLine(Guid id, InvoiceLine invoiceLine)
        {
            if (id != invoiceLine.Id)
            {
                return BadRequest();
            }

            _context.Entry(invoiceLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceLineExists(id))
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

        // POST: api/InvoiceLines
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<InvoiceLine>> PostInvoiceLine(InvoiceLine invoiceLine)
        {
            _context.InvoiceLines.Add(invoiceLine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvoiceLine", new { id = invoiceLine.Id }, invoiceLine);
        }

        // DELETE: api/InvoiceLines/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<InvoiceLine>> DeleteInvoiceLine(Guid id)
        {
            var invoiceLine = await _context.InvoiceLines.FindAsync(id);
            if (invoiceLine == null)
            {
                return NotFound();
            }

            _context.InvoiceLines.Remove(invoiceLine);
            await _context.SaveChangesAsync();

            return invoiceLine;
        }

        private bool InvoiceLineExists(Guid id)
        {
            return _context.InvoiceLines.Any(e => e.Id == id);
        }
    }
}
