using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1
{
    public class PartyOrderDTO : DomainEntityId
    {
        public Guid AppUserId { get; set; } = default!;
        public AppUserDTO? AppUser { get; set; }
        
        public DateTime Start { get; set; } = DateTime.Now;

        public DateTime? End { get; set; }
        
        public decimal? Total { get; set; }
        
        [MaxLength(256)] [MinLength(1)] public string Address { get; set; } = default!;

        [MaxLength(8)] [MinLength(6)] public string InviteKey { get; set; } = Guid.NewGuid().ToString();

    }
}