#pragma warning disable 1591
using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Transport = BLL.App.DTO.Transport;

namespace WebApp.Areas.Admin.Controllers

{
    [Authorize(Roles = "Admin")][Area("Admin")]
    public class TransportsController : Controller
    {
        private readonly IAppBLL _bll;

        public TransportsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Transports
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Transports.GetAllAsync(null));
        }

        // GET: Transports/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transport = await _bll.Transports.FirstOrDefaultAsync(id.Value);

            if (transport == null)
            {
                return NotFound();
            }

            return View(transport);
        }

        // GET: Transports/Create
        public IActionResult Create()
        {
            var transport = new Transport();
            return View(transport);
        }

        // POST: Transports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.Transport transport)
        {
            if (ModelState.IsValid)
            {
                _bll.Transports.Add(transport);
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(transport);
        }

        // GET: Transports/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var transport = await _bll.Transports.FirstOrDefaultAsync(id.Value);
            
            if (transport == null)
            {
                return NotFound();
            }

            return View(transport);
        }

        // POST: Transports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Transport transport)
        {
            if (id != transport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.Transports.UpdateAsync(transport);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(transport);
        }

        // GET: Transports/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transport = await _bll.Transports.FirstOrDefaultAsync(id.Value);
            if (transport == null)
            {
                return NotFound();
            }

            return View(transport);
        }

        // POST: Transports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Transports.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}