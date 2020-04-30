using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;
using DAL.Base;

namespace DAL.App.DTO
{
    public class PartyOrder : DomainEntityIdMetadataUser<Domain.Identity.AppUser>
    {

        [Display(Name = nameof(Start), ResourceType = typeof(Resources.Domain.Shared))]
        public DateTime Start { get; set; } = DateTime.Now;
        [Display(Name = nameof(End), ResourceType = typeof(Resources.Domain.Shared))]
        public DateTime? End { get; set; }

        [Display(Name = nameof(Total), ResourceType = typeof(Resources.Domain.Shared))]
        [NotMapped] public decimal? Total { get; set; }

        [Display(Name = nameof(Address), ResourceType = typeof(Resources.Domain.Shared))]
        [MaxLength(256)] [MinLength(1)] public string Address { get; set; } = default!;

        [Display(Name = nameof(InviteKey), ResourceType = typeof(Resources.Domain.Shared))]
        [MaxLength(8)] [MinLength(6)] public string InviteKey { get; set; } = Guid.NewGuid().ToString();

        public ICollection<PartyOrderInvoice>? PartyOrderInvoices { get; set; }
    }
}