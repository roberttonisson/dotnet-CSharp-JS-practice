using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


using Domain.Identity;
using ee.itcollege.rotoni.pizzaApp.DAL.Base;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BLL.App.DTO
{
    public class Invoice : DomainEntityIdMetadataUser<AppUser>
    {
        [Display(Name = nameof(IsPaid), ResourceType = typeof(Resources.Domain.Shared))]
        public bool IsPaid { get; set; } = false;

        public DateTime? Estimated { get; set; }
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public Guid TransportId { get; set; } = default!;
        public Transport? Transport { get; set; }
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public Guid OrderStatusId { get; set; } = default!;
        public OrderStatus? OrderStatus { get; set; }

        public ICollection<InvoiceLine>? InvoiceLines { get; set; }

        public ICollection<PartyOrderInvoice>? PartyOrderInvoices { get; set; }

        public SelectList? AppUserSelectList { get; set; }
        public SelectList? TransportSelectList { get; set; }
        public SelectList? OrderStatusSelectList { get; set; }


        [Display(Name = nameof(Total), ResourceType = typeof(Resources.Domain.Shared))]
        [NotMapped]
        public decimal Total
        {
            get
            {
                decimal total = 0;
                if (InvoiceLines != null)
                {
                    foreach (var invoiceLine in InvoiceLines)
                    {
                        if (invoiceLine.DrinkInCart != null)
                        {
                            total += invoiceLine.DrinkInCart.Total;
                        }

                        if (invoiceLine.PizzaInCart != null)
                        {
                            total += invoiceLine.PizzaInCart.Total;
                        }
                    }
                }

                return total;
            }
        }
    }
}