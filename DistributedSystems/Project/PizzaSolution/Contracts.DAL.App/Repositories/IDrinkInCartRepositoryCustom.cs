using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IDrinkInCartRepositoryCustom: IDrinkInCartRepositoryCustom<DrinkInCart>
    {
    }

    public interface IDrinkInCartRepositoryCustom<TDrinkInCart>
    {
        Task<IEnumerable<TDrinkInCart>> GetAllAsync( Guid? userId = null, bool noTracking = true);
        Task<TDrinkInCart> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true);
    }
    
}