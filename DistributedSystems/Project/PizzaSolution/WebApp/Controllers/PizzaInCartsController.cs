using System;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;

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
            return View();
        }

        // POST: PizzaInCarts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Quantity,PizzaTypeId,CrustId,SizeId,CartId,CreatedBy,CreatedAt,CreatedBy,CreatedAt,Id")]
            PizzaInCart pizzaInCart)
        {
            if (ModelState.IsValid)
            {
                //pizzaInCart.Id = Guid.NewGuid();
                _pizzaInCartRepository.Add(pizzaInCart);
                await _pizzaInCartRepository.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(pizzaInCart);
        }

        // GET: PizzaInCarts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
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

        // POST: PizzaInCarts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Quantity,PizzaTypeId,CrustId,SizeId,CartId,CreatedBy,CreatedAt,CreatedBy,CreatedAt,Id")]
            PizzaInCart pizzaInCart)
        {
            if (id != pizzaInCart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _pizzaInCartRepository.Update(pizzaInCart);
                await _pizzaInCartRepository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            return View(pizzaInCart);
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