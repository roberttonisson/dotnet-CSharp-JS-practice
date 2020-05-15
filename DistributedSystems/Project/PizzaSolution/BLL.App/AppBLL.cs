using System;
using BLL.App.Services;
using BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using DAL.App.EF;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        public AppBLL(IAppUnitOfWork uow) : base(uow)
        {
        }

        
        public IAdditionalToppingService AdditionalToppings =>
            GetService<IAdditionalToppingService>(() => new AdditionalToppingService(UOW));

        public ICartService Carts =>
            GetService<ICartService>(() => new CartService(UOW));

        public ICrustService Crusts =>
            GetService<ICrustService>(() => new CrustService(UOW));
        
        public IDefaultToppingService DefaultToppings =>
            GetService<IDefaultToppingService>(() => new DefaultToppingService(UOW));
        
        public IDrinkService Drinks =>
            GetService<IDrinkService>(() => new DrinkService(UOW));
        
        public IDrinkInCartService DrinkInCarts =>
            GetService<IDrinkInCartService>(() => new DrinkInCartService(UOW));
        
        public IInvoiceService Invoices =>
            GetService<IInvoiceService>(() => new InvoiceService(UOW));
        
        public IInvoiceLineService InvoiceLines =>
            GetService<IInvoiceLineService>(() => new InvoiceLineService(UOW));
        
        public IPartyOrderService PartyOrders =>
            GetService<IPartyOrderService>(() => new PartyOrderService(UOW));
        
        public IPartyOrderInvoiceService PartyOrderInvoices =>
            GetService<IPartyOrderInvoiceService>(() => new PartyOrderInvoiceService(UOW));
        
        public IPizzaInCartService PizzaInCarts =>
            GetService<IPizzaInCartService>(() => new PizzaInCartService(UOW));
        
        public IPizzaTypeService PizzaTypes =>
            GetService<IPizzaTypeService>(() => new PizzaTypeService(UOW));
        
        public ISizeService Sizes =>
            GetService<ISizeService>(() => new SizeService(UOW));
        
        public IToppingService Toppings =>
            GetService<IToppingService>(() => new ToppingService(UOW));
        
        public ITransportService Transports =>
            GetService<ITransportService>(() => new TransportService(UOW));
        
        public IAppUserService AppUsers =>
            GetService<IAppUserService>(() => new AppUserService(UOW));
        
        public IOrderStatusService OrderStatuses =>
            GetService<IOrderStatusService>(() => new OrderStatusService(UOW));
        
    }
}