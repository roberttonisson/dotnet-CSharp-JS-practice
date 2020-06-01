using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ee.itcollege.rotoni.pizzaApp.DAL.Base;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BLL.App.DTO
{
    public class PizzaInCart : DomainEntityIdMetadata
    {
        [Display(Name = nameof(Price), ResourceType = typeof(Resources.Domain.Shared))]
        [NotMapped] public decimal? Price { get; set; }

        [Display(Name = nameof(Quantity), ResourceType = typeof(Resources.Domain.Shared))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public int Quantity { get; set; } = default;
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public Guid PizzaTypeId { get; set; } = default!;
        public PizzaType? PizzaType { get; set; }
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public Guid CrustId { get; set; } = default!;
        public Crust? Crust { get; set; }
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public Guid SizeId { get; set; } = default!;
        public Size? Size { get; set; }
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public Guid CartId { get; set; } = default!;
        public Cart? Cart { get; set; }

        public ICollection<InvoiceLine>? InvoiceLines { get; set; }
        public ICollection<AdditionalTopping>? AdditionalToppings { get; set; }
        
        public SelectList? PizzaTypeSelectList { get; set; }
        public SelectList? CrustSelectList { get; set; }
        public SelectList? SizeSelectList { get; set; }
        public SelectList? CartSelectList { get; set; }

        public decimal Total
        {
            get
            {
                decimal total = 0;
                if (Crust == null || Size == null || PizzaType == null)
                {
                    return total;
                }

                total = Crust.Price + Size.Price + PizzaType.Price;
                if (AdditionalToppings != null)
                {
                    total += AdditionalToppings!.Sum(additional => additional.Topping!.Price);
                }
                return total * Quantity;
            }
        }
    }
}