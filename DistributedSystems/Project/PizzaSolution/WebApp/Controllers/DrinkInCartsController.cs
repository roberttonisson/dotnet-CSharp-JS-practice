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
    public class DrinkInCartsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IDrinkInCartRepository _drinkInCartRepository;

        public DrinkInCartsController(AppDbContext context)
        {
            _context = context;
            _drinkInCartRepository = new DrinkInCartRepository(_context);
        }

        // GET: DrinkInCarts
        public async Task<IActionResult> Index()
        {
            return View(await _drinkInCartRepository.AllAsync());
        }

        // GET: DrinkInCarts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkInCart = await _drinkInCartRepository.FindAsync(id);

            if (drinkInCart == null)
            {
                return NotFound();
            }

            return View(drinkInCart);
        }

           // GET: DrinkInCarts/Create
        public IActionResult Create()
        {
            var vm =  new DrinkInCartCreateEditViewModel();
            vm.DrinkSelectList = new SelectList(_context.Drinks, nameof(Drink.Id), nameof(Drink.Name));
            vm.CartSelectList = new SelectList(_context.Carts, nameof(Cart.Id), nameof(Cart.Id));
            return View(vm);
        }

        // POST: DrinkInCarts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DrinkInCartCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //drinkInCart.Id = Guid.NewGuid();
                _drinkInCartRepository.Add(vm.DrinkInCart);
                await _drinkInCartRepository.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            vm.DrinkSelectList = new SelectList(_context.Drinks, nameof(Drink.Id), nameof(Drink.Name));
            vm.CartSelectList = new SelectList(_context.Carts, nameof(Cart.Id), nameof(Cart.Id));
            return View(vm);
        }

        // GET: DrinkInCarts/Edit/5
        public async Task<IActionResult> Edit(Guid? id, DrinkInCartCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.DrinkInCart = await _drinkInCartRepository.FindAsync(id);
            if (vm.DrinkInCart == null)
            {
                return NotFound();
            }
            vm.DrinkSelectList = new SelectList(_context.Drinks, nameof(Drink.Id), nameof(Drink.Name));
            vm.CartSelectList = new SelectList(_context.Carts, nameof(Cart.Id), nameof(Cart.Id));

            return View(vm);
        }

        // POST: DrinkInCarts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, DrinkInCartCreateEditViewModel vm)
        {
            if (id != vm.DrinkInCart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _drinkInCartRepository.Update(vm.DrinkInCart);
                await _drinkInCartRepository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            vm.DrinkSelectList = new SelectList(_context.Drinks, nameof(Drink.Id), nameof(Drink.Name));
            vm.CartSelectList = new SelectList(_context.Carts, nameof(Cart.Id), nameof(Cart.Id));
            return View(vm);
        }

        // GET: DrinkInCarts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkInCart = await _drinkInCartRepository.FindAsync(id);
            if (drinkInCart == null)
            {
                return NotFound();
            }

            return View(drinkInCart);
        }

        // POST: DrinkInCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var drinkInCart = _drinkInCartRepository.Remove(id);
            await _drinkInCartRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}