using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;

using DAL.App.EF.Mappers;


using Domain;
using ee.itcollege.rotoni.pizzaApp.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Drink = BLL.App.DTO.Drink;

namespace DAL.App.EF.Repositories
{
    public class NewProductRepository :
        EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.NewProduct, DAL.App.DTO.NewProduct>,
        INewProductRepository
    {
        public NewProductRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<NewProduct, DTO.NewProduct>())
        {
        }

        public virtual async Task<IEnumerable<DTO.NewProduct>> GetAllAsync(Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(i => i.PizzaType)
                .Where(i => i.IsActive);
            query = query.OrderBy(t => t.CreatedAt);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public virtual async Task<IEnumerable<DTO.NewProduct>> GetAllWithCollectionsAsync(Guid? userId = null,
            bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(i => i.PizzaType)
                .Where(i => i.IsActive);

            query = query.OrderByDescending(t => t.CreatedAt);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public virtual async Task<DTO.NewProduct> FirstOrDefaultAsync(Guid Id, Guid? userId = null,
            bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(i => i.PizzaType)
                .Where(i => i.IsActive)
                .Where(x => x.Id == Id);

            var domainEntity = await query.FirstOrDefaultAsync();
            return Mapper.Map(domainEntity);
        }

        public virtual async Task<DTO.NewProduct> FirstWithCollectionsAsync(Guid Id, Guid? userId = null,
            bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Where(d => d.Id == Id)
                .Include(i => i.PizzaType)
                .Where(i => i.IsActive);

            var domainEntity = await query.FirstOrDefaultAsync();
            return Mapper.Map(domainEntity);
        }
    }
}