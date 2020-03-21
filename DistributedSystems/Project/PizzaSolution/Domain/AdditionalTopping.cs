using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class AdditionalTopping : DomainEntity
    {
        public Guid ToppingId { get; set; } = default!;
        public Topping? Topping { get; set; }

        public Guid PizzaInCartId { get; set; } = default!;
        public PizzaInCart? PizzaInCart { get; set; }
    }
}