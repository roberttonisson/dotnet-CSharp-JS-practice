using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IPizzaInCartRepository : IBaseRepository<PizzaInCart>
    {
        Task<IEnumerable<PizzaInCart>> GetIncluded(Guid? userId = null);
        Task<PizzaInCart> FirstOrDefaultAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
    }
}