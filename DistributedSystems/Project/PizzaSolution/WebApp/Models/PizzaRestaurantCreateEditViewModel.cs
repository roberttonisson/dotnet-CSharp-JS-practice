using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class PizzaRestaurantCreateEditViewModel
    {
        public PizzaRestaurant PizzaRestaurant { get; set; } = default!;

        public SelectList? PizzaRestaurantSelectList { get; set; }
    }
}