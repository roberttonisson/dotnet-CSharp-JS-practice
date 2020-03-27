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
    public class PizzaInCartsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PizzaInCartsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PizzaInCarts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PizzaInCart>>> GetPizzaInCarts()
        {
            return await _context.PizzaInCarts.ToListAsync();
        }

        // GET: api/PizzaInCarts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PizzaInCart>> GetPizzaInCart(Guid id)
        {
            var pizzaInCart = await _context.PizzaInCarts.FindAsync(id);

            if (pizzaInCart == null)
            {
                return NotFound();
            }

            return pizzaInCart;
        }

        // PUT: api/PizzaInCarts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizzaInCart(Guid id, PizzaInCart pizzaInCart)
        {
            if (id != pizzaInCart.Id)
            {
                return BadRequest();
            }

            _context.Entry(pizzaInCart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PizzaInCartExists(id))
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

        // POST: api/PizzaInCarts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PizzaInCart>> PostPizzaInCart(PizzaInCart pizzaInCart)
        {
            _context.PizzaInCarts.Add(pizzaInCart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPizzaInCart", new { id = pizzaInCart.Id }, pizzaInCart);
        }

        // DELETE: api/PizzaInCarts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PizzaInCart>> DeletePizzaInCart(Guid id)
        {
            var pizzaInCart = await _context.PizzaInCarts.FindAsync(id);
            if (pizzaInCart == null)
            {
                return NotFound();
            }

            _context.PizzaInCarts.Remove(pizzaInCart);
            await _context.SaveChangesAsync();

            return pizzaInCart;
        }

        private bool PizzaInCartExists(Guid id)
        {
            return _context.PizzaInCarts.Any(e => e.Id == id);
        }
    }
}
