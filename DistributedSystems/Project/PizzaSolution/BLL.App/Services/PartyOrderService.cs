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
    public class PartyOrderService :
        BaseEntityService<IAppUnitOfWork, IPartyOrderRepository, IPartyOrderServiceMapper,
            DAL.App.DTO.PartyOrder, BLL.App.DTO.PartyOrder>, IPartyOrderService
    {
        public PartyOrderService(IAppUnitOfWork uow) : base(uow, uow.PartyOrders, new PartyOrderServiceMapper())
        {
        }

        public async Task<IEnumerable<PartyOrder>> GetAllAsync(Guid? userId = null, bool noTracking = true)
        {
            return (await Repository.GetAllAsync(userId, noTracking)).Select(e => Mapper.Map(e));
        }

        public async Task<PartyOrder> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true)
        {
            return Mapper.Map(await Repository.FirstOrDefaultAsync(Id, userId));
        }
    }
}