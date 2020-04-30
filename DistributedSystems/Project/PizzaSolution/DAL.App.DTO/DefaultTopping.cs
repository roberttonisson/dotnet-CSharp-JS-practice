using System;
using DAL.Base;

namespace DAL.App.DTO
{
    public class DefaultTopping : DomainEntityIdMetadata
    {

        public Guid ToppingId { get; set; } = default!;
        public Domain.Topping? Topping { get; set; }

        public Guid PizzaTypeId { get; set; } = default!;
        public Domain.PizzaType? PizzaType { get; set; }
    }
}