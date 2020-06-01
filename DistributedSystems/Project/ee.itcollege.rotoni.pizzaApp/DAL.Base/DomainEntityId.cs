using System;
using ee.itcollege.rotoni.pizzaApp.Contracts.DAL.Base;

namespace ee.itcollege.rotoni.pizzaApp.DAL.Base
{
    public abstract class DomainEntityId : DomainEntityId<Guid>, IDomainEntityId
    {
        
    }

    public abstract class DomainEntityId<TKey> : IDomainEntityId<TKey> 
        where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; } = default!;
    }
}