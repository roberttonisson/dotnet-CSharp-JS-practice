using System;
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
    /// Client controller for Drinks
    /// </summary>
    public class ClientDrinksController : Controller
    {
        
        private readonly IAppBLL _bll;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll">BLL</param>
        public ClientDrinksController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Carts
        /// <summary>
        /// Main view for Drinks
        /// </summary>
        /// <returns>View with DrinksViewModel</returns>
        public async Task<IActionResult> Index()
        {
            return View(new DrinksViewModel()
            {
                Drinks = await _bll.Drinks.GetAllAsync(),
                DrinkInCart = new DrinkInCart(),
            });
        }
        
        /// <summary>
        /// Add Drink to the Cart
        /// </summary>
        /// <param name="drinkInCart">DB entity for DrinkInCart</param>
        /// <returns>Return back to the main View</returns>
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddToCart(DrinkInCart drinkInCart)
        {
            drinkInCart.CartId = _bll.Carts.GetActiveCartWithoutCollections(User.UserGuidId()).Result.Id;
            
            _bll.DrinkInCarts.Add(drinkInCart);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
            
        }
        
        /// <summary>
        /// Checks if User is logged in.
        /// </summary>
        /// <returns>Return back to the main View</returns>
        [Authorize]
        public ActionResult  CheckLogin()
        {
          //  ClientDrinksController viewModel = new ClientDrinksController(_bll);
            return RedirectToAction(nameof(Index));
        }
    }
}