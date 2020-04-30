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
    public class DrinkInCartService :
        BaseEntityService<IAppUnitOfWork, IDrinkInCartRepository, IDrinkInCartServiceMapper,
            DAL.App.DTO.DrinkInCart, BLL.App.DTO.DrinkInCart>, IDrinkInCartService
    {
        public DrinkInCartService(IAppUnitOfWork uow) : base(uow, uow.DrinkInCarts, new DrinkInCartServiceMapper())
        {
        }

        public async Task<IEnumerable<DrinkInCart>> GetAllAsync(Guid? userId = null, bool noTracking = true)
        {
            return (await Repository.GetAllAsync(userId, noTracking)).Select(e => Mapper.Map(e));
        }

        public async Task<DrinkInCart> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true)
        {
            return Mapper.Map(await Repository.FirstOrDefaultAsync(Id, userId));
        }
    }
}