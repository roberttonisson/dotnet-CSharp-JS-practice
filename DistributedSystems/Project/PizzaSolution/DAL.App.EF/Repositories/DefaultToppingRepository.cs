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
    public class DefaultToppingRepository :
        EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.DefaultTopping, DAL.App.DTO.DefaultTopping>,
        IDefaultToppingRepository
    {
        public DefaultToppingRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<DefaultTopping, DTO.DefaultTopping>())
        {
        }

        public virtual async Task<IEnumerable<DTO.DefaultTopping>> GetAllAsync(Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(d => d.Topping!)
                .Include(t => t.PizzaType!);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }
        
        public virtual async Task<IEnumerable<DTO.DefaultTopping>> GetAllForPizzaAsync(Guid pizzaTypeId, Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Where(x=> x.PizzaTypeId == pizzaTypeId)
                .Include(d => d.Topping!)
                .Include(t => t.PizzaType!);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public virtual async Task<DTO.DefaultTopping> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(d => d.Topping)
                .Include(t => t.PizzaType)
                .Where(x => x.Id == Id);
            var domainEntity = await query.FirstOrDefaultAsync();
            return Mapper.Map(domainEntity);

        }
    }
}