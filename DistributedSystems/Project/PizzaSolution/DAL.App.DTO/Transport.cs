﻿using System;
 using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
 using DAL.App.DTO;
 using ee.itcollege.rotoni.pizzaApp.DAL.Base;


 namespace DAL.App.DTO
{
    
    public class Transport : DomainEntityIdMetadata
    {
        [Display(Name = nameof(Cost), ResourceType = typeof(Resources.Domain.Shared))]
        [Column(TypeName = "decimal(6,2)")] 
        public decimal Cost { get; set; } = default!;
        
        [Display(Name = nameof(Address), ResourceType = typeof(Resources.Domain.Shared))]
        [MaxLength(256, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common))] [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common))] public string Address { get; set; } = default!;
        
        public ICollection<Invoice>? Invoices { get; set; }
        
    }
}