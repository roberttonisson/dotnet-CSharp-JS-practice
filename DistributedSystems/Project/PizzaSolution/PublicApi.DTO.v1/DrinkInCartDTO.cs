using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain;
using ee.itcollege.rotoni.pizzaApp.DAL.Base;

namespace PublicApi.DTO.v1
{
    
    public class DrinkInCartDTO : DomainEntityId
    {

        public int Quantity { get; set; }
        
        public decimal? Price { get; set; }

        public Guid DrinkId { get; set; } = default!;
        public DrinkDTO? Drink { get; set; }

        public Guid CartId { get; set; } = default!;
        public CartDTO? Cart { get; set; }
    }
}