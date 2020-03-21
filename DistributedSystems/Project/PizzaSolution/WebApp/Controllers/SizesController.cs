using System;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;

namespace WebApp.Controllers
{
    public class SizesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISizeRepository _sizeRepository;

        public SizesController(AppDbContext context)
        {
            _context = context;
            _sizeRepository = new SizeRepository(_context);
        }

        // GET: Sizes
        public async Task<IActionResult> Index()
        {
            return View(await _sizeRepository.AllAsync());
        }

        // GET: Sizes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var size = await _sizeRepository.FindAsync(id);

            if (size == null)
            {
                return NotFound();
            }

            return View(size);
        }

        // GET: Sizes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sizes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Name,Price,SizeCm,CreatedBy,CreatedAt,CreatedBy,CreatedAt,Id")]
            Size size)
        {
            if (ModelState.IsValid)
            {
                //size.Id = Guid.NewGuid();
                _sizeRepository.Add(size);
                await _sizeRepository.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(size);
        }

        // GET: Sizes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var size = await _sizeRepository.FindAsync(id);

            if (size == null)
            {
                return NotFound();
            }

            return View(size);
        }

        // POST: Sizes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Name,Price,SizeCm,CreatedBy,CreatedAt,CreatedBy,CreatedAt,Id")]
            Size size)
        {
            if (id != size.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _sizeRepository.Update(size);
                await _sizeRepository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            return View(size);
        }

        // GET: Sizes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var size = await _sizeRepository.FindAsync(id);
            if (size == null)
            {
                return NotFound();
            }

            return View(size);
        }

        // POST: Sizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var size = _sizeRepository.Remove(id);
            await _sizeRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}