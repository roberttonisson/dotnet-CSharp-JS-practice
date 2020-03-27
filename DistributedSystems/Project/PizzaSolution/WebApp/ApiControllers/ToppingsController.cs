using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToppingsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAppUnitOfWork _uow;

        public ToppingsController(AppDbContext context, IAppUnitOfWork uow)
        {
            _uow = uow;
            _context = context;
        }

        // GET: api/Toppings
        [HttpGet]
        public async Task<IEnumerable<ToppingDTO>> GetToppings()
        {
            return await _uow.Toppings.SelectAllDTO();
        }

        // GET: api/Toppings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToppingDTO>> GetTopping(Guid id)
        {
            var topping = await _uow.Toppings.SelectDTO(id);

            if (topping == null)
            {
                return NotFound();
            }

            return topping;
        }

        // PUT: api/Toppings/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTopping(Guid id, Topping topping)
        {
            if (id != topping.Id)
            {
                return BadRequest();
            }

            _context.Entry(topping).State = EntityState.Modified;

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToppingExists(id))
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

        // POST: api/Toppings
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Topping>> PostTopping(Topping topping)
        {
            _uow.Toppings.Add(topping);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetTopping", new { id = topping.Id }, topping);
        }

        // DELETE: api/Toppings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Topping>> DeleteTopping(Guid id)
        {
            var topping = await _uow.Toppings.FindAsync(id);
            if (topping == null)
            {
                return NotFound();
            }

            _uow.Toppings.Remove(topping);
            await _uow.SaveChangesAsync();

            return topping;
        }

        private bool ToppingExists(Guid id)
        {
            return _context.Toppings.Any(e => e.Id == id);
        }
    }
}
