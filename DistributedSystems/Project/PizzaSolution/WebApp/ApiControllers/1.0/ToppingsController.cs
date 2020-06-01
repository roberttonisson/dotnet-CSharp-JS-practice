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
    /// Controller for Toppings
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ToppingsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ToppingDTOMapper _mapper = new ToppingDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public ToppingsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/Toppings
        /// <summary>
        /// Get the list of all Toppings for specific user.
        /// </summary>
        /// <returns>List of Toppings</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToppingDTO>>> GetToppings()
        {
            var toppings = (await _bll.Toppings.GetAllAsync(null))
                .Select(bllEntity => _mapper.Map(bllEntity));
            
            return Ok(toppings);
        }

        // GET: api/Toppings/5
        /// <summary>
        /// Get single Topping by given id
        /// </summary>
        /// <param name="id">Id of the Topping that we are returning</param>
        /// <returns>Topping</returns>
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
        /// <summary>
        /// Change existing Topping by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the Topping from DB</param>
        /// <param name="toppingDTO">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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
        /// <summary>
        /// Add a new Topping to the DB.
        /// </summary>
        /// <param name="toppingDTO">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<ToppingDTO>> PostTopping(ToppingDTO toppingDTO)
        {
            var bllEntity = _mapper.Map(toppingDTO);
            _bll.Toppings.Add(bllEntity);
            await _bll.SaveChangesAsync();

            toppingDTO.Id = bllEntity.Id;

            return Ok(toppingDTO);
        }

        // DELETE: api/Toppings/5
        /// <summary>
        /// Deletes a Topping record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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
