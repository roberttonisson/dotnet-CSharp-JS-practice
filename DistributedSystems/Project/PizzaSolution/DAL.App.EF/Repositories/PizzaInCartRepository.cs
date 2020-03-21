using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PizzaInCartRepository : BaseRepository<PizzaInCart>, IPizzaInCartRepository
    {
        public PizzaInCartRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}