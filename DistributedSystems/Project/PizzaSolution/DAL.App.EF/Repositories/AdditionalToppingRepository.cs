using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class AdditionalToppingRepository : BaseRepository<AdditionalTopping>, IAdditionalToppingRepository
    {
        public AdditionalToppingRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}