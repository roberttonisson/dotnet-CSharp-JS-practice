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
    public class ToppingRepository : BaseRepository<Topping>, IToppingRepository
    {
        public ToppingRepository(DbContext dbContext) : base(dbContext)
        {
        }
        
        public async Task<IEnumerable<ToppingDTO>> SelectAllDTO()
        {
            return await RepoDbSet.Select(t => new ToppingDTO()
            {
                Id = t.Id, Name = t.Name, Price = t.Price
            }).ToListAsync();
        }
        
        public async Task<ToppingDTO> SelectDTO()
        {
            return await RepoDbSet.Select(t => new ToppingDTO()
            {
                Id = t.Id, Name = t.Name, Price = t.Price
            }).FirstOrDefaultAsync();
        }
    }
}