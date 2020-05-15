using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;

namespace PublicApi.DTO.v1
{
    public class OrderStatusDTO : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(256)] [MinLength(1)] public string Status { get; set; } = default!;
        
    }
}