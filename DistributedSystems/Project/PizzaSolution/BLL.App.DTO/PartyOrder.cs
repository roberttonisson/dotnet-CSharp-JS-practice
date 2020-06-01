using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


using Domain.Identity;
using ee.itcollege.rotoni.pizzaApp.DAL.Base;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BLL.App.DTO
{
    public class PartyOrder : DomainEntityIdMetadataUser<AppUser>
    {

        [Display(Name = nameof(Start), ResourceType = typeof(Resources.Domain.Shared))]
        public DateTime Start { get; set; } = DateTime.Now;
        [Display(Name = nameof(End), ResourceType = typeof(Resources.Domain.Shared))]
        public DateTime? End { get; set; }

        [Display(Name = nameof(Total), ResourceType = typeof(Resources.Domain.Shared))]
        [NotMapped] public decimal? Total { get; set; }

        [Display(Name = nameof(Address), ResourceType = typeof(Resources.Domain.Shared))]
        [MaxLength(256, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common))] [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common))] public string Address { get; set; } = default!;

        [Display(Name = nameof(InviteKey), ResourceType = typeof(Resources.Domain.Shared))]
        [MaxLength(8)] [MinLength(6)] public string InviteKey { get; set; } = Guid.NewGuid().ToString();

        public ICollection<PartyOrderInvoice>? PartyOrderInvoices { get; set; }
        
        public SelectList? AppUserSelectList { get; set; }
    }
}