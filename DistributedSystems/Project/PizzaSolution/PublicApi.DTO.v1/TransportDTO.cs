using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;

namespace PublicApi.DTO.v1
{
    public class TransportDTO : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [Column(TypeName = "decimal(6,2)")] 
        public decimal Cost { get; set; } = default!;

        [MaxLength(256)] [MinLength(1)] public string Address { get; set; } = default!;
        
    }
}