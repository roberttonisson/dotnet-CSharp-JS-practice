using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPartyOrderRepositoryCustom: IPartyOrderRepositoryCustom<PartyOrder>
    {
    }

    public interface IPartyOrderRepositoryCustom<TPartyOrder>
    {
        Task<IEnumerable<TPartyOrder>> GetAllAsync(Guid? userId = null, bool noTracking = true);
        Task<TPartyOrder> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true);
    }
    
}