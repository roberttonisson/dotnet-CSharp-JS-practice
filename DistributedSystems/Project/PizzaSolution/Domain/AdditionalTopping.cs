using System;
using System.ComponentModel.DataAnnotations;
using Domain.Identity;
using ee.itcollege.rotoni.pizzaApp.DAL.Base;

namespace Domain
{
    public class AdditionalTopping : DomainEntityIdMetadata
    {

        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public Guid ToppingId { get; set; } = default!;
        public Topping? Topping { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public Guid PizzaInCartId { get; set; } = default!;
        public PizzaInCart? PizzaInCart { get; set; }
    }
}