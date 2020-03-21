using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PartyOrderRepository : BaseRepository<PartyOrder>, IPartyOrderRepository
    {
        public PartyOrderRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}