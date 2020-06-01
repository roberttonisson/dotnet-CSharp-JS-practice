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
    /// Controller for Invoices
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InvoicesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly InvoiceDTOMapper _mapper = new InvoiceDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public InvoicesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Invoices
        /// <summary>
        /// Get the list of all Invoices for specific user.
        /// </summary>
        /// <returns>List of Invoices</returns>
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
        /// <summary>
        /// Get single Invoice by given id
        /// </summary>
        /// <param name="id">Id of the Invoice that we are returning</param>
        /// <returns>Invoice</returns>
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

        /// <summary>
        /// Adds all the items from the invoice to the currently active Cart for the User.
        /// </summary>
        /// <param name="invoiceDTO">Invoice with the items that will be inserted into the Cart.</param>
        /// <returns></returns>
        [HttpPost("reOrder")]
        public async Task<ActionResult<CartDTO>> ReOrder(InvoiceDTO invoiceDTO)
        {
            return Ok(await _bll.Invoices.ReOrder(_bll, invoiceDTO.Id, User.UserGuidId()));
        }

        // PUT: api/Invoices/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Change existing Invoice by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the Invoice from DB</param>
        /// <param name="invoiceDTO">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
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
        /// <summary>
        /// Add a new Invoice to the DB.
        /// </summary>
        /// <param name="invoiceDTO">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
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
        /// <summary>
        /// Deletes a Invoice record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
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