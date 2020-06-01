using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.rotoni.pizzaApp.Contracts.DAL.Base;


namespace PublicApi.DTO.v1
{
    public class NewProductDTO : IDomainEntityId
    {
        
        public bool IsActive { get; set; }
        
        [MaxLength(512)] [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common))]
        public string Description { get; set; } = default!;
        
        public Guid PizzaTypeId { get; set; } = default!;
        public PizzaTypeDTO? PizzaType { get; set; }
        
        public Guid Id { get; set; }
    }
}