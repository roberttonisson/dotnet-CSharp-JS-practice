﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace DAL.App.DTO
{
    
    public class Drink : DomainEntityIdMetadata
    {
        [Display(Name = nameof(Name), ResourceType = typeof(Resources.Domain.Shared))]
        [MaxLength(64)] [MinLength(1)] public string Name { get; set; } = default!;

        [Display(Name = nameof(Price), ResourceType = typeof(Resources.Domain.Shared))]
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; } = default!;
        [Display(Name = nameof(Size), ResourceType = typeof(Resources.Domain.Shared))]
        [Column(TypeName = "decimal(4,3)")]
        public decimal Size { get; set; } = default!;

        public ICollection<DrinkInCart>? DrinkInCarts{ get; set; }
        
    }
}