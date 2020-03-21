using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CrustRepository : BaseRepository<Crust>, ICrustRepository
    {
        public CrustRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}