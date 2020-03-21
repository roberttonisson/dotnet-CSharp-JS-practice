using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppRole : IdentityRole<Guid>
    {
        // fix the pk length
        public override Guid Id { get; set; } = default!;
    }
}