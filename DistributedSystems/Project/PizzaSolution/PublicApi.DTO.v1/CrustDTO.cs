using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain;
using ee.itcollege.rotoni.pizzaApp.DAL.Base;

namespace PublicApi.DTO.v1
{
    public class CrustDTO : DomainEntityId
    {


        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common))] [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common))] public string Name { get; set; } = default!;

        [Column(TypeName = "decimal(6,2)")]public decimal Price { get; set; } = default!;
        
    }
}