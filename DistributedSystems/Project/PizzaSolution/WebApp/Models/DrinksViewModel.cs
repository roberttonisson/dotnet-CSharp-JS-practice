using System.Collections;
using System.Collections.Generic;
using BLL.App.DTO;
#pragma warning disable 1591
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    /// <summary>
    /// View model for ClientDrinks view
    /// </summary>
    public class DrinksViewModel
    {
        /// <summary>
        /// Drink that is going to be added to the Cart
        /// </summary>
        public DrinkInCart DrinkInCart { get; set; } = default!;

        /// <summary>
        /// All the available drinks
        /// </summary>
        public IEnumerable<Drink> Drinks { get; set; } = default!;
        
    }
}