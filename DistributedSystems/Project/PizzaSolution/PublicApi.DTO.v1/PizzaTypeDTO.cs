using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;
using Domain;

namespace PublicApi.DTO.v1
{
    public class PizzaTypeDTO : DomainEntityId
    {
        [MaxLength(64)] [MinLength(1)] 
        public string Name { get; set; } = default!;

        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; } = default!;
        
    }
}