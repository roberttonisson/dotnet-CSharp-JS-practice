using System;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.rotoni.pizzaApp.Contracts.DAL.Base;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace ee.itcollege.rotoni.pizzaApp.DAL.Base
{
    public abstract class DomainEntityIdMetadataUser<TUser> : DomainEntityIdMetadataUser<Guid, TUser>, IDomainEntityUser<TUser>, IDomainEntityMetadata
        where TUser : IdentityUser<Guid>
    {
    }

    public abstract class DomainEntityIdMetadataUser<TKey, TUser> : DomainEntityIdMetadata<TKey>,
        IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser: IdentityUser<TKey>
    {
        [ForeignKey("AppUser")]
        public TKey AppUserId { get; set; } = default!;

        [JsonIgnore] public TUser? AppUser { get; set; }
    }
}