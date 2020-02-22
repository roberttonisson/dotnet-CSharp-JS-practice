using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class AdditionalTopping : DomainEntityMetadata
    {
        [MaxLength(36)] public string ToppingId { get; set; } = default!;
        public Topping? Topping { get; set; }

        [MaxLength(36)] public string PizzaInCartId { get; set; } = default!;
        public PizzaInCart? PizzaInCart { get; set; }
    }
}