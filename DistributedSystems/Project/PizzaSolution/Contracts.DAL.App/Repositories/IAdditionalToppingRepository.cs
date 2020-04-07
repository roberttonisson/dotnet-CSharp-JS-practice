using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IAdditionalToppingRepository : IBaseRepository<AdditionalTopping>
    {
        Task<IEnumerable<AdditionalTopping>> GetIncluded(Guid? userId = null);
        Task<AdditionalTopping> FirstOrDefaultAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
    }
}