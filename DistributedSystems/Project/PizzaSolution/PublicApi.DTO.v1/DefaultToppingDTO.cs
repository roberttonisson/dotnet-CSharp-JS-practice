using System;
using DAL.Base;
using Domain;

namespace PublicApi.DTO.v1
{
    public class DefaultToppingDTO : DomainEntityId
    {

        public Guid ToppingId { get; set; } = default!;
        public ToppingDTO? Topping { get; set; }

        public Guid PizzaTypeId { get; set; } = default!;
        public PizzaTypeDTO? PizzaType { get; set; }
    }
}