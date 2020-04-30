using System;
using DAL.Base;

namespace DAL.App.DTO
{
    public class AdditionalTopping : DomainEntityIdMetadata
    {

        public Guid ToppingId { get; set; } = default!;
        public Domain.Topping? Topping { get; set; }

        public Guid PizzaInCartId { get; set; } = default!;
        public Domain.PizzaInCart? PizzaInCart { get; set; }
    }
}