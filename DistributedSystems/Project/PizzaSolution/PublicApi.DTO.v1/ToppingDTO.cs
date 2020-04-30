using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;

namespace PublicApi.DTO.v1
{
    public class ToppingDTO : IDomainEntityId
    {
        
        public Guid Id { get; set; }
        
        [MaxLength(64)] [MinLength(1)] public string Name { get; set; } = default!;

        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; } = default!;
    }
}