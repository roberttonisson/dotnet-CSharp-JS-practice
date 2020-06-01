using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Controller for PartyOrders
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PartyOrdersController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PartyOrderDTOMapper _mapper = new PartyOrderDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public PartyOrdersController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/PartyOrders
        /// <summary>
        /// Get the list of all PartyOrders for specific user.
        /// </summary>
        /// <returns>List of PartyOrders</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartyOrderDTO>>> GetPartyOrders()
        {
            var partyOrders = (await _bll.PartyOrders.GetAllAsync(User.UserGuidId()))
                .Select(bllEntity => _mapper.Map(bllEntity));
            
            return Ok(partyOrders);
        }

        // GET: api/PartyOrders/5
        /// <summary>
        /// Get single PartyOrder by given id
        /// </summary>
        /// <param name="id">Id of the PartyOrder that we are returning</param>
        /// <returns>PartyOrder</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PartyOrderDTO>> GetPartyOrder(Guid id)
        {
            var partyOrder = await _bll.PartyOrders.FirstOrDefaultAsync(id, User.UserGuidId());

            if (partyOrder == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(partyOrder));
        }

        // PUT: api/PartyOrders/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Change existing PartyOrder by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the PartyOrder from DB</param>
        /// <param name="partyOrderDTO">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPartyOrder(Guid id, PartyOrderDTO partyOrderDTO)
        {
            if (id != partyOrderDTO.Id)
            {
                return BadRequest();
            }

            await _bll.PartyOrders.UpdateAsync(_mapper.Map(partyOrderDTO));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/PartyOrders
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new PartyOrder to the DB.
        /// </summary>
        /// <param name="partyOrderDTO">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        public async Task<ActionResult<PartyOrderDTO>> PostPartyOrder(PartyOrderDTO partyOrderDTO)
        {
            var bllEntity = _mapper.Map(partyOrderDTO);
            _bll.PartyOrders.Add(bllEntity);
            await _bll.SaveChangesAsync();

            partyOrderDTO.Id = bllEntity.Id;

            return Ok(partyOrderDTO);
        }

        // DELETE: api/PartyOrders/5
        /// <summary>
        /// Deletes a PartyOrder record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<PartyOrderDTO>> DeletePartyOrder(Guid id)
        {
            var partyOrder = await _bll.PartyOrders.FirstOrDefaultAsync(id, User.UserGuidId());
            if (partyOrder == null)
            {
                return NotFound();
            }

            await _bll.PartyOrders.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(partyOrder));
        }
        
    }
}
