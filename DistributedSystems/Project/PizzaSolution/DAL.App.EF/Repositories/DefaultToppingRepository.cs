using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class DefaultToppingRepository : BaseRepository<DefaultTopping>, IDefaultToppingRepository
    {
        public DefaultToppingRepository(DbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<DefaultTopping>> GetIncluded()
        {
            var query = RepoDbSet
                .Include(d => d.Topping)
                .Include(t => t.PizzaType)
                .AsQueryable();
            
            return await query.ToListAsync();
        }
        
        
        public async Task<DefaultTopping> FirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet
                .Include(d => d.Topping)
                .Include(t => t.PizzaType)
                .Where(x => x.Id == id)
                .AsQueryable();

            return await query.FirstOrDefaultAsync();
        }
        
    }
}