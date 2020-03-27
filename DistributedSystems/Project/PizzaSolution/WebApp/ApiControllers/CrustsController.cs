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
    public class CrustsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CrustsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Crusts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Crust>>> GetCrusts()
        {
            return await _context.Crusts.ToListAsync();
        }

        // GET: api/Crusts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Crust>> GetCrust(Guid id)
        {
            var crust = await _context.Crusts.FindAsync(id);

            if (crust == null)
            {
                return NotFound();
            }

            return crust;
        }

        // PUT: api/Crusts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCrust(Guid id, Crust crust)
        {
            if (id != crust.Id)
            {
                return BadRequest();
            }

            _context.Entry(crust).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CrustExists(id))
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

        // POST: api/Crusts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Crust>> PostCrust(Crust crust)
        {
            _context.Crusts.Add(crust);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCrust", new { id = crust.Id }, crust);
        }

        // DELETE: api/Crusts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Crust>> DeleteCrust(Guid id)
        {
            var crust = await _context.Crusts.FindAsync(id);
            if (crust == null)
            {
                return NotFound();
            }

            _context.Crusts.Remove(crust);
            await _context.SaveChangesAsync();

            return crust;
        }

        private bool CrustExists(Guid id)
        {
            return _context.Crusts.Any(e => e.Id == id);
        }
    }
}
