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
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PartyOrderInvoicesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PartyOrderInvoiceDTOMapper _mapper = new PartyOrderInvoiceDTOMapper();

        public PartyOrderInvoicesController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/PartyOrderInvoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartyOrderInvoiceDTO>>> GetPartyOrderInvoices()
        {
            var partyOrderInvoices = (await _bll.PartyOrderInvoices.GetAllAsync(User.UserGuidId()))
                .Select(bllEntity => _mapper.GetDTO(bllEntity));
            
            return Ok(partyOrderInvoices);
        }

     // GET: api/PartyOrderInvoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PartyOrderInvoiceDTO>> GetPartyOrderInvoice(Guid id)
        {
            var partyOrderInvoice = await _bll.PartyOrderInvoices.FirstOrDefaultAsync(id, User.UserGuidId());

            if (partyOrderInvoice == null)
            {
                return NotFound();
            }

            return Ok(_mapper.GetDTO(partyOrderInvoice));
        }

        // PUT: api/PartyOrderInvoices/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPartyOrderInvoice(Guid id, PartyOrderInvoiceDTO partyOrderInvoiceDTO)
        {
            if (id != partyOrderInvoiceDTO.Id)
            {
                return BadRequest();
            }

            await _bll.PartyOrderInvoices.UpdateAsync(_mapper.GetBLL(partyOrderInvoiceDTO));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/PartyOrderInvoices
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PartyOrderInvoiceDTO>> PostPartyOrderInvoice(PartyOrderInvoiceDTO partyOrderInvoiceDTO)
        {
            var bllEntity = _mapper.GetBLL(partyOrderInvoiceDTO);
            _bll.PartyOrderInvoices.Add(bllEntity);
            await _bll.SaveChangesAsync();

            partyOrderInvoiceDTO.Id = bllEntity.Id;

            return Ok(partyOrderInvoiceDTO);
        }

        // DELETE: api/PartyOrderInvoices/5
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

            return Ok(_mapper.GetDTO(partyOrderInvoice));
        }
        
    }
}
