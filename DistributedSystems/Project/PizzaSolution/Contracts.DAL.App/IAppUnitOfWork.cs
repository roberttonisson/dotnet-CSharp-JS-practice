using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
        IAdditionalToppingRepository AdditionalToppings { get; } 
        ICartRepository Carts { get; } 
        ICrustRepository Crusts { get; } 
        IDefaultToppingRepository DefaultToppings { get; } 
        IDrinkRepository Drinks { get; } 
        IDrinkInCartRepository DrinkInCarts { get; } 
        IInvoiceRepository Invoices { get; } 
        IInvoiceLineRepository InvoiceLines { get; } 
        IPartyOrderRepository PartyOrders { get; } 
        IPartyOrderInvoiceRepository PartyOrderInvoices { get; } 
        IPizzaInCartRepository PizzaInCarts { get; } 
        IPizzaTypeRepository PizzaTypes { get; } 
        ISizeRepository Sizes { get; } 
        IToppingRepository Toppings { get; } 
        ITransportRepository Transports { get; }
        IUserRepository Users { get; }
        
    }
}