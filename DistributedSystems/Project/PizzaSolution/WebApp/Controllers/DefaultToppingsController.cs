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
    public class DefaultToppingsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IDefaultToppingRepository _defaultToppingRepository;

        public DefaultToppingsController(AppDbContext context)
        {
            _context = context;
            _defaultToppingRepository = new DefaultToppingRepository(_context);
        }

        // GET: DefaultToppings
        public async Task<IActionResult> Index()
        {
            return View(await _defaultToppingRepository.AllAsync());
        }

        // GET: DefaultToppings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defaultTopping = await _defaultToppingRepository.FindAsync(id);

            if (defaultTopping == null)
            {
                return NotFound();
            }

            return View(defaultTopping);
        }

        // GET: DefaultToppings/Create
           // GET: DefaultToppings/Create
        public IActionResult Create()
        {
            var vm =  new DefaultToppingCreateEditViewModel();
            vm.ToppingSelectList = new SelectList(_context.Toppings, nameof(Topping.Id), nameof(Topping.Name));
            vm.PizzaTypeSelectList = new SelectList(_context.PizzaTypes, nameof(PizzaType.Id), nameof(PizzaType.Name));
            return View(vm);
        }

        // POST: DefaultToppings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DefaultToppingCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //defaultTopping.Id = Guid.NewGuid();
                _defaultToppingRepository.Add(vm.DefaultTopping);
                await _defaultToppingRepository.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            vm.ToppingSelectList = new SelectList(_context.Toppings, nameof(Topping.Id), nameof(Topping.Name));
            vm.PizzaTypeSelectList = new SelectList(_context.PizzaTypes, nameof(PizzaType.Id), nameof(PizzaType.Name));
            return View(vm);
        }

        // GET: DefaultToppings/Edit/5
        public async Task<IActionResult> Edit(Guid? id, DefaultToppingCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.DefaultTopping = await _defaultToppingRepository.FindAsync(id);
            if (vm.DefaultTopping == null)
            {
                return NotFound();
            }
            vm.ToppingSelectList = new SelectList(_context.Toppings, nameof(Topping.Id), nameof(Topping.Name));
            vm.PizzaTypeSelectList = new SelectList(_context.PizzaTypes, nameof(PizzaType.Id), nameof(PizzaType.Name));

            return View(vm);
        }

        // POST: DefaultToppings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, DefaultToppingCreateEditViewModel vm)
        {
            if (id != vm.DefaultTopping.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _defaultToppingRepository.Update(vm.DefaultTopping);
                await _defaultToppingRepository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            vm.ToppingSelectList = new SelectList(_context.Toppings, nameof(Topping.Id), nameof(Topping.Name));
            vm.PizzaTypeSelectList = new SelectList(_context.PizzaTypes, nameof(PizzaType.Id), nameof(PizzaType.Name));
            return View(vm);
        }

        // GET: DefaultToppings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defaultTopping = await _defaultToppingRepository.FindAsync(id);
            if (defaultTopping == null)
            {
                return NotFound();
            }

            return View(defaultTopping);
        }

        // POST: DefaultToppings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var defaultTopping = _defaultToppingRepository.Remove(id);
            await _defaultToppingRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}