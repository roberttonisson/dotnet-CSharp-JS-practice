using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
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

        public async Task<Cart> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true)
        {
            return Mapper.Map(await Repository.FirstOrDefaultAsync(Id, userId));
        }
    }
}