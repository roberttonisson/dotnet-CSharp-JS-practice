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
    public class DefaultToppingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DefaultToppingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/DefaultToppings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DefaultTopping>>> GetDefaultToppings()
        {
            return await _context.DefaultToppings.ToListAsync();
        }

        // GET: api/DefaultToppings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DefaultTopping>> GetDefaultTopping(Guid id)
        {
            var defaultTopping = await _context.DefaultToppings.FindAsync(id);

            if (defaultTopping == null)
            {
                return NotFound();
            }

            return defaultTopping;
        }

        // PUT: api/DefaultToppings/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDefaultTopping(Guid id, DefaultTopping defaultTopping)
        {
            if (id != defaultTopping.Id)
            {
                return BadRequest();
            }

            _context.Entry(defaultTopping).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DefaultToppingExists(id))
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

        // POST: api/DefaultToppings
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<DefaultTopping>> PostDefaultTopping(DefaultTopping defaultTopping)
        {
            _context.DefaultToppings.Add(defaultTopping);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDefaultTopping", new { id = defaultTopping.Id }, defaultTopping);
        }

        // DELETE: api/DefaultToppings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DefaultTopping>> DeleteDefaultTopping(Guid id)
        {
            var defaultTopping = await _context.DefaultToppings.FindAsync(id);
            if (defaultTopping == null)
            {
                return NotFound();
            }

            _context.DefaultToppings.Remove(defaultTopping);
            await _context.SaveChangesAsync();

            return defaultTopping;
        }

        private bool DefaultToppingExists(Guid id)
        {
            return _context.DefaultToppings.Any(e => e.Id == id);
        }
    }
}
