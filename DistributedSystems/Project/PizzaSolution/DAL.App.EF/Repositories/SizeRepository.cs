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
    public class SizeRepository : BaseRepository<Size>, ISizeRepository
    {
        public SizeRepository(DbContext dbContext) : base(dbContext)
        {
        }
        
        public async Task<IEnumerable<SizeDTO>> SelectAllDTO()
        {
            return await RepoDbSet.Select(s => new SizeDTO()
            {
                Id = s.Id, Name = s.Name, Price = s.Price, SizeCm = s.SizeCm
            }).ToListAsync();
        }
        
        public async Task<SizeDTO> SelectDTO(Guid id)
        {
            return await RepoDbSet.Select(s => new SizeDTO()
            {
                Id = s.Id, Name = s.Name, Price = s.Price, SizeCm = s.SizeCm
            }).FirstOrDefaultAsync(t => t.Id == id);
        }
        
    
    }
}