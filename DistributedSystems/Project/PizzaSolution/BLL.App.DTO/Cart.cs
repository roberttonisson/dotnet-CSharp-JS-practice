using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


using Domain.Identity;
using ee.itcollege.rotoni.pizzaApp.DAL.Base;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BLL.App.DTO
{    
    public class Cart : DomainEntityIdMetadataUser<AppUser>
    {
        public SelectList? AppUserSelectList { get; set; }
        
        public bool Active { get; set; } = true;
        
        public ICollection<PizzaInCart>? PizzaInCarts { get; set; }
        
        public ICollection<DrinkInCart>? DrinkInCarts { get; set; }
        
        
        [NotMapped]
        [Display(Name = nameof(Total), ResourceType = typeof(Resources.Domain.Shared))]
        public decimal? Total 
        {
            get
            {
                decimal total = 0;
                if (PizzaInCarts != null)
                {
                    total += PizzaInCarts!.Sum(pizzaInCart => pizzaInCart.Total);
                }
                if (DrinkInCarts != null)
                {
                    total += DrinkInCarts!.Sum(drinkInCart => drinkInCart.Total);
                }
                return total;
            }
        }
        
    }
}