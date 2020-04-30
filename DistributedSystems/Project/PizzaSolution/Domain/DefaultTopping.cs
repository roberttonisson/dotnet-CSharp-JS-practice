using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class DefaultTopping : DomainEntityIdMetadata
    {

        public Guid ToppingId { get; set; } = default!;
        public Topping? Topping { get; set; }

        public Guid PizzaTypeId { get; set; } = default!;
        public PizzaType? PizzaType { get; set; }
    }
}