using System;
using System.Collections.Generic;
using BLL.App.DTO;

namespace WebApp.Models
{
    /// <summary>
    /// View model for ClientOrders view
    /// </summary>
    public class OrdersViewModel
    {
        /// <summary>
        /// All of the User's Invoices
        /// </summary>
        public IEnumerable<Invoice> Invoices { get; set; } = default!;
        /// <summary>
        /// ID of the invoice that User is going to order again
        /// </summary>
        public Guid? InvoiceId { get; set; }
    }
}