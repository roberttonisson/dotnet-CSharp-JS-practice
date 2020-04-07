using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
    public class Size : DomainEntity
    {
        [Display(Name = nameof(Name), ResourceType = typeof(Resources.Domain.Shared))]
        [MaxLength(64)] [MinLength(1)] public string Name { get; set; } = default!;
        
        [Display(Name = nameof(Price), ResourceType = typeof(Resources.Domain.Shared))]
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; } = default!;
        
        [Display(Name = nameof(SizeCm), ResourceType = typeof(Resources.Domain.Size))]
        [Column(TypeName = "decimal(6,2)")]
        public decimal SizeCm { get; set; } = default!;
        
        public ICollection<PizzaInCart>? PizzaInCarts { get; set; }
    }
}