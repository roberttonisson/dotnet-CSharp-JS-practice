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
    public class InvoiceLinesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly InvoiceLineDTOMapper _mapper = new InvoiceLineDTOMapper();

        public InvoiceLinesController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/InvoiceLines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceLineDTO>>> GetInvoiceLines()
        {
            var invoiceLines = (await _bll.InvoiceLines.GetAllAsync(User.UserGuidId()))
                .Select(bllEntity => _mapper.GetDTO(bllEntity));
            
            return Ok(invoiceLines);
        }

     // GET: api/InvoiceLines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceLineDTO>> GetInvoiceLine(Guid id)
        {
            var invoiceLine = await _bll.InvoiceLines.FirstOrDefaultAsync(id, User.UserGuidId());

            if (invoiceLine == null)
            {
                return NotFound();
            }

            return Ok(_mapper.GetDTO(invoiceLine));
        }

        // PUT: api/InvoiceLines/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoiceLine(Guid id, InvoiceLineDTO invoiceLineDTO)
        {
            if (id != invoiceLineDTO.Id)
            {
                return BadRequest();
            }

            await _bll.InvoiceLines.UpdateAsync(_mapper.GetBLL(invoiceLineDTO));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/InvoiceLines
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<InvoiceLineDTO>> PostInvoiceLine(InvoiceLineDTO invoiceLineDTO)
        {
            var bllEntity = _mapper.GetBLL(invoiceLineDTO);
            _bll.InvoiceLines.Add(bllEntity);
            await _bll.SaveChangesAsync();

            invoiceLineDTO.Id = bllEntity.Id;

            return Ok(invoiceLineDTO);
        }

        // DELETE: api/InvoiceLines/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<InvoiceLineDTO>> DeleteInvoiceLine(Guid id)
        {
            var invoiceLine = await _bll.InvoiceLines.FirstOrDefaultAsync(id, User.UserGuidId());
            if (invoiceLine == null)
            {
                return NotFound();
            }

            await _bll.InvoiceLines.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.GetDTO(invoiceLine));
        }
        
    }
}
