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
    public class PizzaInCartRepository : BaseRepository<PizzaInCart>, IPizzaInCartRepository
    {
        public PizzaInCartRepository(DbContext dbContext) : base(dbContext)
        {
            
        }
        public async Task<IEnumerable<PizzaInCart>> GetIncluded(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(d => d.Cart)
                    .ThenInclude(u => u.AppUser)
                .Include(p => p.PizzaType)
                .Include(c => c.Crust)
                .Include(s => s.Size)
                .AsQueryable();
            if (userId != null)
            {
                query = query.Where(x => x.Cart.AppUser.Id == userId);
            }

            return await query.ToListAsync();
        }
        
        public async Task<PizzaInCart> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(d => d.Cart)
                    .ThenInclude(u => u.AppUser)
                .Include(p => p.PizzaType)
                .Include(c => c.Crust)
                .Include(s => s.Size)
                .Where(x => x.Id == id)
                .AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.Cart.AppUser.Id == userId);
            }

            return await query.FirstOrDefaultAsync();
        }
        
        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var pizzaInCart = await FirstOrDefaultAsync(id, userId);
            base.Remove(pizzaInCart);
        }
    }
}