using System;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;
using WebApp.Models;

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
            var vm = new SizeCreateEditViewModel();
            return View(vm);
        }

        // POST: Sizes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SizeCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //crust.Id = Guid.NewGuid();
                _sizeRepository.Add(vm.Size);
                await _sizeRepository.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Sizes/Edit/5
        public async Task<IActionResult> Edit(Guid? id, SizeCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.Size = await _sizeRepository.FindAsync(id);

            if (vm.Size == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // POST: Sizes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            SizeCreateEditViewModel vm)
        {
            if (id != vm.Size.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _sizeRepository.Update(vm.Size);
                await _sizeRepository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
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