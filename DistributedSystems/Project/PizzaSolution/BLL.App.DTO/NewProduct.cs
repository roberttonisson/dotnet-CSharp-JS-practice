using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.rotoni.pizzaApp.DAL.Base;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BLL.App.DTO
{
    public class NewProduct : DomainEntityIdMetadata
    {
        public bool IsActive { get; set; }
        
        [MaxLength(512)] [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public string Description { get; set; } = default!;
        
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public Guid PizzaTypeId { get; set; } = default!;
        public PizzaType? PizzaType { get; set; }

        public SelectList? PizzaTypeSelectList { get; set; }
    }
}