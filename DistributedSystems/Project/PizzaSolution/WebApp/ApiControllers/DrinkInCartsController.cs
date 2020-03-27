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
    public class DrinkInCartsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DrinkInCartsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/DrinkInCarts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DrinkInCart>>> GetDrinkInCarts()
        {
            return await _context.DrinkInCarts.ToListAsync();
        }

        // GET: api/DrinkInCarts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DrinkInCart>> GetDrinkInCart(Guid id)
        {
            var drinkInCart = await _context.DrinkInCarts.FindAsync(id);

            if (drinkInCart == null)
            {
                return NotFound();
            }

            return drinkInCart;
        }

        // PUT: api/DrinkInCarts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDrinkInCart(Guid id, DrinkInCart drinkInCart)
        {
            if (id != drinkInCart.Id)
            {
                return BadRequest();
            }

            _context.Entry(drinkInCart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrinkInCartExists(id))
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

        // POST: api/DrinkInCarts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<DrinkInCart>> PostDrinkInCart(DrinkInCart drinkInCart)
        {
            _context.DrinkInCarts.Add(drinkInCart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDrinkInCart", new { id = drinkInCart.Id }, drinkInCart);
        }

        // DELETE: api/DrinkInCarts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DrinkInCart>> DeleteDrinkInCart(Guid id)
        {
            var drinkInCart = await _context.DrinkInCarts.FindAsync(id);
            if (drinkInCart == null)
            {
                return NotFound();
            }

            _context.DrinkInCarts.Remove(drinkInCart);
            await _context.SaveChangesAsync();

            return drinkInCart;
        }

        private bool DrinkInCartExists(Guid id)
        {
            return _context.DrinkInCarts.Any(e => e.Id == id);
        }
    }
}
