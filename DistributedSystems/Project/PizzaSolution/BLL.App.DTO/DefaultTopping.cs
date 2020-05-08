using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BLL.App.DTO
{
    public class DefaultTopping : DomainEntityIdMetadata
    {

        public Guid ToppingId { get; set; } = default!;
        public Topping? Topping { get; set; }

        public Guid PizzaTypeId { get; set; } = default!;
        public PizzaType? PizzaType { get; set; }
        
        public SelectList? ToppingSelectList { get; set; }
        public SelectList? PizzaTypeSelectList { get; set; }
        
    }
}