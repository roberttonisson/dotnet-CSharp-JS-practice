using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public override Guid Id { get; set; } = default!;

        // add your own fields
        [MaxLength(128)] [MinLength(1)] public string FirstName { get; set; } = default!;

        [MaxLength(128)] [MinLength(1)] public string LastName { get; set; } = default!;

        [MaxLength(256)] [MinLength(1)] public string Address { get; set; } = default!;

        public ICollection<Cart>? Carts { get; set; }

        public ICollection<Invoice>? Invoices { get; set; }

        public ICollection<PartyOrder>? PartyOrders { get; set; }
    }
}