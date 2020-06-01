using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.rotoni.pizzaApp.Contracts.DAL.Base;
using Newtonsoft.Json;

namespace ee.itcollege.rotoni.pizzaApp.DAL.Base
{
    public abstract class DomainEntityIdMetadata : DomainEntityIdMetadata<Guid>, IDomainEntityId, IDomainEntityMetadata
    {
        
    }

    public abstract class DomainEntityIdMetadata<TKey> : DomainEntityId<TKey> 
        where TKey : IEquatable<TKey>
    {
        [MaxLength(256)]
        [JsonIgnore]
        public string? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }

        [MaxLength(256)]
        [JsonIgnore]
        public string? ChangedBy { get; set; }
        [JsonIgnore]
        public DateTime ChangedAt { get; set; }
    }
}