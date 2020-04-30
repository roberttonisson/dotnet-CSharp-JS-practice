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

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PartyOrdersController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PartyOrderDTOMapper _mapper = new PartyOrderDTOMapper();

        public PartyOrdersController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/PartyOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartyOrderDTO>>> GetPartyOrders()
        {
            var partyOrders = (await _bll.PartyOrders.GetAllAsync(User.UserGuidId()))
                .Select(bllEntity => _mapper.GetDTO(bllEntity));
            
            return Ok(partyOrders);
        }

     // GET: api/PartyOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PartyOrderDTO>> GetPartyOrder(Guid id)
        {
            var partyOrder = await _bll.PartyOrders.FirstOrDefaultAsync(id, User.UserGuidId());

            if (partyOrder == null)
            {
                return NotFound();
            }

            return Ok(_mapper.GetDTO(partyOrder));
        }

        // PUT: api/PartyOrders/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPartyOrder(Guid id, PartyOrderDTO partyOrderDTO)
        {
            if (id != partyOrderDTO.Id)
            {
                return BadRequest();
            }

            await _bll.PartyOrders.UpdateAsync(_mapper.GetBLL(partyOrderDTO));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/PartyOrders
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PartyOrderDTO>> PostPartyOrder(PartyOrderDTO partyOrderDTO)
        {
            var bllEntity = _mapper.GetBLL(partyOrderDTO);
            _bll.PartyOrders.Add(bllEntity);
            await _bll.SaveChangesAsync();

            partyOrderDTO.Id = bllEntity.Id;

            return Ok(partyOrderDTO);
        }

        // DELETE: api/PartyOrders/5
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

            return Ok(_mapper.GetDTO(partyOrder));
        }
        
    }
}
