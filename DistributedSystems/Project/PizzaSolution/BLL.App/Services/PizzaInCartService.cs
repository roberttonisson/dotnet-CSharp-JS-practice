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
    public class PizzaInCartService :
        BaseEntityService<IAppUnitOfWork, IPizzaInCartRepository, IPizzaInCartServiceMapper,
            DAL.App.DTO.PizzaInCart, BLL.App.DTO.PizzaInCart>, IPizzaInCartService
    {
        public PizzaInCartService(IAppUnitOfWork uow) : base(uow, uow.PizzaInCarts, new PizzaInCartServiceMapper())
        {
        }

        public async Task<IEnumerable<PizzaInCart>> GetAllAsync(Guid? userId = null, bool noTracking = true)
        {
            return (await Repository.GetAllAsync(userId, noTracking)).Select(e => Mapper.Map(e));
        }

        public async Task<PizzaInCart> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true)
        {
            return Mapper.Map(await Repository.FirstOrDefaultAsync(Id, userId));
        }
    }
}