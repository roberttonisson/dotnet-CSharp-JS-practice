using Contracts.BLL.App.Services;
using ee.itcollege.rotoni.pizzaApp.Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        public IAdditionalToppingService AdditionalToppings { get; } 
        public ICartService Carts { get; } 
        public ICrustService Crusts { get; } 
        public IDefaultToppingService DefaultToppings { get; } 
        public IDrinkService Drinks { get; } 
        public IDrinkInCartService DrinkInCarts { get; } 
        public IInvoiceService Invoices { get; } 
        public IInvoiceLineService InvoiceLines { get; } 
        public IPartyOrderService PartyOrders { get; } 
        public IPartyOrderInvoiceService PartyOrderInvoices { get; } 
        public IPizzaInCartService PizzaInCarts { get; } 
        public IPizzaTypeService PizzaTypes { get; } 
        public ISizeService Sizes { get; } 
        public IToppingService Toppings { get; } 
        public ITransportService Transports { get; }
        public IAppUserService AppUsers { get; }
        public IOrderStatusService OrderStatuses { get; }
        public INewProductService NewProducts { get; }
    }
}