using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IDefaultToppingRepository : IBaseRepository<DefaultTopping>
    {
        Task<IEnumerable<DefaultTopping>> GetIncluded();
        Task<DefaultTopping> FirstOrDefaultAsync(Guid id);
    }
}