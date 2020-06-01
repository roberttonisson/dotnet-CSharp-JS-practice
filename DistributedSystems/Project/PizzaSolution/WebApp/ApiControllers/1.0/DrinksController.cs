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
    /// Controller for Drinks
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class DrinksController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly DrinkDTOMapper _mapper = new DrinkDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public DrinksController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/Drinks
        /// <summary>
        /// Get the list of all Drinks for specific user.
        /// </summary>
        /// <returns>List of Drinks</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DrinkDTO>>> GetDrinks()
        {
            var drinks = (await _bll.Drinks.GetAllAsync(null))
                .Select(bllEntity => _mapper.Map(bllEntity));
            
            return Ok(drinks);
        }

        // GET: api/Drinks/5
        /// <summary>
        /// Get single Drink by given id
        /// </summary>
        /// <param name="id">Id of the Drink that we are returning</param>
        /// <returns>Drink</returns>
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
        /// <summary>
        /// Change existing Drink by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the Drink from DB</param>
        /// <param name="drinkDTO">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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
        /// <summary>
        /// Add a new Drink to the DB.
        /// </summary>
        /// <param name="drinkDTO">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<DrinkDTO>> PostDrink(DrinkDTO drinkDTO)
        {
            var bllEntity = _mapper.Map(drinkDTO);
            _bll.Drinks.Add(bllEntity);
            await _bll.SaveChangesAsync();

            drinkDTO.Id = bllEntity.Id;

            return Ok(drinkDTO);
        }

        // DELETE: api/Drinks/5
        /// <summary>
        /// Deletes a Drink record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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
