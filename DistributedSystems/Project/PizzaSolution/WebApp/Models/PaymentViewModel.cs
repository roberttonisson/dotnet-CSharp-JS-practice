using BLL.App.DTO;

namespace WebApp.Models
{
    /// <summary>
    /// View model for the ClientCarts Payment view
    /// </summary>
    public class PaymentViewModel
    {
        /// <summary>
        /// Total amount to be paid for
        /// </summary>
        public decimal Total { get; set; } = default!;
        
        /// <summary>
        /// Transport for the Order/Invoice
        /// </summary>
        public Transport Transport { get; set; } = default!;
    }
}