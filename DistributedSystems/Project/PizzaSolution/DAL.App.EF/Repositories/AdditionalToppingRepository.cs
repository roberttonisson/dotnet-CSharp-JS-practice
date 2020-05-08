using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Mappers;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class AdditionalToppingRepository :
        EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.AdditionalTopping, DAL.App.DTO.AdditionalTopping>,
        IAdditionalToppingRepository
    {
        public AdditionalToppingRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<AdditionalTopping, DTO.AdditionalTopping>())
        {
        }

        public virtual async Task<IEnumerable<DTO.AdditionalTopping>> GetAllAsync(Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(a => a.PizzaInCart!)
                    .ThenInclude(p => p.Cart!)
                        .ThenInclude(c => c.AppUser!)
                .Include(a => a.PizzaInCart!)
                    .ThenInclude(c => c.PizzaType!)
                .Include(a => a.PizzaInCart!)
                    .ThenInclude(c => c.Size!)
                .Include(a => a.PizzaInCart!)
                    .ThenInclude(c => c.Crust!)
                .Include(a => a.Topping!);
            if (userId != null)
            {
                query = query.Where(a => a.PizzaInCart!.Cart!.AppUser!.Id == userId);
            }
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public virtual async Task<DTO.AdditionalTopping> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(a => a.PizzaInCart!)
                    .ThenInclude(p => p.Cart!)
                        .ThenInclude(c => c.AppUser!)
                .Include(a => a.PizzaInCart!)
                    .ThenInclude(c => c.PizzaType!)
                .Include(a => a.PizzaInCart!)
                    .ThenInclude(c => c.Size!)
                .Include(a => a.PizzaInCart!)
                    .ThenInclude(c => c.Crust!)
                .Include(a => a.Topping!)
                .Where(x => x.Id == Id);
            if (userId != null)
            {
                query = query.Where(a => a.PizzaInCart!.Cart!.AppUser!.Id == userId);
            }
            var domainEntity = await query.FirstOrDefaultAsync();
            return Mapper.Map(domainEntity);

        }
    }
}