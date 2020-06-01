using System;
using System.Collections.Generic;
using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    /// <summary>
    /// View model for ClientCarts view
    /// </summary>
    public class CartViewModel
    {
        /// <summary>
        /// User's active cart.
        /// </summary>
        public Cart Cart { get; set; } = default!;

        /// <summary>
        /// ID of PizzaInCart that User is going to remove.
        /// </summary>
        public Guid? PizzaInCart { get; set; } = default!;
       
        /// <summary>
        /// ID of DrinkInCArt that User is going to remove.
        /// </summary>
        public Guid? DrinkInCart { get; set; } = default!;
        
        /// <summary>
        /// AdditionalToppings IDs that are going to be removed.
        /// </summary>
        public List<Guid>? AdditionalToppings { get; set; } = default!;
        
        /// <summary>
        /// Total cost of items in Cart
        /// </summary>
        public decimal Total { get; set; } = default!;
        
    }
}