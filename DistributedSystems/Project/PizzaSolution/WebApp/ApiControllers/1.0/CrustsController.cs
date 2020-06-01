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
    /// Controller for Crusts
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CrustsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly CrustDTOMapper _mapper = new CrustDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public CrustsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/Crusts
        /// <summary>
        /// Get the list of all Crusts for specific user.
        /// </summary>
        /// <returns>List of Crusts</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CrustDTO>>> GetCrusts()
        {
            var crusts = (await _bll.Crusts.GetAllAsync(null))
                .Select(bllEntity => _mapper.Map(bllEntity));
            
            return Ok(crusts);
        }

        // GET: api/Crusts/5
        /// <summary>
        /// Get single Crust by given id
        /// </summary>
        /// <param name="id">Id of the Crust that we are returning</param>
        /// <returns>Crust</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CrustDTO>> GetCrust(Guid id)
        {
            var crust = await _bll.Crusts.FirstOrDefaultAsync(id);

            if (crust == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(crust));
        }

        // PUT: api/Crusts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Change existing Crust by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the Crust from DB</param>
        /// <param name="crustDTO">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> PutCrust(Guid id, CrustDTO crustDTO)
        {
            if (id != crustDTO.Id)
            {
                return BadRequest();
            }

            await _bll.Crusts.UpdateAsync(_mapper.Map(crustDTO));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/Crusts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new Crust to the DB.
        /// </summary>
        /// <param name="crustDTO">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<CrustDTO>> PostCrust(CrustDTO crustDTO)
        {
            var bllEntity = _mapper.Map(crustDTO);
            _bll.Crusts.Add(bllEntity);
            await _bll.SaveChangesAsync();

            crustDTO.Id = bllEntity.Id;

            return Ok(crustDTO);
        }

        // DELETE: api/Crusts/5
        /// <summary>
        /// Deletes a Crust record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<CrustDTO>> DeleteCrust(Guid id)
        {
            var crust = await _bll.Crusts.FirstOrDefaultAsync(id);
            if (crust == null)
            {
                return NotFound();
            }

            await _bll.Crusts.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(crust));
        }
        
    }
}
