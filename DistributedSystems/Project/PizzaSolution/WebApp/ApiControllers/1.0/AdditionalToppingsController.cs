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
    /// Controller for AdditionalToppings
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AdditionalToppingsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly AdditionalToppingDTOMapper _mapper = new AdditionalToppingDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public AdditionalToppingsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/AdditionalToppings
        /// <summary>
        /// Get the list of all AdditionalToppings for specific user.
        /// </summary>
        /// <returns>List of AdditionalToppings</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdditionalToppingDTO>>> GetAdditionalToppings()
        {
            var additionalToppings = (await _bll.AdditionalToppings.GetAllAsync(User.UserGuidId()))
                .Select(bllEntity => _mapper.Map(bllEntity));
            
            return Ok(additionalToppings);
        }

        // GET: api/AdditionalToppings/5
        /// <summary>
        /// Get single AdditionalTopping by given id
        /// </summary>
        /// <param name="id">Id of the AdditionalTopping that we are returning</param>
        /// <returns>AdditionalTopping</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<AdditionalToppingDTO>> GetAdditionalTopping(Guid id)
        {
            var additionalTopping = await _bll.AdditionalToppings.FirstOrDefaultAsync(id, User.UserGuidId());

            if (additionalTopping == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(additionalTopping));
        }

        // PUT: api/AdditionalToppings/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Change existing AdditionalTopping by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the AdditionalTopping from DB</param>
        /// <param name="additionalToppingDTO">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdditionalTopping(Guid id, AdditionalToppingDTO additionalToppingDTO)
        {
            if (id != additionalToppingDTO.Id)
            {
                return BadRequest();
            }

            await _bll.AdditionalToppings.UpdateAsync(_mapper.Map(additionalToppingDTO));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/AdditionalToppings
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new AdditionalTopping to the DB.
        /// </summary>
        /// <param name="additionalToppingDTO">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        public async Task<ActionResult<AdditionalToppingDTO>> PostAdditionalTopping(AdditionalToppingDTO additionalToppingDTO)
        {
            var bllEntity = _mapper.Map(additionalToppingDTO);
            _bll.AdditionalToppings.Add(bllEntity);
            await _bll.SaveChangesAsync();

            additionalToppingDTO.Id = bllEntity.Id;

            return Ok(additionalToppingDTO);
        }

        // DELETE: api/AdditionalToppings/5
        /// <summary>
        /// Deletes a AdditionalTopping record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
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

            return Ok(_mapper.Map(additionalTopping));
        }
        
    }
}
