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
    /// Client controller for Invoices
    /// </summary>
    [Authorize]
    public class ClientOrdersController : Controller
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll">BLL</param>
        public ClientOrdersController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Carts
        /// <summary>
        /// Main view for Invoices/Orders
        /// </summary>
        /// <returns>View with OrdersViewModel</returns>
        public async Task<IActionResult> Index()
        {
            return View(new OrdersViewModel()
            {
                Invoices = await _bll.Invoices.GetAllWithCollectionsAsync(User.UserGuidId())
            });
        }

        /// <summary>
        /// Adds the items from selected Invoice to the Cart again.
        /// </summary>
        /// <param name="invoiceId">ID for the selected Invoice</param>
        /// <returns></returns>
        public async Task<IActionResult> ReOrder(Guid invoiceId)
        {
            await _bll.Invoices.ReOrder(_bll, invoiceId, User.UserGuidId());
            return RedirectToAction(nameof(Index), "ClientCarts");
        }
    }
}