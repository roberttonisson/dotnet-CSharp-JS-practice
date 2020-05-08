using System;
using DAL.Base;

namespace DAL.App.DTO
{
    public class DefaultTopping : DomainEntityIdMetadata
    {

        public Guid ToppingId { get; set; } = default!;
        public Topping? Topping { get; set; }

        public Guid PizzaTypeId { get; set; } = default!;
        public PizzaType? PizzaType { get; set; }
    }
}