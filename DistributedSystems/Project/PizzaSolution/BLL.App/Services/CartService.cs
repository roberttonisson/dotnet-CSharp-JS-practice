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
    public class CartService :
        BaseEntityService<IAppUnitOfWork, ICartRepository, ICartServiceMapper,
            DAL.App.DTO.Cart, BLL.App.DTO.Cart>, ICartService
    {
        public CartService(IAppUnitOfWork uow) : base(uow, uow.Carts, new CartServiceMapper())
        {
        }

        public async Task<IEnumerable<Cart>> GetAllAsync(Guid? userId = null, bool noTracking = true)
        {
            return (await Repository.GetAllAsync(userId, noTracking)).Select(e => Mapper.Map(e));
        }
        
        public async Task<IEnumerable<Cart>> GetAllWithCollectionsAsync(Guid? userId = null, bool noTracking = true)
        {
            return (await Repository.GetAllWithCollectionsAsync(userId, noTracking)).Select(e => Mapper.Map(e));
        }
        
        public async Task<Cart> GetActiveCart(Guid? userId = null, bool noTracking = true)
        {
            return Mapper.Map(await Repository.GetActiveCart(userId));;
        }
        
        public async Task<Cart> GetActiveCartWithoutCollections(Guid? userId = null, bool noTracking = true)
        {
            return Mapper.Map(await Repository.GetActiveCartWithoutCollections(userId));;
        }
        
        public async Task<Cart> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true)
        {
            return Mapper.Map(await Repository.FirstOrDefaultAsync(Id, userId));
        }

        public async Task<bool> RemoveFromCart(IAppBLL bll, Guid pizzaInCart, Guid drinkInCart, List<Guid>? additionalToppings = null)
        {
            if (additionalToppings != null)
            {
                foreach (var additional in additionalToppings)
                {
                    await bll.AdditionalToppings.RemoveAsync(additional);
                }
            }
            await bll.SaveChangesAsync();
            
            if (pizzaInCart.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                await bll.PizzaInCarts.RemoveAsync(pizzaInCart);
                await bll.SaveChangesAsync();
            }

            if (drinkInCart.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                await bll.DrinkInCarts.RemoveAsync(drinkInCart);
                await bll.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> Pay(IAppBLL bll, Transport transport, Guid userId)
        {
            bll.Transports.Add(transport);
            await bll.SaveChangesAsync();

            var cart = bll.Carts.GetActiveCart(userId).Result;

            var invoice = new Invoice()
            {
                AppUserId = userId,
                Estimated = DateTime.Now.AddHours(1),
                OrderStatusId = Guid.Parse("c2c9026a-b9d2-4279-6453-08d7ffe78ef8"),
                IsPaid = true,
                TransportId = transport.Id
            };
            bll.Invoices.Add(invoice);
            await bll.SaveChangesAsync();

            if (cart.DrinkInCarts != null)
            {
                foreach (var dic in cart.DrinkInCarts)
                {
                    bll.InvoiceLines.Add(new InvoiceLine()
                    {
                        DrinkInCartId = dic.Id,
                        PizzaInCartId = null,
                        InvoiceId = invoice.Id,
                        Quantity = dic.Quantity
                    });
                }
            }
            if (cart.PizzaInCarts != null)
            {
                foreach (var pic in cart.PizzaInCarts)
                {
                    bll.InvoiceLines.Add(new InvoiceLine()
                    {
                        DrinkInCartId = null,
                        PizzaInCartId = pic.Id,
                        InvoiceId = invoice.Id,
                        Quantity = pic.Quantity
                    });
                }
            }

            var x = bll.Carts.GetActiveCartWithoutCollections().Result;
            x.Active = false;
            await bll.Carts.UpdateAsync(x);
            await bll.SaveChangesAsync();
            
            bll.Carts.Add(new Cart()
            {
                Active = true,
                AppUserId = userId,
            });
            await bll.SaveChangesAsync();

            return true;
        }
    }
}