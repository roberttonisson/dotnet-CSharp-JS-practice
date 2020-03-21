using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class PartyOrder : DomainEntity
    {
        public DateTime Start { get; set; } = DateTime.Now;
        public DateTime? End { get; set; }

        [NotMapped] public decimal Total { get; set; }

        [MaxLength(256)] [MinLength(1)] public string Address { get; set; } = default!;

        [MaxLength(8)] [MinLength(6)] public string InviteKey { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey(nameof(Owner))] public Guid OwnerId { get; set; } = default!;
        public AppUser? Owner { get; set; }

        public ICollection<PartyOrderInvoice>? PartyOrderInvoices { get; set; }
    }
}