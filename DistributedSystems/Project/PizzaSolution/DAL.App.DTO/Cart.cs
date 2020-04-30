using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.App.DTO.Identity;
using DAL.Base;


namespace DAL.App.DTO
{    
    public class Cart : DomainEntityIdMetadataUser<Domain.Identity.AppUser>
    {
        [NotMapped]
        [Display(Name = nameof(Total), ResourceType = typeof(Resources.Domain.Shared))]
        public decimal? Total { get; set; }

        public ICollection<PizzaInCart>? PizzaInCarts { get; set; }
        
        public ICollection<DrinkInCart>? DrinkInCarts { get; set; }

    }
}