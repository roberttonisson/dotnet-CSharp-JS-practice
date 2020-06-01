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
using PizzaInCart = DAL.App.DTO.PizzaInCart;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Controller for PizzaInCarts
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PizzaInCartsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PizzaInCartDTOMapper _mapper = new PizzaInCartDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public PizzaInCartsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/PizzaInCarts
        /// <summary>
        /// Get the list of all PizzaInCarts for specific user.
        /// </summary>
        /// <returns>List of PizzaInCarts</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PizzaInCartDTO>>> GetPizzaInCarts()
        {
            var pizzaInCarts = (await _bll.PizzaInCarts.GetAllAsync(User.UserGuidId()))
                .Select(bllEntity => _mapper.Map(bllEntity));
            
            return Ok(pizzaInCarts);
        }

        // GET: api/PizzaInCarts/5
        /// <summary>
        /// Get single PizzaInCart by given id
        /// </summary>
        /// <param name="id">Id of the PizzaInCart that we are returning</param>
        /// <returns>PizzaInCart</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PizzaInCartDTO>> GetPizzaInCart(Guid id)
        {
            var pizzaInCart = await _bll.PizzaInCarts.FirstOrDefaultAsync(id, User.UserGuidId());

            if (pizzaInCart == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(pizzaInCart));
        }

        // PUT: api/PizzaInCarts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Change existing PizzaInCart by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the PizzaInCart from DB</param>
        /// <param name="pizzaInCartDTO">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizzaInCart(Guid id, PizzaInCartDTO pizzaInCartDTO)
        {
            if (id != pizzaInCartDTO.Id)
            {
                return BadRequest();
            }

            await _bll.PizzaInCarts.UpdateAsync(_mapper.Map(pizzaInCartDTO));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/PizzaInCarts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new PizzaInCart to the DB.
        /// </summary>
        /// <param name="pizzaInCartDTO">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        public async Task<ActionResult<PizzaInCartDTO>> PostPizzaInCart(PizzaInCartDTO pizzaInCartDTO)
        {
            var bllEntity = _mapper.Map(pizzaInCartDTO);
            _bll.PizzaInCarts.Add(bllEntity);
            await _bll.SaveChangesAsync();

            pizzaInCartDTO.Id = bllEntity.Id;

            return Ok(pizzaInCartDTO);
        }

        // DELETE: api/PizzaInCarts/5
        /// <summary>
        /// Deletes a PizzaInCart record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<PizzaInCartDTO>> DeletePizzaInCart(Guid id)
        {
            var pizzaInCart = await _bll.PizzaInCarts.FirstOrDefaultAsync(id, User.UserGuidId());
            if (pizzaInCart == null)
            {
                return NotFound();
            }

            await _bll.PizzaInCarts.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(pizzaInCart));
        }
        
        // DELETE: api/PizzaInCarts/5
        /// <summary>
        /// Delete PizzaInCart with AdditionalToppings.
        /// </summary>
        /// <param name="id">Id for PizzaInCart.</param>
        /// <param name="pizzaInCartDTO">DTO with all the AdditionalToppings.</param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<PizzaInCartDTO>> DeleteCascadePizzaInCart(Guid id, PizzaInCartDTO pizzaInCartDTO)
        {
            var pizzaInCart = await _bll.PizzaInCarts.FirstOrDefaultAsync(id, User.UserGuidId());
            if (pizzaInCart == null)
            {
                return NotFound();
            }

            foreach (var additional in pizzaInCartDTO.AdditionalToppings!)
            {
                await _bll.AdditionalToppings.RemoveAsync(additional.Id);
            }
            await _bll.SaveChangesAsync();
            await _bll.PizzaInCarts.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(pizzaInCart));
        }
        
    }
}
