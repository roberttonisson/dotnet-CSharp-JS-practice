#pragma warning disable 1591
using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewProduct = BLL.App.DTO.NewProduct;

namespace WebApp.Areas.Admin.Controllers

{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class NewProductsController : Controller
    {
        private readonly IAppBLL _bll;

        public NewProductsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: NewProducts
        public async Task<IActionResult> Index()
        {
            return View(await _bll.NewProducts.GetAllAsync(User.UserGuidId()));
        }

        // GET: NewProducts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newProduct = await _bll.NewProducts.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (newProduct == null)
            {
                return NotFound();
            }

            return View(newProduct);
        }

        // GET: NewProducts/Create
        public async Task<IActionResult> Create()
        {
            var newProduct = new NewProduct();
            newProduct.PizzaTypeSelectList = new SelectList(await _bll.PizzaTypes.GetAllAsync(), nameof(PizzaType.Id), nameof(PizzaType.Name));
            return View(newProduct);
        }

        // POST: NewProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.NewProduct newProduct)
        {
            if (ModelState.IsValid)
            {
                _bll.NewProducts.Add(newProduct);
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            newProduct.PizzaTypeSelectList = new SelectList(await _bll.PizzaTypes.GetAllAsync(), nameof(PizzaType.Id), nameof(PizzaType.Name));
            return View(newProduct);
        }

        // GET: NewProducts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var newProduct = await _bll.NewProducts.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            
            if (newProduct == null)
            {
                return NotFound();
            }
            newProduct.PizzaTypeSelectList = new SelectList(await _bll.PizzaTypes.GetAllAsync(), nameof(PizzaType.Id), nameof(PizzaType.Name));
            return View(newProduct);
        }

        // POST: NewProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, NewProduct newProduct)
        {
            if (id != newProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.NewProducts.UpdateAsync(newProduct);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            newProduct.PizzaTypeSelectList = new SelectList(await _bll.PizzaTypes.GetAllAsync(), nameof(PizzaType.Id), nameof(PizzaType.Name));
            return View(newProduct);
        }

        // GET: NewProducts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newProduct = await _bll.NewProducts.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (newProduct == null)
            {
                return NotFound();
            }

            return View(newProduct);
        }

        // POST: NewProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.NewProducts.RemoveAsync(id, User.UserGuidId());
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}