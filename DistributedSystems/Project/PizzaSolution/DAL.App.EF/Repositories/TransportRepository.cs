using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace DAL.App.EF.Repositories
{
    public class TransportRepository : BaseRepository<Transport>, ITransportRepository
    {
        public TransportRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<TransportDTO>> SelectAllDTO()
        {
            return await RepoDbSet.Select(t => new TransportDTO()
            {
                Id = t.Id, Address = t.Address, Cost = t.Cost
            }).ToListAsync();
        }
        
        public async Task<TransportDTO> SelectDTO(Guid id)
        {
            return await RepoDbSet.Select(t => new TransportDTO()
            {
                Id = t.Id, Address = t.Address, Cost = t.Cost
            }).FirstOrDefaultAsync(s => s.Id == id);
        }
        


    }
}