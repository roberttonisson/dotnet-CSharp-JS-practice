using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.rotoni.pizzaApp.Contracts.DAL.Base;


namespace PublicApi.DTO.v1
{
    public class OrderStatusDTO : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(256, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common))] [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common))] public string Status { get; set; } = default!;
    }
}