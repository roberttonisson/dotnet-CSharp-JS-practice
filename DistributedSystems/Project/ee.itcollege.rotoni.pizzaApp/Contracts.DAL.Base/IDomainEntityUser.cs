using System;
using Microsoft.AspNetCore.Identity;

namespace ee.itcollege.rotoni.pizzaApp.Contracts.DAL.Base
{
    public interface IDomainEntityUser<TUser> : IDomainEntityUser<Guid, TUser>
        where TUser : IdentityUser<Guid>
    {
    }

    public interface IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey>
    {
        public TKey AppUserId { get; set; }
        public TUser? AppUser { get; set; }
    }
}