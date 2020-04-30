using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IDefaultToppingRepositoryCustom: IDefaultToppingRepositoryCustom<DefaultTopping>
    {
    }

    public interface IDefaultToppingRepositoryCustom<TDefaultTopping>
    {
        Task<IEnumerable<TDefaultTopping>> GetAllAsync(Guid? userId = null, bool noTracking = true);
        Task<TDefaultTopping> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true);
    }
    
}