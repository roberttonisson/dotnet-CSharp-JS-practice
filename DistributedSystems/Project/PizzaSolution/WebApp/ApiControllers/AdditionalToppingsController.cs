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
    public class AdditionalToppingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdditionalToppingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AdditionalToppings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdditionalTopping>>> GetAdditionalToppings()
        {
            return await _context.AdditionalToppings.ToListAsync();
        }

        // GET: api/AdditionalToppings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdditionalTopping>> GetAdditionalTopping(Guid id)
        {
            var additionalTopping = await _context.AdditionalToppings.FindAsync(id);

            if (additionalTopping == null)
            {
                return NotFound();
            }

            return additionalTopping;
        }

        // PUT: api/AdditionalToppings/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdditionalTopping(Guid id, AdditionalTopping additionalTopping)
        {
            if (id != additionalTopping.Id)
            {
                return BadRequest();
            }

            _context.Entry(additionalTopping).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdditionalToppingExists(id))
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

        // POST: api/AdditionalToppings
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<AdditionalTopping>> PostAdditionalTopping(AdditionalTopping additionalTopping)
        {
            _context.AdditionalToppings.Add(additionalTopping);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdditionalTopping", new { id = additionalTopping.Id }, additionalTopping);
        }

        // DELETE: api/AdditionalToppings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AdditionalTopping>> DeleteAdditionalTopping(Guid id)
        {
            var additionalTopping = await _context.AdditionalToppings.FindAsync(id);
            if (additionalTopping == null)
            {
                return NotFound();
            }

            _context.AdditionalToppings.Remove(additionalTopping);
            await _context.SaveChangesAsync();

            return additionalTopping;
        }

        private bool AdditionalToppingExists(Guid id)
        {
            return _context.AdditionalToppings.Any(e => e.Id == id);
        }
    }
}
