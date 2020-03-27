using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublicApi.DTO.v1
{
    public class TransportDTO
    {
        public Guid Id { get; set; }
        
        [Column(TypeName = "decimal(6,2)")] 
        public decimal Cost { get; set; } = default!;
        
        [MaxLength(256)] [MinLength(1)] public string Address { get; set; } = default!;
        
    }
}