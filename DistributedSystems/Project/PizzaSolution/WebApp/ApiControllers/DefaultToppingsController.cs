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
    public class DefaultToppingsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly DefaultToppingDTOMapper _mapper = new DefaultToppingDTOMapper();

        public DefaultToppingsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/DefaultToppings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DefaultToppingDTO>>> GetDefaultToppings()
        {
            var defaultToppings = (await _bll.DefaultToppings.GetAllAsync(null))
                .Select(bllEntity => _mapper.Map(bllEntity));
            
            return Ok(defaultToppings);
        }

     // GET: api/DefaultToppings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DefaultToppingDTO>> GetDefaultTopping(Guid id)
        {
            var defaultTopping = await _bll.DefaultToppings.FirstOrDefaultAsync(id);

            if (defaultTopping == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(defaultTopping));
        }

        // PUT: api/DefaultToppings/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDefaultTopping(Guid id, DefaultToppingDTO defaultToppingDTO)
        {
            if (id != defaultToppingDTO.Id)
            {
                return BadRequest();
            }

            await _bll.DefaultToppings.UpdateAsync(_mapper.Map(defaultToppingDTO));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/DefaultToppings
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<DefaultToppingDTO>> PostDefaultTopping(DefaultToppingDTO defaultToppingDTO)
        {
            var bllEntity = _mapper.Map(defaultToppingDTO);
            _bll.DefaultToppings.Add(bllEntity);
            await _bll.SaveChangesAsync();

            defaultToppingDTO.Id = bllEntity.Id;

            return Ok(defaultToppingDTO);
        }

        // DELETE: api/DefaultToppings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DefaultToppingDTO>> DeleteDefaultTopping(Guid id)
        {
            var defaultTopping = await _bll.DefaultToppings.FirstOrDefaultAsync(id);
            if (defaultTopping == null)
            {
                return NotFound();
            }

            await _bll.DefaultToppings.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(defaultTopping));
        }
        
    }
}
