using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using Contracts.BLL.App;
using ee.itcollege.rotoni.pizzaApp.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class InvoiceService :
        BaseEntityService<IAppUnitOfWork, IInvoiceRepository, IInvoiceServiceMapper,
            DAL.App.DTO.Invoice, BLL.App.DTO.Invoice>, IInvoiceService
    {
        public InvoiceService(IAppUnitOfWork uow) : base(uow, uow.Invoices, new InvoiceServiceMapper())
        {
        }

        public async Task<IEnumerable<Invoice>> GetAllAsync(Guid? userId = null, bool noTracking = true)
        {
            return (await Repository.GetAllAsync(userId, noTracking)).Select(e => Mapper.Map(e));
        }

        public async Task<IEnumerable<Invoice>> GetAllWithCollectionsAsync(Guid? userId = null, bool noTracking = true)
        {
            return (await Repository.GetAllWithCollectionsAsync(userId, noTracking)).Select(e => Mapper.Map(e));
        }
        
        public async Task<Invoice> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true)
        {
            return Mapper.Map(await Repository.FirstOrDefaultAsync(Id, userId));
        }

        public async Task<bool> ReOrder(IAppBLL bll, Guid invoiceId, Guid? userId)
        {
            Invoice invoice = new Invoice();
            if (invoiceId != null)
            {
                invoice = await bll.Invoices.FirstWithCollectionsAsync(invoiceId, userId);
            }

            if (invoice == null)
            {
                return false;
            }

            var cartId = bll.Carts.GetActiveCartWithoutCollections(userId).Result.Id;
            foreach (var line in invoice.InvoiceLines!)
            {
                if (line.DrinkInCart != null)
                {
                    if (bll.Drinks.ExistsAsync(line.DrinkInCart.DrinkId).Result)
                    {
                        bll.DrinkInCarts.Add(new DrinkInCart()
                        {
                            CartId = cartId,
                            DrinkId = line.DrinkInCart.Drink!.Id,
                            Quantity = line.DrinkInCart.Quantity
                        });
                    }
                    
                }

                if (line.PizzaInCart != null)
                {
                    var x = line.PizzaInCart;
                    if (bll.Sizes.ExistsAsync(x.SizeId).Result 
                        && bll.Crusts.ExistsAsync(x.CrustId).Result 
                        && bll.PizzaTypes.ExistsAsync(x.PizzaTypeId).Result
                    )
                    {
                        var pic = new PizzaInCart
                        {
                            Quantity = x.Quantity,
                            PizzaTypeId = x.PizzaTypeId,
                            CrustId = x.CrustId,
                            SizeId = x.SizeId,
                            CartId = cartId
                        };
                        bll.PizzaInCarts.Add(pic);
                        await bll.SaveChangesAsync();
                        if (x.AdditionalToppings != null)
                        {
                            foreach (var additional in x.AdditionalToppings)
                            {
                                bll.AdditionalToppings.Add(new AdditionalTopping()
                                {
                                    ToppingId = additional.ToppingId,
                                    PizzaInCartId = pic.Id
                                });
                                
                            }
                        }
                    }
                }
            }
            await bll.SaveChangesAsync();
            return true;
        }

        public async Task<Invoice> FirstWithCollectionsAsync(Guid Id, Guid? userId = null, bool noTracking = true)
        {
            return Mapper.Map(await Repository.FirstWithCollectionsAsync(Id, userId));
        }
        
    }
}