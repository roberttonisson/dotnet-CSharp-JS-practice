using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ToppingRepository : BaseRepository<Topping>, IToppingRepository
    {
        public ToppingRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}