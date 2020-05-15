using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace DAL.App.DTO
{
    public class OrderStatus : DomainEntityIdMetadata
    {
        [MaxLength(256)] [MinLength(1)] public string Status { get; set; } = default!;

        public ICollection<Invoice>? Invoices { get; set; }
    }
}