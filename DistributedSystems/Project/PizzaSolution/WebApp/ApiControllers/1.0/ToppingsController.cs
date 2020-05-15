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
    public class ToppingsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ToppingDTOMapper _mapper = new ToppingDTOMapper();

        public ToppingsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/Toppings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToppingDTO>>> GetToppings()
        {
            var toppings = (await _bll.Toppings.GetAllAsync(null))
                .Select(bllEntity => _mapper.Map(bllEntity));
            
            return Ok(toppings);
        }

     // GET: api/Toppings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToppingDTO>> GetTopping(Guid id)
        {
            var topping = await _bll.Toppings.FirstOrDefaultAsync(id);

            if (topping == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(topping));
        }

        // PUT: api/Toppings/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTopping(Guid id, ToppingDTO toppingDTO)
        {
            if (id != toppingDTO.Id)
            {
                return BadRequest();
            }

            await _bll.Toppings.UpdateAsync(_mapper.Map(toppingDTO));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/Toppings
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ToppingDTO>> PostTopping(ToppingDTO toppingDTO)
        {
            var bllEntity = _mapper.Map(toppingDTO);
            _bll.Toppings.Add(bllEntity);
            await _bll.SaveChangesAsync();

            toppingDTO.Id = bllEntity.Id;

            return Ok(toppingDTO);
        }

        // DELETE: api/Toppings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ToppingDTO>> DeleteTopping(Guid id)
        {
            var topping = await _bll.Toppings.FirstOrDefaultAsync(id);
            if (topping == null)
            {
                return NotFound();
            }

            await _bll.Toppings.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(topping));
        }
        
    }
}
