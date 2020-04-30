using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPizzaInCartRepositoryCustom: IPizzaInCartRepositoryCustom<PizzaInCart>
    {
    }

    public interface IPizzaInCartRepositoryCustom<TPizzaInCart>
    {
        Task<IEnumerable<TPizzaInCart>> GetAllAsync( Guid? userId = null, bool noTracking = true);
        Task<TPizzaInCart> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true);
    }
    
}