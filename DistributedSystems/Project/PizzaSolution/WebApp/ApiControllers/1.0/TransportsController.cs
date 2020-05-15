using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TransportsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly TransportDTOMapper _mapper = new TransportDTOMapper();

        public TransportsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/Transports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransportDTO>>> GetTransports()
        {
            var transports = (await _bll.Transports.GetAllAsync(null))
                .Select(bllEntity => _mapper.Map(bllEntity));
            
            return Ok(transports);
        }

     // GET: api/Transports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransportDTO>> GetTransport(Guid id)
        {
            var transport = await _bll.Transports.FirstOrDefaultAsync(id);

            if (transport == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(transport));
        }

        // PUT: api/Transports/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransport(Guid id, TransportDTO transportDTO)
        {
            if (id != transportDTO.Id)
            {
                return BadRequest();
            }

            await _bll.Transports.UpdateAsync(_mapper.Map(transportDTO));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/Transports
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TransportDTO>> PostTransport(TransportDTO transportDTO)
        {
            var bllEntity = _mapper.Map(transportDTO);
            _bll.Transports.Add(bllEntity);
            await _bll.SaveChangesAsync();

            transportDTO.Id = bllEntity.Id;

            return Ok(transportDTO);
        }

        // DELETE: api/Transports/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TransportDTO>> DeleteTransport(Guid id)
        {
            var transport = await _bll.Transports.FirstOrDefaultAsync(id);
            if (transport == null)
            {
                return NotFound();
            }

            await _bll.Transports.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(transport));
        }
        
    }
}
