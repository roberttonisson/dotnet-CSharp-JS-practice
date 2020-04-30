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
    public class CrustsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly CrustDTOMapper _mapper = new CrustDTOMapper();

        public CrustsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/Crusts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CrustDTO>>> GetCrusts()
        {
            var crusts = (await _bll.Crusts.GetAllAsync(null))
                .Select(bllEntity => _mapper.Map(bllEntity));
            
            return Ok(crusts);
        }

     // GET: api/Crusts/5
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
        [HttpPut("{id}")]
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
        [HttpPost]
        public async Task<ActionResult<CrustDTO>> PostCrust(CrustDTO crustDTO)
        {
            var bllEntity = _mapper.Map(crustDTO);
            _bll.Crusts.Add(bllEntity);
            await _bll.SaveChangesAsync();

            crustDTO.Id = bllEntity.Id;

            return Ok(crustDTO);
        }

        // DELETE: api/Crusts/5
        [HttpDelete("{id}")]
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
