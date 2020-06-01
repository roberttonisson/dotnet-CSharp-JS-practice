using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.rotoni.pizzaApp.DAL.Base;


namespace Domain
{
    public class Drink : DomainEntityIdMetadata
    {
        [Display(Name = nameof(Name), ResourceType = typeof(Resources.Domain.Shared))]
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength",
            ErrorMessageResourceType = typeof(Resources.Common))]
        [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength",
            ErrorMessageResourceType = typeof(Resources.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public string Name { get; set; } = default!;

        [Display(Name = nameof(Price), ResourceType = typeof(Resources.Domain.Shared))]
        [Column(TypeName = "decimal(6,2)")]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required",
            ErrorMessageResourceType = typeof(Resources.Common))]
        public decimal Price { get; set; } = default!;

        [Display(Name = nameof(Size), ResourceType = typeof(Resources.Domain.Shared))]
        [Column(TypeName = "decimal(4,3)")]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required",
            ErrorMessageResourceType = typeof(Resources.Common))]
        public decimal Size { get; set; } = default!;

        [MaxLength(1024)]
        [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength",
            ErrorMessageResourceType = typeof(Resources.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public string ImageUrl { get; set; } = default!;

        public ICollection<DrinkInCart>? DrinkInCarts { get; set; }
    }
}