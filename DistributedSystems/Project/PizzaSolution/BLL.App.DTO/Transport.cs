﻿using System;
 using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 using Contracts.DAL.Base;
 using DAL.Base;

namespace BLL.App.DTO
{
    
    public class Transport : DomainEntityIdMetadata
    {
        [Display(Name = nameof(Cost), ResourceType = typeof(Resources.Domain.Shared))]
        [Column(TypeName = "decimal(6,2)")] 
        public decimal Cost { get; set; } = default!;
        
        [Display(Name = nameof(Address), ResourceType = typeof(Resources.Domain.Shared))]
        [MaxLength(256)] [MinLength(1)] public string Address { get; set; } = default!;
        
        public ICollection<Invoice>? Invoices { get; set; }
        
    }
}