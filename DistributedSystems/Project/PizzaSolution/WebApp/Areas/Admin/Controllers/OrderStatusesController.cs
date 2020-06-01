#pragma warning disable 1591
using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderStatus = BLL.App.DTO.OrderStatus;

namespace WebApp.Areas.Admin.Controllers

{
    [Authorize(Roles = "Admin")][Area("Admin")]
    public class OrderStatusesController : Controller
    {
        private readonly IAppBLL _bll;

        public OrderStatusesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: OrderStatuss
        public async Task<IActionResult> Index()
        {
            return View(await _bll.OrderStatuses.GetAllAsync(null));
        }

        // GET: OrderStatuss/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatus = await _bll.OrderStatuses.FirstOrDefaultAsync(id.Value);

            if (orderStatus == null)
            {
                return NotFound();
            }

            return View(orderStatus);
        }

        // GET: OrderStatuss/Create
        public IActionResult Create()
        {
            var orderStatus = new OrderStatus();
            return View(orderStatus);
        }

        // POST: OrderStatuss/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.OrderStatus orderStatus)
        {
            if (ModelState.IsValid)
            {
                _bll.OrderStatuses.Add(orderStatus);
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(orderStatus);
        }

        // GET: OrderStatuss/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var orderStatus = await _bll.OrderStatuses.FirstOrDefaultAsync(id.Value);
            
            if (orderStatus == null)
            {
                return NotFound();
            }

            return View(orderStatus);
        }

        // POST: OrderStatuss/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, OrderStatus orderStatus)
        {
            if (id != orderStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.OrderStatuses.UpdateAsync(orderStatus);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(orderStatus);
        }

        // GET: OrderStatuss/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatus = await _bll.OrderStatuses.FirstOrDefaultAsync(id.Value);
            if (orderStatus == null)
            {
                return NotFound();
            }

            return View(orderStatus);
        }

        // POST: OrderStatuss/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.OrderStatuses.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}