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
    public class DrinkInCartRepository : BaseRepository<DrinkInCart>, IDrinkInCartRepository
    {
        public DrinkInCartRepository(DbContext dbContext) : base(dbContext)
        {
            
        }
        public async Task<IEnumerable<DrinkInCart>> GetIncluded(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(d => d.Cart!)
                    .ThenInclude(c => c.AppUser!)
                .Include(t => t.Drink!)
                .AsQueryable();
            if (userId != null)
            {
                query = query.Where(x => x.Cart!.AppUser!.Id == userId);
            }

            return await query.ToListAsync();
        }
        
        public async Task<DrinkInCart> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(d => d.Cart!)
                    .ThenInclude(c => c.AppUser!)
                .Include(t => t.Drink!)
                .Where(x => x.Id == id)
                .AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.Cart!.AppUser!.Id == userId);
            }

            return await query.FirstOrDefaultAsync();
        }
        
        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var drinkInCart = await FirstOrDefaultAsync(id, userId);
            base.Remove(drinkInCart);
        }
    }
}