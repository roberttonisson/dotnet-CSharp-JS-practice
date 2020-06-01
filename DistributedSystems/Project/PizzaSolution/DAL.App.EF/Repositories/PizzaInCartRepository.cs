using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;

using DAL.App.EF.Mappers;


using Domain;
using ee.itcollege.rotoni.pizzaApp.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PizzaInCartRepository :
        EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.PizzaInCart, DAL.App.DTO.PizzaInCart>,
        IPizzaInCartRepository
    {
        public PizzaInCartRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<PizzaInCart, DTO.PizzaInCart>())
        {
        }

        public virtual async Task<IEnumerable<DTO.PizzaInCart>> GetAllAsync(Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(d => d.Cart!)
                    .ThenInclude(u => u.AppUser!)
                .Include(p => p.PizzaType!)
                .Include(c => c.Crust!)
                .Include(s => s.Size!);
            if (userId != null)
            {
                query = query.Where(a => a.Cart!.AppUser!.Id == userId);
            }
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public virtual async Task<DTO.PizzaInCart> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(d => d.Cart!)
                    .ThenInclude(u => u.AppUser!)
                .Include(p => p.PizzaType!)
                .Include(c => c.Crust!)
                .Include(s => s.Size!)
                .Where(x => x.Id == Id);
            if (userId != null)
            {
                query = query.Where(a => a.Cart!.AppUser!.Id == userId);
            }
            var domainEntity = await query.FirstOrDefaultAsync();
            return Mapper.Map(domainEntity);

        }
        
    }
}