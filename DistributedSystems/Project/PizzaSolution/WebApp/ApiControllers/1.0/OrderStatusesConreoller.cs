using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class OrderStatusesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly OrderStatusDTOMapper _mapper = new OrderStatusDTOMapper();

        public OrderStatusesController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/OrderStatuss
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderStatusDTO>>> GetOrderStatuss()
        {
            var orderStatuss = (await _bll.OrderStatuses.GetAllAsync(null))
                .Select(bllEntity => _mapper.Map(bllEntity));
            
            return Ok(orderStatuss);
        }

     // GET: api/OrderStatuss/5
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

        // PUT: api/OrderStatuss/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
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

        // POST: api/OrderStatuss
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<OrderStatusDTO>> PostOrderStatus(OrderStatusDTO orderStatusDTO)
        {
            var bllEntity = _mapper.Map(orderStatusDTO);
            _bll.OrderStatuses.Add(bllEntity);
            await _bll.SaveChangesAsync();

            orderStatusDTO.Id = bllEntity.Id;

            return Ok(orderStatusDTO);
        }

        // DELETE: api/OrderStatuss/5
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
