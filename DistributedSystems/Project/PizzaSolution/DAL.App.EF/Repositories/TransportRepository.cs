using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class TransportRepository : BaseRepository<Transport>, ITransportRepository
    {
        public TransportRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}