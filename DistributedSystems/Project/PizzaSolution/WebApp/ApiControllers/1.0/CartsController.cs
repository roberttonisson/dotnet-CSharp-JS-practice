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
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CartsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly CartDTOMapper _mapper = new CartDTOMapper();

        public CartsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/Carts
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
        [HttpGet("active")]
        public async Task<ActionResult<CartDTO>> GetActiveCart(Guid id)
        {
            var cart = await _bll.Carts.GetActiveCart(User.UserGuidId());

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(cart));
        }
        

        // PUT: api/Carts/active
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
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
