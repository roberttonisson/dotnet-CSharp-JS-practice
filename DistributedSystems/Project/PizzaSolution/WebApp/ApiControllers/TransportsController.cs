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
    public class TransportsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        private readonly AppDbContext _context;

        public TransportsController(IAppUnitOfWork uow, AppDbContext context)
        {
            _context = context;
            _uow = uow;
        }

        // GET: api/Transports
        [HttpGet]
        public async Task<IEnumerable<TransportDTO>> GetTransports()
        {
            return await _uow.Transports.SelectAllDTO();
        }

        // GET: api/Transports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransportDTO>> GetTransport(Guid id)
        {
            //var transport = await _context.Transports.FindAsync(id);
            var transport = await _uow.Transports.SelectDTO(id);

            if (transport == null)
            {
                return NotFound();
            }

            return transport;
        }

        // PUT: api/Transports/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransport(Guid id, Transport transport)
        {
            if (id != transport.Id)
            {
                return BadRequest();
            }

            _context.Entry(transport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransportExists(id))
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

        // POST: api/Transports
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Transport>> PostTransport(TransportDTO dto)
        {
            var transport = new Transport()
            {
                Address = dto.Address,
                Cost = dto.Cost,
            };
            _uow.Transports.Add(transport);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetTransport", new { id = transport.Id }, transport);
        }

        // DELETE: api/Transports/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Transport>> DeleteTransport(Guid id)
        {
            var transport = await _uow.Transports.FindAsync(id);
            if (transport == null)
            {
                return NotFound();
            }

            _uow.Transports.Remove(transport);
            await _uow.SaveChangesAsync();

            return transport;
        }

        private bool TransportExists(Guid id)
        {
            return _context.Transports.Any(e => e.Id == id);
        }
    }
}
