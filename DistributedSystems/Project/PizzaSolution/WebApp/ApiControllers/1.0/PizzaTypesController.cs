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
    /// Controller for PizzaTypes
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class PizzaTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PizzaTypeDTOMapper _mapper = new PizzaTypeDTOMapper();
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public PizzaTypesController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/PizzaTypes
        /// <summary>
        /// Get the list of all PizzaTypes for specific user.
        /// </summary>
        /// <returns>List of PizzaTypes</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PizzaTypeDTO>>> GetPizzaTypes()
        {
            var pizzaTypes = (await _bll.PizzaTypes.GetAllAsync(null))
                .Select(bllEntity => _mapper.Map(bllEntity));
            
            return Ok(pizzaTypes);
        }

        // GET: api/PizzaTypes/5
        /// <summary>
        /// Get single PizzaType by given id
        /// </summary>
        /// <param name="id">Id of the PizzaType that we are returning</param>
        /// <returns>PizzaType</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PizzaTypeDTO>> GetPizzaType(Guid id)
        {
            var pizzaType = await _bll.PizzaTypes.FirstOrDefaultAsync(id);

            if (pizzaType == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(pizzaType));
        }

        // PUT: api/PizzaTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Change existing PizzaType by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the PizzaType from DB</param>
        /// <param name="pizzaTypeDTO">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> PutPizzaType(Guid id, PizzaTypeDTO pizzaTypeDTO)
        {
            if (id != pizzaTypeDTO.Id)
            {
                return BadRequest();
            }

            await _bll.PizzaTypes.UpdateAsync(_mapper.Map(pizzaTypeDTO));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/PizzaTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new PizzaType to the DB.
        /// </summary>
        /// <param name="pizzaTypeDTO">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<PizzaTypeDTO>> PostPizzaType(PizzaTypeDTO pizzaTypeDTO)
        {
            var bllEntity = _mapper.Map(pizzaTypeDTO);
            _bll.PizzaTypes.Add(bllEntity);
            await _bll.SaveChangesAsync();

            pizzaTypeDTO.Id = bllEntity.Id;

            return Ok(pizzaTypeDTO);
        }

        // DELETE: api/PizzaTypes/5
        /// <summary>
        /// Deletes a PizzaType record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<PizzaTypeDTO>> DeletePizzaType(Guid id)
        {
            var pizzaType = await _bll.PizzaTypes.FirstOrDefaultAsync(id);
            if (pizzaType == null)
            {
                return NotFound();
            }

            await _bll.PizzaTypes.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(pizzaType));
        }
        
    }
}
