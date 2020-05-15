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
    public class InvoicesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly InvoiceDTOMapper _mapper = new InvoiceDTOMapper();

        public InvoicesController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/Invoices
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<ActionResult<IEnumerable<InvoiceDTO>>> GetInvoices()
        {
            var invoices = (await _bll.Invoices.GetAllWithCollectionsAsync(User.UserGuidId()))
                .Select(bllEntity => _mapper.Map(bllEntity));

            return Ok(invoices);
        }

     // GET: api/Invoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceDTO>> GetInvoice(Guid id)
        {
            var invoice = await _bll.Invoices.FirstOrDefaultAsync(id, User.UserGuidId());

            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(invoice));
        }

        // PUT: api/Invoices/active
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(Guid id, InvoiceDTO invoiceDTO)
        {
            if (id != invoiceDTO.Id)
            {
                return BadRequest();
            }

            await _bll.Invoices.UpdateAsync(_mapper.Map(invoiceDTO));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/Invoices
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<InvoiceDTO>> PostInvoice(InvoiceDTO invoiceDTO)
        {
            var bllEntity = _mapper.Map(invoiceDTO);
            _bll.Invoices.Add(bllEntity);
            await _bll.SaveChangesAsync();

            invoiceDTO.Id = bllEntity.Id;

            return Ok(invoiceDTO);
        }

        // DELETE: api/Invoices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<InvoiceDTO>> DeleteInvoice(Guid id)
        {
            var invoice = await _bll.Invoices.FirstOrDefaultAsync(id, User.UserGuidId());
            if (invoice == null)
            {
                return NotFound();
            }

            await _bll.Invoices.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(invoice));
        }
        
    }
}
