using System;
using System.Collections.Generic;
using ee.itcollege.rotoni.pizzaApp.DAL.Base;


namespace DAL.App.DTO
{
    public class AdditionalTopping : DomainEntityIdMetadata
    {

        public Guid ToppingId { get; set; } = default!;
        public Topping? Topping { get; set; }

        public Guid PizzaInCartId { get; set; } = default!;
        public PizzaInCart? PizzaInCart { get; set; }
        
        public ICollection<Domain.PizzaInCart>? PizzaInCarts { get; set; }
        
        public ICollection<Domain.DrinkInCart>? DrinkInCarts { get; set; }
    }
}