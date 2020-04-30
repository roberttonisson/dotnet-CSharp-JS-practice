using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{    
    public class Cart : DomainEntityIdMetadataUser<AppUser>
    {
        [NotMapped]
        [Display(Name = nameof(Total), ResourceType = typeof(Resources.Domain.Shared))]
        public decimal? Total { get; set; }

        public ICollection<PizzaInCart>? PizzaInCarts { get; set; }
        
        public ICollection<DrinkInCart>? DrinkInCarts { get; set; }

    }
}