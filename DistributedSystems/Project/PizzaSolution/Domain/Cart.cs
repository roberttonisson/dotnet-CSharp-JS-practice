using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Cart : DomainEntity
    {
     
        [NotMapped]
        [Display(Name = nameof(Total), ResourceType = typeof(Resources.Domain.Shared))]
        public decimal Total { get; set; }

        [ForeignKey("AppUser")]
        public Guid UserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }

        public ICollection<PizzaInCart>? PizzaInCarts { get; set; }
        
        public ICollection<DrinkInCart>? DrinkInCarts { get; set; }

    }
}