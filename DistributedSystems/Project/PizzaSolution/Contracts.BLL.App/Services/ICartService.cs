using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;

using Contracts.DAL.App.Repositories;
using ee.itcollege.rotoni.pizzaApp.Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface ICartService : IBaseEntityService<Cart>, ICartRepositoryCustom<Cart>
    {
        Task<bool> RemoveFromCart(IAppBLL bll, Guid pizzaInCart, Guid drinkInCart,
            List<Guid>? additionalToppings = null);
        
        Task<bool> Pay(IAppBLL bll, Transport transport,  Guid userId);
    }
}