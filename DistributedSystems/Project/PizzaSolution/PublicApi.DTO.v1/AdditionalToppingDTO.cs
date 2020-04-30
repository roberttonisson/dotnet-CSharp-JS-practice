using System;
using DAL.Base;
using Domain;

namespace PublicApi.DTO.v1
{
    public class AdditionalToppingDTO : DomainEntityId
    {

        public Guid ToppingId { get; set; } = default!;
        public ToppingDTO? Topping { get; set; }

        public Guid PizzaInCartId { get; set; } = default!;
        public PizzaInCartDTO? PizzaInCart { get; set; }
    }
}