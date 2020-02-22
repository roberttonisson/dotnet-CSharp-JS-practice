using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class DefaultTopping : DomainEntityMetadata
    {
        [MaxLength(36)]
        public string ToppingId { get; set; } = default!;
        public Topping? Topping { get; set; }

        [MaxLength(36)] public string PizzaTypeId { get; set; } = default!;
        public PizzaType? PizzaType { get; set; }

    }
}