using System;
using System.Collections.Generic;
using ee.itcollege.rotoni.pizzaApp.DAL.Base;


namespace PublicApi.DTO.v1
{
    public class PizzaInCartDTO : DomainEntityId
    {
        public decimal? Price { get; set; }

        public int Quantity { get; set; } = default;

        public Guid PizzaTypeId { get; set; } = default!;
        public PizzaTypeDTO? PizzaType { get; set; }

        public Guid CrustId { get; set; } = default!;
        public CrustDTO? Crust { get; set; }

        public Guid SizeId { get; set; } = default!;
        public SizeDTO? Size { get; set; }

        public Guid CartId { get; set; } = default!;
        public CartDTO? Cart { get; set; }
        
        public ICollection<AdditionalToppingDTO>? AdditionalToppings { get; set; }
        

    }
}