using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PizzaRestaurantRepository : BaseRepository<PizzaRestaurant>, IPizzaRestaurantRepository
    {
        public PizzaRestaurantRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}