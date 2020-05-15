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
    public class DrinksController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly DrinkDTOMapper _mapper = new DrinkDTOMapper();

        public DrinksController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/Drinks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DrinkDTO>>> GetDrinks()
        {
            var drinks = (await _bll.Drinks.GetAllAsync(null))
                .Select(bllEntity => _mapper.Map(bllEntity));
            
            return Ok(drinks);
        }

     // GET: api/Drinks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DrinkDTO>> GetDrink(Guid id)
        {
            var drink = await _bll.Drinks.FirstOrDefaultAsync(id);

            if (drink == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(drink));
        }

        // PUT: api/Drinks/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDrink(Guid id, DrinkDTO drinkDTO)
        {
            if (id != drinkDTO.Id)
            {
                return BadRequest();
            }

            await _bll.Drinks.UpdateAsync(_mapper.Map(drinkDTO));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/Drinks
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<DrinkDTO>> PostDrink(DrinkDTO drinkDTO)
        {
            var bllEntity = _mapper.Map(drinkDTO);
            _bll.Drinks.Add(bllEntity);
            await _bll.SaveChangesAsync();

            drinkDTO.Id = bllEntity.Id;

            return Ok(drinkDTO);
        }

        // DELETE: api/Drinks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DrinkDTO>> DeleteDrink(Guid id)
        {
            var drink = await _bll.Drinks.FirstOrDefaultAsync(id);
            if (drink == null)
            {
                return NotFound();
            }

            await _bll.Drinks.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(drink));
        }
        
    }
}
