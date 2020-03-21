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
    public class PizzaInCartsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPizzaInCartRepository _pizzaInCartRepository;

        public PizzaInCartsController(AppDbContext context)
        {
            _context = context;
            _pizzaInCartRepository = new PizzaInCartRepository(_context);
        }

        // GET: PizzaInCarts
        public async Task<IActionResult> Index()
        {
            return View(await _pizzaInCartRepository.AllAsync());
        }

        // GET: PizzaInCarts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaInCart = await _pizzaInCartRepository.FindAsync(id);

            if (pizzaInCart == null)
            {
                return NotFound();
            }

            return View(pizzaInCart);
        }

               // GET: PizzaInCarts/Create
        public IActionResult Create()
        {
            var vm =  new PizzaInCartCreateEditViewModel();
            vm.PizzaTypeSelectList = new SelectList(_context.PizzaTypes, nameof(PizzaType.Id), nameof(PizzaType.Name));
            vm.CrustSelectList = new SelectList(_context.Crusts, nameof(Crust.Id), nameof(Crust.Name));
            vm.SizeSelectList = new SelectList(_context.Sizes, nameof(Size.Id), nameof(Size.Name));
            vm.CartSelectList = new SelectList(_context.Carts, nameof(Cart.Id), nameof(Cart.Id));
            return View(vm);
        }

        // POST: PizzaInCarts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PizzaInCartCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //pizzaInCart.Id = Guid.NewGuid();
                _pizzaInCartRepository.Add(vm.PizzaInCart);
                await _pizzaInCartRepository.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            vm.PizzaTypeSelectList = new SelectList(_context.PizzaTypes, nameof(PizzaType.Id), nameof(PizzaType.Name));
            vm.CrustSelectList = new SelectList(_context.Crusts, nameof(Crust.Id), nameof(Crust.Name));
            vm.SizeSelectList = new SelectList(_context.Sizes, nameof(Size.Id), nameof(Size.Name));
            vm.CartSelectList = new SelectList(_context.Carts, nameof(Cart.Id), nameof(Cart.Id));
            return View(vm);
        }

        // GET: PizzaInCarts/Edit/5
        public async Task<IActionResult> Edit(Guid? id, PizzaInCartCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.PizzaInCart = await _pizzaInCartRepository.FindAsync(id);
            if (vm.PizzaInCart == null)
            {
                return NotFound();
            }
            vm.PizzaTypeSelectList = new SelectList(_context.PizzaTypes, nameof(PizzaType.Id), nameof(PizzaType.Name));
            vm.CrustSelectList = new SelectList(_context.Crusts, nameof(Crust.Id), nameof(Crust.Name));
            vm.SizeSelectList = new SelectList(_context.Sizes, nameof(Size.Id), nameof(Size.Name));
            vm.CartSelectList = new SelectList(_context.Carts, nameof(Cart.Id), nameof(Cart.Id));

            return View(vm);
        }

        // POST: PizzaInCarts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PizzaInCartCreateEditViewModel vm)
        {
            if (id != vm.PizzaInCart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _pizzaInCartRepository.Update(vm.PizzaInCart);
                await _pizzaInCartRepository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            vm.PizzaTypeSelectList = new SelectList(_context.PizzaTypes, nameof(PizzaType.Id), nameof(PizzaType.Name));
            vm.CrustSelectList = new SelectList(_context.Crusts, nameof(Crust.Id), nameof(Crust.Name));
            vm.SizeSelectList = new SelectList(_context.Sizes, nameof(Size.Id), nameof(Size.Name));
            vm.CartSelectList = new SelectList(_context.Carts, nameof(Cart.Id), nameof(Cart.Id));
            return View(vm);
        }
        // GET: PizzaInCarts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaInCart = await _pizzaInCartRepository.FindAsync(id);
            if (pizzaInCart == null)
            {
                return NotFound();
            }

            return View(pizzaInCart);
        }

        // POST: PizzaInCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pizzaInCart = _pizzaInCartRepository.Remove(id);
            await _pizzaInCartRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}