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
        private readonly IAppUnitOfWork _uow;

        public SizesController(IAppUnitOfWork uow)
        {
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
            var size = await _uow.Sizes.SelectDTO(id);

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
        public async Task<IActionResult> PutSize(Guid id, SizeDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var size = _uow.Sizes.Find(dto.Id);
            if (size == null)
            {
                return BadRequest();
            }
            size.Name = dto.Name;
            size.Price = dto.Price;
            size.SizeCm = dto.SizeCm;

            _uow.Sizes.Update(size);

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
            return _uow.Sizes.Find(id) != null;
        }
    }
}
