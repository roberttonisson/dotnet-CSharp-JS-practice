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
    /// Controller for NewProducts
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class NewProductsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly NewProductDTOMapper _mapper = new NewProductDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public NewProductsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/NewProducts
        /// <summary>
        /// Get the list of all NewProducts for specific user.
        /// </summary>
        /// <returns>List of NewProducts</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewProductDTO>>> GetNewProducts()
        {
            var newProducts = (await _bll.NewProducts.GetAllAsync(null))
                .Select(bllEntity => _mapper.Map(bllEntity));
            
            return Ok(newProducts);
        }

        // GET: api/NewProducts/5
        /// <summary>
        /// Get single NewProduct by given id
        /// </summary>
        /// <param name="id">Id of the NewProduct that we are returning</param>
        /// <returns>NewProduct</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<NewProductDTO>> GetNewProduct(Guid id)
        {
            var newProduct = await _bll.NewProducts.FirstOrDefaultAsync(id, User.UserGuidId());

            if (newProduct == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(newProduct));
        }

        // PUT: api/NewProducts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Change existing NewProduct by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the NewProduct from DB</param>
        /// <param name="newProductDTO">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> PutNewProduct(Guid id, NewProductDTO newProductDTO)
        {
            if (id != newProductDTO.Id)
            {
                return BadRequest();
            }

            await _bll.NewProducts.UpdateAsync(_mapper.Map(newProductDTO));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/NewProducts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new NewProduct to the DB.
        /// </summary>
        /// <param name="newProductDTO">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<NewProductDTO>> PostNewProduct(NewProductDTO newProductDTO)
        {
            var bllEntity = _mapper.Map(newProductDTO);
            _bll.NewProducts.Add(bllEntity);
            await _bll.SaveChangesAsync();

            newProductDTO.Id = bllEntity.Id;

            return Ok(newProductDTO);
        }

        // DELETE: api/NewProducts/5
        /// <summary>
        /// Deletes a NewProduct record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<NewProductDTO>> DeleteNewProduct(Guid id)
        {
            var newProduct = await _bll.NewProducts.FirstOrDefaultAsync(id, User.UserGuidId());
            if (newProduct == null)
            {
                return NotFound();
            }

            await _bll.NewProducts.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(newProduct));
        }
        
    }
}
