using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class DefaultTopping : DomainEntity
    {
        public Guid ToppingId { get; set; } = default!;
        public Topping? Topping { get; set; }

        public Guid PizzaTypeId { get; set; } = default!;
        public PizzaType? PizzaType { get; set; }
    }
}