using System;
using System.Collections.Generic;
using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    /// <summary>
    /// View model for the ClientPizzas view
    /// </summary>
    public class PizzasViewModel
    {
        /// <summary>
        /// Pizza that is going to be added to the Cart.
        /// </summary>
        public PizzaInCart PizzaInCart { get; set; } = default!;

        /// <summary>
        /// All of the available PizzaTypes
        /// </summary>
        public IEnumerable<PizzaType> PizzaTypes { get; set; } = default!;
        /// <summary>
        /// All of the available Toppings
        /// </summary>
        public IEnumerable<Topping> Toppings { get; set; } = default!;
        /// <summary>
        /// All of the default topping for the pizzas
        /// </summary>
        public IEnumerable<DefaultTopping> DefaultToppings { get; set; } = default!;
        /// <summary>
        /// Additional tppings that will be added to the PizzaInCart
        /// </summary>
        public IEnumerable<Guid>? AdditionalToppings { get; set; }
        
        
        /// <summary>
        /// SelectList for all the Crusts
        /// </summary>
        public SelectList? CrustSelectList { get; set; }
                
        /// <summary>
        /// SelectList for all the Sizes
        /// </summary>
        public SelectList? SizeSelectList { get; set; }
    }
}