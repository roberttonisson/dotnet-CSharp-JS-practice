using System;
using ee.itcollege.rotoni.pizzaApp.Contracts.DAL.Base;
using ee.itcollege.rotoni.pizzaApp.Contracts.DAL.Base.Repositories;

namespace ee.itcollege.rotoni.pizzaApp.Contracts.BLL.Base.Services
{
    public interface IBaseEntityService<TEntity> : IBaseEntityService<Guid, TEntity>
        where TEntity : class, IDomainEntityId<Guid>, new()
    {
    }

    public interface IBaseEntityService<in TKey, TEntity> : IBaseService, IBaseRepository<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IDomainEntityId<TKey>, new()
    {
        
    }

}