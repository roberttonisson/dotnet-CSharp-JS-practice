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
using Newtonsoft.Json;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Controller for Carts
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CartsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly CartDTOMapper _mapper = new CartDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public CartsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/Carts
        /// <summary>
        /// Get the list of all Carts for specific user.
        /// </summary>
        /// <returns>List of Carts</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<CartDTO>>> GetCarts()
        {
            var carts = (await _bll.Carts.GetAllWithCollectionsAsync(User.UserGuidId()))
                .Select(bllEntity => _mapper.Map(bllEntity));

            return Ok(carts);
        }

        // GET: api/Carts/5
        /// <summary>
        /// Get single Cart by given id
        /// </summary>
        /// <param name="id">Id of the Cart that we are returning</param>
        /// <returns>Cart</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CartDTO>> GetCart(Guid id)
        {
            var cart = await _bll.Carts.FirstOrDefaultAsync(id, User.UserGuidId());

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(cart));
        }
        /// <summary>
        /// Get the currently active cart for the User.
        /// </summary>
        /// <returns>The received cart from DB.</returns>
        [HttpGet("active")]
        public async Task<ActionResult<CartDTO>> GetActiveCart()
        {
            var cart = await _bll.Carts.GetActiveCart(User.UserGuidId());

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(cart));
        }
        

        // PUT: api/Carts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Change existing Cart by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the Cart from DB</param>
        /// <param name="cartDTO">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(Guid id, CartDTO cartDTO)
        {
            if (id != cartDTO.Id)
            {
                return BadRequest();
            }

            await _bll.Carts.UpdateAsync(_mapper.Map(cartDTO));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/Carts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new Cart to the DB.
        /// </summary>
        /// <param name="cartDTO">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        public async Task<ActionResult<CartDTO>> PostCart(CartDTO cartDTO)
        {
            var bllEntity = _mapper.Map(cartDTO);
            _bll.Carts.Add(bllEntity);
            await _bll.SaveChangesAsync();

            cartDTO.Id = bllEntity.Id;

            return Ok(cartDTO);
        }

        // DELETE: api/Carts/5
        /// <summary>
        /// Deletes a Cart record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<CartDTO>> DeleteCart(Guid id)
        {
            var cart = await _bll.Carts.FirstOrDefaultAsync(id, User.UserGuidId());
            if (cart == null)
            {
                return NotFound();
            }

            await _bll.Carts.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(cart));
        }
        
    }
}
