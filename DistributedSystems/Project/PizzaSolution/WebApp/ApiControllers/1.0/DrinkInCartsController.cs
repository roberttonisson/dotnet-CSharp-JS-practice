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
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DrinkInCartsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly DrinkInCartDTOMapper _mapper = new DrinkInCartDTOMapper();

        public DrinkInCartsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/DrinkInCarts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DrinkInCartDTO>>> GetDrinkInCarts()
        {
            var drinkInCarts = (await _bll.DrinkInCarts.GetAllAsync(User.UserGuidId()))
                .Select(bllEntity => _mapper.GetDTO(bllEntity));
            
            return Ok(drinkInCarts);
        }

     // GET: api/DrinkInCarts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DrinkInCartDTO>> GetDrinkInCart(Guid id)
        {
            var drinkInCart = await _bll.DrinkInCarts.FirstOrDefaultAsync(id, User.UserGuidId());

            if (drinkInCart == null)
            {
                return NotFound();
            }

            return Ok(_mapper.GetDTO(drinkInCart));
        }

        // PUT: api/DrinkInCarts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDrinkInCart(Guid id, DrinkInCartDTO drinkInCartDTO)
        {
            if (id != drinkInCartDTO.Id)
            {
                return BadRequest();
            }

            await _bll.DrinkInCarts.UpdateAsync(_mapper.GetBLL(drinkInCartDTO));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/DrinkInCarts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<DrinkInCartDTO>> PostDrinkInCart(DrinkInCartDTO drinkInCartDTO)
        {
            var bllEntity = _mapper.GetBLL(drinkInCartDTO);
            _bll.DrinkInCarts.Add(bllEntity);
            await _bll.SaveChangesAsync();

            drinkInCartDTO.Id = bllEntity.Id;

            return Ok(drinkInCartDTO);
        }

        // DELETE: api/DrinkInCarts/5
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

            return Ok(_mapper.GetDTO(drinkInCart));
        }
        
    }
}
