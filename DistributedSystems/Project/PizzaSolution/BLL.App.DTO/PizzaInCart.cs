﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BLL.App.DTO
{
    public class PizzaInCart : DomainEntityIdMetadata
    {
        [Display(Name = nameof(Price), ResourceType = typeof(Resources.Domain.Shared))]
        [NotMapped] public decimal? Price { get; set; }

        [Display(Name = nameof(Quantity), ResourceType = typeof(Resources.Domain.Shared))]
        public int Quantity { get; set; } = default;

        public Guid PizzaTypeId { get; set; } = default!;
        public Domain.PizzaType? PizzaType { get; set; }

        public Guid CrustId { get; set; } = default!;
        public Domain.Crust? Crust { get; set; }

        public Guid SizeId { get; set; } = default!;
        public Domain.Size? Size { get; set; }

        public Guid CartId { get; set; } = default!;
        public Domain.Cart? Cart { get; set; }

        public ICollection<InvoiceLine>? InvoiceLines { get; set; }
        
        public SelectList? PizzaTypeSelectList { get; set; }
        public SelectList? CrustSelectList { get; set; }
        public SelectList? SizeSelectList { get; set; }
        public SelectList? CartSelectList { get; set; }
    }
}