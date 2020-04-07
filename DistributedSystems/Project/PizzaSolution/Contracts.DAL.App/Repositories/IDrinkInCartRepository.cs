using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IDrinkInCartRepository : IBaseRepository<DrinkInCart>
    {
        Task<IEnumerable<DrinkInCart>> GetIncluded(Guid? userId = null);
        Task<DrinkInCart> FirstOrDefaultAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
    }
    
    
}