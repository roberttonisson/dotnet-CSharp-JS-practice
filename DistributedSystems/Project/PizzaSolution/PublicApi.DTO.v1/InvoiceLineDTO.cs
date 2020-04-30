using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;
using Domain;

namespace PublicApi.DTO.v1
{
    public class InvoiceLineDTO : DomainEntityId
    {
        public decimal? Total { get; set; }
        
        public int Quantity { get; set; }

        public Guid? PizzaInCartId { get; set; }
        public PizzaInCartDTO? PizzaInCart { get; set; }

        public Guid? DrinkInCartId { get; set; }
        public DrinkInCartDTO? DrinkInCart { get; set; }

        public Guid InvoiceId { get; set; } = default!;
        public InvoiceDTO? Invoice { get; set; }
    }
}