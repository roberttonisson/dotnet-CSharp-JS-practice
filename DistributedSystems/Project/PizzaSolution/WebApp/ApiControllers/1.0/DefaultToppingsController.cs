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
    /// Controller for DefaultToppings
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class DefaultToppingsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly DefaultToppingDTOMapper _mapper = new DefaultToppingDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public DefaultToppingsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/DefaultToppings
        /// <summary>
        /// Get the list of all DefaultToppings for specific user.
        /// </summary>
        /// <returns>List of DefaultToppings</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DefaultToppingDTO>>> GetDefaultToppings()
        {
            var defaultToppings = (await _bll.DefaultToppings.GetAllAsync(null))
                .Select(bllEntity => _mapper.Map(bllEntity));
            
            return Ok(defaultToppings);
        }

        // GET: api/DefaultToppings/5
        /// <summary>
        /// Get single DefaultTopping by given id
        /// </summary>
        /// <param name="id">Id of the DefaultTopping that we are returning</param>
        /// <returns>DefaultTopping</returns>
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
        /// <summary>
        /// Change existing DefaultTopping by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the DefaultTopping from DB</param>
        /// <param name="defaultToppingDTO">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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
        /// <summary>
        /// Add a new DefaultTopping to the DB.
        /// </summary>
        /// <param name="defaultToppingDTO">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<DefaultToppingDTO>> PostDefaultTopping(DefaultToppingDTO defaultToppingDTO)
        {
            var bllEntity = _mapper.Map(defaultToppingDTO);
            _bll.DefaultToppings.Add(bllEntity);
            await _bll.SaveChangesAsync();

            defaultToppingDTO.Id = bllEntity.Id;

            return Ok(defaultToppingDTO);
        }

        // DELETE: api/DefaultToppings/5
        /// <summary>
        /// Deletes a DefaultTopping record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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
