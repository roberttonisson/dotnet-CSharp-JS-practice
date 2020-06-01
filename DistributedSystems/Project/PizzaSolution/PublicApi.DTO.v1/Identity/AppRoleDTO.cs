using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.rotoni.pizzaApp.Contracts.DAL.Base;


namespace PublicApi.DTO.v1.Identity
{
    public class AppRoleDTO : IDomainEntityId 
    {
        public Guid Id { get; set; }
        
        [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [MaxLength(256, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [Required]
        public string DisplayName { get; set; } = default!;
        
    }
}