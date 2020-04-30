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

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PizzaInCartsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PizzaInCartDTOMapper _mapper = new PizzaInCartDTOMapper();

        public PizzaInCartsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/PizzaInCarts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PizzaInCartDTO>>> GetPizzaInCarts()
        {
            var pizzaInCarts = (await _bll.PizzaInCarts.GetAllAsync(User.UserGuidId()))
                .Select(bllEntity => _mapper.GetDTO(bllEntity));
            
            return Ok(pizzaInCarts);
        }

     // GET: api/PizzaInCarts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PizzaInCartDTO>> GetPizzaInCart(Guid id)
        {
            var pizzaInCart = await _bll.PizzaInCarts.FirstOrDefaultAsync(id, User.UserGuidId());

            if (pizzaInCart == null)
            {
                return NotFound();
            }

            return Ok(_mapper.GetDTO(pizzaInCart));
        }

        // PUT: api/PizzaInCarts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizzaInCart(Guid id, PizzaInCartDTO pizzaInCartDTO)
        {
            if (id != pizzaInCartDTO.Id)
            {
                return BadRequest();
            }

            await _bll.PizzaInCarts.UpdateAsync(_mapper.GetBLL(pizzaInCartDTO));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/PizzaInCarts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PizzaInCartDTO>> PostPizzaInCart(PizzaInCartDTO pizzaInCartDTO)
        {
            var bllEntity = _mapper.GetBLL(pizzaInCartDTO);
            _bll.PizzaInCarts.Add(bllEntity);
            await _bll.SaveChangesAsync();

            pizzaInCartDTO.Id = bllEntity.Id;

            return Ok(pizzaInCartDTO);
        }

        // DELETE: api/PizzaInCarts/5
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

            return Ok(_mapper.GetDTO(pizzaInCart));
        }
        
    }
}
