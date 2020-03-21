using System;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CrustsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICrustRepository _crustRepository;

        public CrustsController(AppDbContext context)
        {
            _context = context;
            _crustRepository = new CrustRepository(_context);
        }

        // GET: Crusts
        public async Task<IActionResult> Index()
        {
            return View(await _crustRepository.AllAsync());
        }

        // GET: Crusts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crust = await _crustRepository.FindAsync(id);

            if (crust == null)
            {
                return NotFound();
            }

            return View(crust);
        }

        // GET: Crusts/Create
        public IActionResult Create()
        {
            var vm = new CrustCreateEditViewModel();
            return View(vm);
        }

        // POST: Crusts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CrustCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //crust.Id = Guid.NewGuid();
                _crustRepository.Add(vm.Crust);
                await _crustRepository.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Crusts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crust = await _crustRepository.FindAsync(id);

            if (crust == null)
            {
                return NotFound();
            }

            return View(crust);
        }

        // POST: Crusts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Name,Price,CreatedBy,CreatedAt,CreatedBy,CreatedAt,Id")]
            Crust crust)
        {
            if (id != crust.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _crustRepository.Update(crust);
                await _crustRepository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            return View(crust);
        }

        // GET: Crusts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crust = await _crustRepository.FindAsync(id);
            if (crust == null)
            {
                return NotFound();
            }

            return View(crust);
        }

        // POST: Crusts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var crust = _crustRepository.Remove(id);
            await _crustRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}