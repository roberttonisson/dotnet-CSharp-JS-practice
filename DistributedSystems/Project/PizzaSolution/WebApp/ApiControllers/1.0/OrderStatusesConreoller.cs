using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0

{
    /// <summary>
    /// Controller for OrderStatuses
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class OrderStatusesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly OrderStatusDTOMapper _mapper = new OrderStatusDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public OrderStatusesController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/OrderStatuses
        /// <summary>
        /// Get the list of all OrderStatuses for specific user.
        /// </summary>
        /// <returns>List of OrderStatuses</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderStatusDTO>>> GetOrderStatuses()
        {
            var orderStatuses = (await _bll.OrderStatuses.GetAllAsync(null))
                .Select(bllEntity => _mapper.Map(bllEntity));
            
            return Ok(orderStatuses);
        }

        // GET: api/OrderStatuses/5
        /// <summary>
        /// Get single OrderStatus by given id
        /// </summary>
        /// <param name="id">Id of the OrderStatus that we are returning</param>
        /// <returns>OrderStatus</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderStatusDTO>> GetOrderStatus(Guid id)
        {
            var orderStatus = await _bll.OrderStatuses.FirstOrDefaultAsync(id);

            if (orderStatus == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(orderStatus));
        }

        // PUT: api/OrderStatuses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Change existing OrderStatus by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the OrderStatus from DB</param>
        /// <param name="orderStatusDTO">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderStatus(Guid id, OrderStatusDTO orderStatusDTO)
        {
            if (id != orderStatusDTO.Id)
            {
                return BadRequest();
            }

            await _bll.OrderStatuses.UpdateAsync(_mapper.Map(orderStatusDTO));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/OrderStatuses
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new OrderStatus to the DB.
        /// </summary>
        /// <param name="orderStatusDTO">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        public async Task<ActionResult<OrderStatusDTO>> PostOrderStatus(OrderStatusDTO orderStatusDTO)
        {
            var bllEntity = _mapper.Map(orderStatusDTO);
            _bll.OrderStatuses.Add(bllEntity);
            await _bll.SaveChangesAsync();

            orderStatusDTO.Id = bllEntity.Id;

            return Ok(orderStatusDTO);
        }

        // DELETE: api/OrderStatuses/5
        /// <summary>
        /// Deletes a OrderStatus record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderStatusDTO>> DeleteOrderStatus(Guid id)
        {
            var orderStatus = await _bll.OrderStatuses.FirstOrDefaultAsync(id);
            if (orderStatus == null)
            {
                return NotFound();
            }

            await _bll.OrderStatuses.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(orderStatus));
        }
        
    }
}
