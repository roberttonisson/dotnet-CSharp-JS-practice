﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


using Domain;
using ee.itcollege.rotoni.pizzaApp.DAL.Base;

namespace BLL.App.DTO
{
    public class Topping : DomainEntityIdMetadata
    {
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        [Display(Name = nameof(Name), ResourceType = typeof(Resources.Domain.Shared))]
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common))] [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common))] public string Name { get; set; } = default!;

        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        [Display(Name = nameof(Price), ResourceType = typeof(Resources.Domain.Shared))]
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; } = default!;
        
        public ICollection<DefaultTopping>? DefaultToppings { get; set; }
        
        public ICollection<BLL.App.DTO.AdditionalTopping>? AdditionalToppings { get; set; }
    }
}