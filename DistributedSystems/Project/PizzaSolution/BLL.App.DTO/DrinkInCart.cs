using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.rotoni.pizzaApp.DAL.Base;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BLL.App.DTO
{
    public class DrinkInCart : DomainEntityIdMetadata
    {
        [Display(Name = nameof(Quantity), ResourceType = typeof(Resources.Domain.Shared))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public int Quantity { get; set; } = default;

        [Display(Name = nameof(Price), ResourceType = typeof(Resources.Domain.Shared))]
        [NotMapped]
        public decimal? Price { get; set; }
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public Guid DrinkId { get; set; } = default!;
        public Drink? Drink { get; set; }
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public Guid CartId { get; set; } = default!;
        public Cart? Cart { get; set; }

        public ICollection<InvoiceLine>? InvoiceLines { get; set; }

        public SelectList? DrinkSelectList { get; set; }
        public SelectList? CartSelectList { get; set; }

        public decimal Total
        {
            get
            {
                decimal total = 0;
                if (Drink != null) return Drink.Price * Quantity;
                return total;
                
            }
        }
    }
}
