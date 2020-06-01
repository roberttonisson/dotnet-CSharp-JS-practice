using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.App.DTO.Identity;
using ee.itcollege.rotoni.pizzaApp.DAL.Base;


namespace DAL.App.DTO
{    
    public class Cart : DomainEntityIdMetadataUser<Domain.Identity.AppUser>
    {

        public bool Active { get; set; } = true;

        public ICollection<PizzaInCart>? PizzaInCarts { get; set; }
        
        public ICollection<DrinkInCart>? DrinkInCarts { get; set; }

    }
}