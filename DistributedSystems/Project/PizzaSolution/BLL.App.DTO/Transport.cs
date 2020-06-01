﻿using System;
 using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 using ee.itcollege.rotoni.pizzaApp.DAL.Base;


 namespace BLL.App.DTO
{
    
    public class Transport : DomainEntityIdMetadata
    {
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        [Display(Name = nameof(Cost), ResourceType = typeof(Resources.Domain.Shared))]
        [Column(TypeName = "decimal(6,2)")] 
        public decimal Cost { get; set; } = default!;
        
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        [Display(Name = nameof(Address), ResourceType = typeof(Resources.Domain.Shared))]
        [MaxLength(256, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common))] [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common))] public string Address { get; set; } = default!;
        
        public ICollection<Invoice>? Invoices { get; set; }
        
    }
}