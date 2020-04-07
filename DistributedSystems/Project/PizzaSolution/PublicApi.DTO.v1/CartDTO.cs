using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Identity;

namespace PublicApi.DTO.v1
{
    public class CartDTO
    {
        public Guid Id { get; set; }
        
        public Guid UserId { get; set; } = default!;

        public decimal? Total { get; set; }
    }
}