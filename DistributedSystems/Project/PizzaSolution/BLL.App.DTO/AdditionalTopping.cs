using System;
using DAL.Base;
using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BLL.App.DTO
{
    public class AdditionalTopping : DomainEntityIdMetadata
    {

        public Guid ToppingId { get; set; } = default!;
        public Topping? Topping { get; set; }

        public Guid PizzaInCartId { get; set; } = default!;
        public PizzaInCart? PizzaInCart { get; set; }
        
        public SelectList? PizzaInCartSelectList { get; set; }
        
        public SelectList? ToppingSelectList { get; set; }
    }
}