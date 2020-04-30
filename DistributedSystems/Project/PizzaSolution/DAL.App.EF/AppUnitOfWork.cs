using System;
using System.Collections.Generic;
using System.ComponentModel;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppUnitOfOfWork : EFBaseUnitOfWork<Guid, AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }
        
        public IAdditionalToppingRepository AdditionalToppings =>
            GetRepository<IAdditionalToppingRepository>(() => new AdditionalToppingRepository(UOWDbContext));
        
        public ICartRepository Carts =>
            GetRepository<ICartRepository>(() => new CartRepository(UOWDbContext));
        
        public ICrustRepository Crusts =>
            GetRepository<ICrustRepository>(() => new CrustRepository(UOWDbContext));
        
        public IDefaultToppingRepository DefaultToppings =>
            GetRepository<IDefaultToppingRepository>(() => new DefaultToppingRepository(UOWDbContext));
        
        public IDrinkRepository Drinks =>
            GetRepository<IDrinkRepository>(() => new DrinkRepository(UOWDbContext));
        
        public IDrinkInCartRepository DrinkInCarts =>
            GetRepository<IDrinkInCartRepository>(() => new DrinkInCartRepository(UOWDbContext));
        
        public IInvoiceRepository Invoices =>
            GetRepository<IInvoiceRepository>(() => new InvoiceRepository(UOWDbContext));
        
        public IInvoiceLineRepository InvoiceLines =>
            GetRepository<IInvoiceLineRepository>(() => new InvoiceLineRepository(UOWDbContext));
        
        public IPartyOrderRepository PartyOrders =>
            GetRepository<IPartyOrderRepository>(() => new PartyOrderRepository(UOWDbContext));
        
        public IPartyOrderInvoiceRepository PartyOrderInvoices =>
            GetRepository<IPartyOrderInvoiceRepository>(() => new PartyOrderInvoiceRepository(UOWDbContext));
        
        public IPizzaInCartRepository PizzaInCarts =>
            GetRepository<IPizzaInCartRepository>(() => new PizzaInCartRepository(UOWDbContext));
        
        public IPizzaTypeRepository PizzaTypes =>
            GetRepository<IPizzaTypeRepository>(() => new PizzaTypeRepository(UOWDbContext));
        
        public ISizeRepository Sizes =>
            GetRepository<ISizeRepository>(() => new SizeRepository(UOWDbContext));
        
        public IToppingRepository Toppings =>
            GetRepository<IToppingRepository>(() => new ToppingRepository(UOWDbContext));
        
        public ITransportRepository Transports =>
            GetRepository<ITransportRepository>(() => new TransportRepository(UOWDbContext));
        
        public IAppUserRepository AppUsers =>
            GetRepository<IAppUserRepository>(() => new AppUserRepository(UOWDbContext));
    }
}