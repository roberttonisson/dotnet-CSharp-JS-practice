using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublicApi.DTO.v1
{
    public class SizeDTO
    {
        
        public Guid Id { get; set; }
        
        [MaxLength(64)] [MinLength(1)] public string Name { get; set; } = default!;
        
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; } = default!;
        [Column(TypeName = "decimal(6,2)")]
        public decimal SizeCm { get; set; } = default!;
    }
}