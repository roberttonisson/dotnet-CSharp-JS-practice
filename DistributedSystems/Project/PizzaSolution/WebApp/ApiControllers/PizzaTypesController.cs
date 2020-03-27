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
    public class PizzaTypesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PizzaTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PizzaTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PizzaType>>> GetPizzaTypes()
        {
            return await _context.PizzaTypes.ToListAsync();
        }

        // GET: api/PizzaTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PizzaType>> GetPizzaType(Guid id)
        {
            var pizzaType = await _context.PizzaTypes.FindAsync(id);

            if (pizzaType == null)
            {
                return NotFound();
            }

            return pizzaType;
        }

        // PUT: api/PizzaTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizzaType(Guid id, PizzaType pizzaType)
        {
            if (id != pizzaType.Id)
            {
                return BadRequest();
            }

            _context.Entry(pizzaType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PizzaTypeExists(id))
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

        // POST: api/PizzaTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PizzaType>> PostPizzaType(PizzaType pizzaType)
        {
            _context.PizzaTypes.Add(pizzaType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPizzaType", new { id = pizzaType.Id }, pizzaType);
        }

        // DELETE: api/PizzaTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PizzaType>> DeletePizzaType(Guid id)
        {
            var pizzaType = await _context.PizzaTypes.FindAsync(id);
            if (pizzaType == null)
            {
                return NotFound();
            }

            _context.PizzaTypes.Remove(pizzaType);
            await _context.SaveChangesAsync();

            return pizzaType;
        }

        private bool PizzaTypeExists(Guid id)
        {
            return _context.PizzaTypes.Any(e => e.Id == id);
        }
    }
}
