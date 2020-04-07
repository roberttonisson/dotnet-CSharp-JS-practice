using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;

namespace DAL.Base
{
    public abstract class DomainEntity : DomainEntity<Guid>
    {
    }

    public abstract class DomainEntity<TKey> : IDomainEntity<TKey>
        where TKey : struct, IComparable
    {
        public virtual TKey Id { get; set; }

        [Display(Name = nameof(CreatedBy), ResourceType = typeof(Resources.Domain.Shared))]
        public virtual string? CreatedBy { get; set; }

        [Display(Name = nameof(CreatedAt), ResourceType = typeof(Resources.Domain.Shared))]
        public virtual DateTime CreatedAt { get; set; }

        [Display(Name = nameof(ChangedBy), ResourceType = typeof(Resources.Domain.Shared))]
        public virtual string? ChangedBy { get; set; }
        
        [Display(Name = nameof(ChangedAt), ResourceType = typeof(Resources.Domain.Shared))]
        public virtual DateTime ChangedAt { get; set; }
    }
}