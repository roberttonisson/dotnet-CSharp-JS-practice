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
    /// Controller for PartyOrderInvoices
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PartyOrderInvoicesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PartyOrderInvoiceDTOMapper _mapper = new PartyOrderInvoiceDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public PartyOrderInvoicesController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/PartyOrderInvoices
        /// <summary>
        /// Get the list of all PartyOrderInvoices for specific user.
        /// </summary>
        /// <returns>List of PartyOrderInvoices</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartyOrderInvoiceDTO>>> GetPartyOrderInvoices()
        {
            var partyOrderInvoices = (await _bll.PartyOrderInvoices.GetAllAsync(User.UserGuidId()))
                .Select(bllEntity => _mapper.Map(bllEntity));
            
            return Ok(partyOrderInvoices);
        }

        // GET: api/PartyOrderInvoices/5
        /// <summary>
        /// Get single PartyOrderInvoice by given id
        /// </summary>
        /// <param name="id">Id of the PartyOrderInvoice that we are returning</param>
        /// <returns>PartyOrderInvoice</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PartyOrderInvoiceDTO>> GetPartyOrderInvoice(Guid id)
        {
            var partyOrderInvoice = await _bll.PartyOrderInvoices.FirstOrDefaultAsync(id, User.UserGuidId());

            if (partyOrderInvoice == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(partyOrderInvoice));
        }

        // PUT: api/PartyOrderInvoices/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Change existing PartyOrderInvoice by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the PartyOrderInvoice from DB</param>
        /// <param name="partyOrderInvoiceDTO">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPartyOrderInvoice(Guid id, PartyOrderInvoiceDTO partyOrderInvoiceDTO)
        {
            if (id != partyOrderInvoiceDTO.Id)
            {
                return BadRequest();
            }

            await _bll.PartyOrderInvoices.UpdateAsync(_mapper.Map(partyOrderInvoiceDTO));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/PartyOrderInvoices
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new PartyOrderInvoice to the DB.
        /// </summary>
        /// <param name="partyOrderInvoiceDTO">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        public async Task<ActionResult<PartyOrderInvoiceDTO>> PostPartyOrderInvoice(PartyOrderInvoiceDTO partyOrderInvoiceDTO)
        {
            var bllEntity = _mapper.Map(partyOrderInvoiceDTO);
            _bll.PartyOrderInvoices.Add(bllEntity);
            await _bll.SaveChangesAsync();

            partyOrderInvoiceDTO.Id = bllEntity.Id;

            return Ok(partyOrderInvoiceDTO);
        }

                // DELETE: api/PartyOrderInvoices/5
        /// <summary>
        /// Deletes a PartyOrderInvoice record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<PartyOrderInvoiceDTO>> DeletePartyOrderInvoice(Guid id)
        {
            var partyOrderInvoice = await _bll.PartyOrderInvoices.FirstOrDefaultAsync(id, User.UserGuidId());
            if (partyOrderInvoice == null)
            {
                return NotFound();
            }

            await _bll.PartyOrderInvoices.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(partyOrderInvoice));
        }
        
    }
}
