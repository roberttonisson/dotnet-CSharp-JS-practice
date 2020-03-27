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
    public class SizesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAppUnitOfWork _uow;

        public SizesController(AppDbContext context, IAppUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        // GET: api/Sizes
        [HttpGet]
        public async Task<IEnumerable<SizeDTO>> GetSizes()
        {
            return await _uow.Sizes.SelectAllDTO();
        }

        // GET: api/Sizes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SizeDTO>> GetSize(Guid id)
        {
            var size = await _uow.Sizes.SelectDTO();

            if (size == null)
            {
                return NotFound();
            }

            return size;
        }

        // PUT: api/Sizes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSize(Guid id, Size size)
        {
            if (id != size.Id)
            {
                return BadRequest();
            }

            _context.Entry(size).State = EntityState.Modified;

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SizeExists(id))
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

        // POST: api/Sizes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Size>> PostSize(Size size)
        {
            _uow.Sizes.Add(size);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetSize", new { id = size.Id }, size);
        }

        // DELETE: api/Sizes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Size>> DeleteSize(Guid id)
        {
            var size = await _uow.Sizes.FindAsync(id);
            if (size == null)
            {
                return NotFound();
            }

            _uow.Sizes.Remove(size);
            await _uow.SaveChangesAsync();

            return size;
        }

        private bool SizeExists(Guid id)
        {
            return _context.Sizes.Any(e => e.Id == id);
        }
    }
}
