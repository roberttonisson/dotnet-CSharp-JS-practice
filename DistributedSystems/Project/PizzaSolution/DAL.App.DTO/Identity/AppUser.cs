﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.App.DTO;
using Microsoft.AspNetCore.Identity;

namespace DAL.App.DTO.Identity
{
    public class AppUser : IdentityUser<Guid>, IDomainEntityId
    {


        // add your own fields
        [MaxLength(128)] [MinLength(1)] public string FirstName { get; set; } = default!;

        [MaxLength(128)] [MinLength(1)] public string LastName { get; set; } = default!;

        [MaxLength(256)] [MinLength(1)] public string Address { get; set; } = default!;

        public ICollection<Cart>? Carts { get; set; }

        public ICollection<Invoice>? Invoices { get; set; }

        public ICollection<PartyOrder>? PartyOrders { get; set; }
        
        public virtual string? CreatedBy { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual string? ChangedBy { get; set; }
        public virtual DateTime ChangedAt { get; set; }
    }
}