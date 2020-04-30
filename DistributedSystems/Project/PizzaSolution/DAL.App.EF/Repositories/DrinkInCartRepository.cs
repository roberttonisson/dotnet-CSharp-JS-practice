using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class DrinkInCartRepository :
        EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.DrinkInCart, DAL.App.DTO.DrinkInCart>,
        IDrinkInCartRepository
    {
        public DrinkInCartRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new BaseMapper<DrinkInCart, DTO.DrinkInCart>())
        {
        }

        public virtual async Task<IEnumerable<DTO.DrinkInCart>> GetAllAsync(Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(d => d.Cart!)
                    .ThenInclude(c => c.AppUser!)
                .Include(t => t.Drink!);
            if (userId != null)
            {
                query = query.Where(a => a.Cart!.AppUser!.Id == userId);
            }
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public virtual async Task<DTO.DrinkInCart> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(d => d.Cart!)
                    .ThenInclude(c => c.AppUser!)
                .Include(t => t.Drink!)
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