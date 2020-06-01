using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ee.itcollege.rotoni.pizzaApp.DAL.Base;
using PublicApi.DTO.v1.Identity;
using AppUser = BLL.App.DTO.Identity.AppUser;

namespace PublicApi.DTO.v1
{
    public class CartDTO : DomainEntityId
    {
        public decimal? Total { get; set; }
        
        public bool Active { get; set; } = true;

        public Guid AppUserId { get; set; } = default!;
        public AppUserDTO? AppUser { get; set; }
        
        public ICollection<PizzaInCartDTO>? PizzaInCarts { get; set; }
        
        public ICollection<DrinkInCartDTO>? DrinkInCarts { get; set; }
        
    }
}