using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers.Client
{
    /// <summary>
    /// Controller for clients view of Cart.
    /// </summary>
    [Authorize]
    public class ClientCartsController : Controller
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll">Business logic layer, mapped from DOMAIN -> DATA ACCESS LAYER -> BUSINESS LOGIC LAYER.</param>
        public ClientCartsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Carts
        /// <summary>
        /// Main view.
        /// </summary>
        /// <returns>Return view with CartViewModel.</returns>
        public async Task<IActionResult> Index()
        {
            return View(new CartViewModel()
            {
                Cart = await _bll.Carts.GetActiveCart(User.UserGuidId()),
                PizzaInCart = new Guid(),
                DrinkInCart = new Guid(),
                AdditionalToppings = new List<Guid>()
            });
        }

        /// <summary>
        /// Remove item from Cart.
        /// </summary>
        /// <param name="pizzaInCart">Id for PizzaInCart entity to remove from DB</param>
        /// <param name="drinkInCart">Id for DrinkInCart entity to remove from DB</param>
        /// <param name="additionalToppings">AdditionalToppings for pizzaInCart that also need to be removed from DB.</param>
        /// <returns></returns>
        public async Task<IActionResult> Remove(Guid pizzaInCart, Guid drinkInCart,
            List<Guid>? additionalToppings = null)
        {
            await _bll.Carts.RemoveFromCart(_bll, pizzaInCart, drinkInCart, additionalToppings);

            return RedirectToAction(nameof(Index));
        }
        // GET: Carts
        /// <summary>
        /// View for paying for the items in Cart.
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        public IActionResult Payment(decimal total)
        {
            return View(new PaymentViewModel()
            {
                Total = total,
                Transport = new Transport()
            });
        }
        
        /// <summary>
        /// Redirect to the Payment page with necessary data.
        /// </summary>
        /// <param name="total">The total cost to be paid</param>
        /// <returns></returns>
        public IActionResult PaymentRedirect(decimal total)
        {
            return RedirectToAction(nameof(Payment), new {Total = total} );
        }
        
        /// <summary>
        /// Adds/edits necessary records to the DB for successful paying.
        /// </summary>
        /// <param name="transport">Transport entity</param>
        /// <returns></returns>
        public async Task<IActionResult> Pay(Transport transport)
        {

            await _bll.Carts.Pay(_bll, transport, User.UserGuidId());
            return RedirectToAction(nameof(Index), "ClientOrders");
        }
    }
}