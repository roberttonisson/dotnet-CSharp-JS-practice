using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Controller for Transports.
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TransportsController : ControllerBase
    {
        /// <summary>
        /// Controller for Transports
        /// </summary>
        private readonly IAppBLL _bll;
        private readonly TransportDTOMapper _mapper = new TransportDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public TransportsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/Transports
        /// <summary>
        /// Get the list of all Transports for specific user.
        /// </summary>
        /// <returns>List of Transports</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransportDTO>>> GetTransports()
        {
            var transports = (await _bll.Transports.GetAllAsync(null))
                .Select(bllEntity => _mapper.Map(bllEntity));
            
            return Ok(transports);
        }

        // GET: api/Transports/5
        /// <summary>
        /// Get single Transport by given id
        /// </summary>
        /// <param name="id">Id of the Transport that we are returning</param>
        /// <returns>Transport</returns>
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
        /// <summary>
        /// Change existing Transport by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the Transport from DB</param>
        /// <param name="transportDTO">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
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
        /// <summary>
        /// Add a new Transport to the DB.
        /// </summary>
        /// <param name="transportDTO">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
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
        /// <summary>
        /// Deletes a Transport record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")][Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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
