using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;

namespace WebApp.Controllers.Client
{
    /// <summary>
    /// Client controller for the Pizzas view
    /// </summary>
    public class ClientPizzasController : Controller
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll">BLL</param>
        public ClientPizzasController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Carts
        /// <summary>
        /// Main view for client Pizzas
        /// </summary>
        /// <returns>View with PizzaViewModel</returns>
        public async Task<IActionResult> Index()
        {
            return View(new PizzasViewModel()
            {
                PizzaTypes = await _bll.PizzaTypes.GetAllAsync(),
                PizzaInCart = new PizzaInCart(),
                Toppings = _bll.Toppings.GetAllAsync().Result,
                DefaultToppings = _bll.DefaultToppings.GetAllAsync(null).Result,
                AdditionalToppings = new List<Guid>(),
                CrustSelectList = new SelectList(await _bll.Crusts.GetAllAsync(), nameof(Crust.Id),
                    nameof(Crust.DisplayName)),
                SizeSelectList = new SelectList(await _bll.Sizes.GetAllAsync(), nameof(Size.Id),
                    nameof(Size.DisplayName))
            });
        }

        /// <summary>
        /// Adds selected Pizza and it's Toppings to the Cart.
        /// </summary>
        /// <param name="pizzaInCart">Entity with pizza data and cart that will be added to DB</param>
        /// <param name="additionalToppings">Additional Toppings that will be added to the DB</param>
        /// <returns>Index View</returns>
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddToCart(PizzaInCart pizzaInCart, IEnumerable<Guid>? additionalToppings = null)
        {
            pizzaInCart.CartId = _bll.Carts.GetActiveCartWithoutCollections(User.UserGuidId()).Result.Id;

            _bll.PizzaInCarts.Add(pizzaInCart);
            await _bll.SaveChangesAsync();

            if (additionalToppings != null)
            {
                foreach (var topping in additionalToppings)
                {
                    _bll.AdditionalToppings.Add(new AdditionalTopping()
                    {
                        PizzaInCartId = pizzaInCart.Id,
                        ToppingId = topping,
                    });
                }
            }
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Check if user is logged in
        /// </summary>
        /// <returns>Index view</returns>
        [Authorize]
        public ActionResult CheckLogin()
        {
            return RedirectToAction(nameof(Index));
        }
    }
}