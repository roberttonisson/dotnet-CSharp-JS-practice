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
    public class AdditionalToppingsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly AdditionalToppingDTOMapper _mapper = new AdditionalToppingDTOMapper();

        public AdditionalToppingsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/AdditionalToppings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdditionalToppingDTO>>> GetAdditionalToppings()
        {
            var additionalToppings = (await _bll.AdditionalToppings.GetAllAsync(User.UserGuidId()))
                .Select(bllEntity => _mapper.GetDTO(bllEntity));
            
            return Ok(additionalToppings);
        }

     // GET: api/AdditionalToppings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdditionalToppingDTO>> GetAdditionalTopping(Guid id)
        {
            var additionalTopping = await _bll.AdditionalToppings.FirstOrDefaultAsync(id, User.UserGuidId());

            if (additionalTopping == null)
            {
                return NotFound();
            }

            return Ok(_mapper.GetDTO(additionalTopping));
        }

        // PUT: api/AdditionalToppings/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdditionalTopping(Guid id, AdditionalToppingDTO additionalToppingDTO)
        {
            if (id != additionalToppingDTO.Id)
            {
                return BadRequest();
            }

            await _bll.AdditionalToppings.UpdateAsync(_mapper.GetBLL(additionalToppingDTO));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/AdditionalToppings
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<AdditionalToppingDTO>> PostAdditionalTopping(AdditionalToppingDTO additionalToppingDTO)
        {
            var bllEntity = _mapper.GetBLL(additionalToppingDTO);
            _bll.AdditionalToppings.Add(bllEntity);
            await _bll.SaveChangesAsync();

            additionalToppingDTO.Id = bllEntity.Id;

            return Ok(additionalToppingDTO);
        }

        // DELETE: api/AdditionalToppings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AdditionalToppingDTO>> DeleteAdditionalTopping(Guid id)
        {
            var additionalTopping = await _bll.AdditionalToppings.FirstOrDefaultAsync(id, User.UserGuidId());
            if (additionalTopping == null)
            {
                return NotFound();
            }

            await _bll.AdditionalToppings.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.GetDTO(additionalTopping));
        }
        
    }
}
