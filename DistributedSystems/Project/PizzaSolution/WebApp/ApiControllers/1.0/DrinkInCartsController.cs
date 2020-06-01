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
    /// Controller for DrinkInCarts
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DrinkInCartsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly DrinkInCartDTOMapper _mapper = new DrinkInCartDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public DrinkInCartsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/DrinkInCarts
        /// <summary>
        /// Get the list of all DrinkInCarts for specific user.
        /// </summary>
        /// <returns>List of DrinkInCarts</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DrinkInCartDTO>>> GetDrinkInCarts()
        {
            var drinkInCarts = (await _bll.DrinkInCarts.GetAllAsync(User.UserGuidId()))
                .Select(bllEntity => _mapper.Map(bllEntity));
            
            return Ok(drinkInCarts);
        }

        // GET: api/DrinkInCarts/5
        /// <summary>
        /// Get single DrinkInCart by given id
        /// </summary>
        /// <param name="id">Id of the DrinkInCart that we are returning</param>
        /// <returns>DrinkInCart</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<DrinkInCartDTO>> GetDrinkInCart(Guid id)
        {
            var drinkInCart = await _bll.DrinkInCarts.FirstOrDefaultAsync(id, User.UserGuidId());

            if (drinkInCart == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(drinkInCart));
        }

        // PUT: api/DrinkInCarts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Change existing DrinkInCart by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the DrinkInCart from DB</param>
        /// <param name="drinkInCartDTO">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDrinkInCart(Guid id, DrinkInCartDTO drinkInCartDTO)
        {
            if (id != drinkInCartDTO.Id)
            {
                return BadRequest();
            }

            await _bll.DrinkInCarts.UpdateAsync(_mapper.Map(drinkInCartDTO));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/DrinkInCarts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new DrinkInCart to the DB.
        /// </summary>
        /// <param name="drinkInCartDTO">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        public async Task<ActionResult<DrinkInCartDTO>> PostDrinkInCart(DrinkInCartDTO drinkInCartDTO)
        {
            var bllEntity = _mapper.Map(drinkInCartDTO);
            _bll.DrinkInCarts.Add(bllEntity);
            await _bll.SaveChangesAsync();

            drinkInCartDTO.Id = bllEntity.Id;

            return Ok(drinkInCartDTO);
        }

        // DELETE: api/DrinkInCarts/5
        /// <summary>
        /// Deletes a DrinkInCart record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<DrinkInCartDTO>> DeleteDrinkInCart(Guid id)
        {
            var drinkInCart = await _bll.DrinkInCarts.FirstOrDefaultAsync(id, User.UserGuidId());
            if (drinkInCart == null)
            {
                return NotFound();
            }

            await _bll.DrinkInCarts.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(drinkInCart));
        }
        
    }
}
