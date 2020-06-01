using System;
using System.ComponentModel.DataAnnotations;
using Domain;
using ee.itcollege.rotoni.pizzaApp.DAL.Base;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BLL.App.DTO
{
    public class AdditionalTopping : DomainEntityIdMetadata
    {
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public Guid ToppingId { get; set; } = default!;
        public Topping? Topping { get; set; }
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public Guid PizzaInCartId { get; set; } = default!;
        public PizzaInCart? PizzaInCart { get; set; }
        
        public SelectList? PizzaInCartSelectList { get; set; }
        
        public SelectList? ToppingSelectList { get; set; }
    }
}