using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    
    public class OrderStatus : DomainEntityIdMetadata
    {
        [MaxLength(256)] [MinLength(1)] public string Status { get; set; } = default!;
        
        public ICollection<Invoice>? Invoices { get; set; }

    }
}