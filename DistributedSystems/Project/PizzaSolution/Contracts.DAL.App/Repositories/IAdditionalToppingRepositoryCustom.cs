using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IAdditionalToppingRepositoryCustom: IAdditionalToppingRepositoryCustom<AdditionalTopping>
    {
    }

    public interface IAdditionalToppingRepositoryCustom<TAdditionalTopping>
    {
        Task<IEnumerable<TAdditionalTopping>> GetAllAsync(Guid? userId = null, bool noTracking = true);
        Task<TAdditionalTopping> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true);
    }
    
}