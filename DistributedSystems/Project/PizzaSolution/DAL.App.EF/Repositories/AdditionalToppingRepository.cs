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
    public class AdditionalToppingRepository : BaseRepository<AdditionalTopping>, IAdditionalToppingRepository
    {
        public AdditionalToppingRepository(DbContext dbContext) : base(dbContext)
        {
            
        }
        
        public async Task<IEnumerable<AdditionalTopping>> GetIncluded(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(a => a.PizzaInCart)
                    .ThenInclude(p => p.Cart)
                        .ThenInclude(c => c.AppUser)
                .Include(a => a.PizzaInCart)
                    .ThenInclude(c => c.PizzaType)
                .Include(a => a.Topping)
                .AsQueryable();
            if (userId != null)
            {
                query = query.Where(x => x.PizzaInCart.Cart.AppUser.Id == userId);
            }

            return await query.ToListAsync();
        }

        public async Task<AdditionalTopping> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(a => a.PizzaInCart)
                    .ThenInclude(p => p.Cart)
                        .ThenInclude(c => c.AppUser)
                .Include(a => a.PizzaInCart)
                    .ThenInclude(c => c.PizzaType)
                .Include(a => a.Topping)
                .Where(x => x.Id == id)
                .AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.PizzaInCart.Cart.AppUser.Id == userId);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var additionalTopping = await FirstOrDefaultAsync(id, userId);
            base.Remove(additionalTopping);
        }
    }
}