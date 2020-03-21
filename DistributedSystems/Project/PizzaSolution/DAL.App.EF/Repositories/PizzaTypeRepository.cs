using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PizzaTypeRepository : BaseRepository<PizzaType>, IPizzaTypeRepository
    {
        public PizzaTypeRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}