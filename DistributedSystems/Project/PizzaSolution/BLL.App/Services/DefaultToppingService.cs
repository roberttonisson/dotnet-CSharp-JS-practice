using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using ee.itcollege.rotoni.pizzaApp.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BLL.App.Services
{
    public class DefaultToppingService :
        BaseEntityService<IAppUnitOfWork, IDefaultToppingRepository, IDefaultToppingServiceMapper,
            DAL.App.DTO.DefaultTopping, BLL.App.DTO.DefaultTopping>, IDefaultToppingService
    {
        public DefaultToppingService(IAppUnitOfWork uow) : base(uow, uow.DefaultToppings, new DefaultToppingServiceMapper())
        {
        }

        public async Task<IEnumerable<DefaultTopping>> GetAllAsync(Guid? userId = null, bool noTracking = true)
        {
            return (await Repository.GetAllAsync(userId, noTracking)).Select(e => Mapper.Map(e));
        }
        
        public async Task<IEnumerable<DefaultTopping>> GetAllForPizzaAsync(Guid pizzaTypeId, Guid? userId = null, bool noTracking = true)
        {
            return (await Repository.GetAllForPizzaAsync(pizzaTypeId, userId, noTracking)).Select(e => Mapper.Map(e));
        }

        public async Task<DefaultTopping> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true)
        {
            return Mapper.Map(await Repository.FirstOrDefaultAsync(Id, userId));
        }
    }
}