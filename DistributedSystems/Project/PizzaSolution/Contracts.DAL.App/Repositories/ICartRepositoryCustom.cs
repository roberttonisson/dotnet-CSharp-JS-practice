using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICartRepositoryCustom: ICartRepositoryCustom<Cart>
    {
    }

    public interface ICartRepositoryCustom<TCart>
    {
        Task<IEnumerable<TCart>> GetAllAsync(Guid? userId = null, bool noTracking = true);
        Task<TCart> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true);
    }
    
}