using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;
using Domain;
using Domain.Identity;
using PublicApi.DTO.v1.Identity;
using AppUser = BLL.App.DTO.Identity.AppUser;

namespace PublicApi.DTO.v1
{
    public class CartDTO : DomainEntityId
    {
        public decimal? Total { get; set; }

        public Guid AppUserId { get; set; } = default!;
        public AppUserDTO? AppUser { get; set; }
        
    }
}