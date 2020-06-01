using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.rotoni.pizzaApp.DAL.Base;


namespace BLL.App.DTO
{
    public class Crust : DomainEntityIdMetadata
    {

        [Display(Name = nameof(Name), ResourceType = typeof(Resources.Domain.Shared))]
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common))] 
        [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common))] 
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public string Name { get; set; } = default!;
        
        [Display(Name = nameof(Price), ResourceType = typeof(Resources.Domain.Shared))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        [Column(TypeName = "decimal(6,2)")]public decimal Price { get; set; } = default!;
        
        public ICollection<PizzaInCart>? PizzaInCarts { get; set; }
        
        [NotMapped]
        public string DisplayName => this.Name + " -- €" + this.Price.ToString();
        
    }
}