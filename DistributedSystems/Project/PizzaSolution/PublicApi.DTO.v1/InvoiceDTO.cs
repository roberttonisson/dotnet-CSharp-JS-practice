using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain;
using Domain.Identity;
using ee.itcollege.rotoni.pizzaApp.DAL.Base;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1
{
    public class InvoiceDTO : DomainEntityId
    {
        public Guid AppUserId { get; set; } = default!;
        public AppUserDTO? AppUser { get; set; }

        public decimal? Total { get; set; }

        public bool IsPaid { get; set; } = false;

        public DateTime? CreatedAt { get; set; }

        public Guid TransportId { get; set; } = default!;
        public TransportDTO? Transport { get; set; }

        public Guid OrderStatusId { get; set; } = default!;
        public OrderStatusDTO? OrderStatus { get; set; }

        public ICollection<InvoiceLineDTO>? InvoiceLines { get; set; }
    }
}