using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.rotoni.pizzaApp.DAL.Base;


namespace Domain
{
    public class DefaultTopping : DomainEntityIdMetadata
    {

        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public Guid ToppingId { get; set; } = default!;
        public Topping? Topping { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public Guid PizzaTypeId { get; set; } = default!;
        public PizzaType? PizzaType { get; set; }
    }
}