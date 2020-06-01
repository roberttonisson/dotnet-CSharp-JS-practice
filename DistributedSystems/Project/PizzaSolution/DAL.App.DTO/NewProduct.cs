using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.rotoni.pizzaApp.DAL.Base;


namespace DAL.App.DTO
{
    public class NewProduct : DomainEntityIdMetadata
    {
        public bool IsActive { get; set; }
        
        [MaxLength(512)] [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common))]
        public string Description { get; set; } = default!;
        
        public Guid PizzaTypeId { get; set; } = default!;
        public PizzaType? PizzaType { get; set; }
        
    }
}