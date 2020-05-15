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
    public class SizesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly SizeDTOMapper _mapper = new SizeDTOMapper();

        public SizesController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/Sizes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SizeDTO>>> GetSizes()
        {
            var sizes = (await _bll.Sizes.GetAllAsync(null))
                .Select(bllEntity => _mapper.Map(bllEntity));
            
            return Ok(sizes);
        }

     // GET: api/Sizes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SizeDTO>> GetSize(Guid id)
        {
            var size = await _bll.Sizes.FirstOrDefaultAsync(id);

            if (size == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(size));
        }

        // PUT: api/Sizes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSize(Guid id, SizeDTO sizeDTO)
        {
            if (id != sizeDTO.Id)
            {
                return BadRequest();
            }

            await _bll.Sizes.UpdateAsync(_mapper.Map(sizeDTO));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/Sizes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<SizeDTO>> PostSize(SizeDTO sizeDTO)
        {
            var bllEntity = _mapper.Map(sizeDTO);
            _bll.Sizes.Add(bllEntity);
            await _bll.SaveChangesAsync();

            sizeDTO.Id = bllEntity.Id;

            return Ok(sizeDTO);
        }

        // DELETE: api/Sizes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SizeDTO>> DeleteSize(Guid id)
        {
            var size = await _bll.Sizes.FirstOrDefaultAsync(id);
            if (size == null)
            {
                return NotFound();
            }

            await _bll.Sizes.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(size));
        }
        
    }
}
