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
    public class CartRepository :
        EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.Cart, DAL.App.DTO.Cart>,
        ICartRepository
    {
        public CartRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Cart, DTO.Cart>())
        {
        }

        public virtual async Task<IEnumerable<DTO.Cart>> GetAllAsync(Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(c => c.AppUser!);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }
        
        public virtual async Task<IEnumerable<DTO.Cart>> GetAllWithCollectionsAsync(Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(c => c.DrinkInCarts)
                    .ThenInclude(d => d.Drink)
                .Include(c => c.PizzaInCarts)
                    .ThenInclude(d => d.Crust)
                .Include(c => c.PizzaInCarts)
                    .ThenInclude(d => d.Size)
                .Include(c => c.PizzaInCarts)
                    .ThenInclude(d => d.PizzaType)
                .Include(c => c.PizzaInCarts)
                    .ThenInclude(d => d.AdditionalToppings)
                        .ThenInclude(d => d.Topping)
                .Include(c => c.PizzaInCarts)
                    .ThenInclude(d => d.AdditionalToppings)
                        .ThenInclude(d => d.Topping)
                .Include(c => c.AppUser!);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }
        
        public virtual async Task<DTO.Cart> GetActiveCart(Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Where(a => a.Active == true)
                .Include(c => c.DrinkInCarts)
                    .ThenInclude(d => d.Drink)
                .Include(c => c.PizzaInCarts)
                    .ThenInclude(d => d.Crust)
                .Include(c => c.PizzaInCarts)
                    .ThenInclude(d => d.Size)
                .Include(c => c.PizzaInCarts)
                    .ThenInclude(d => d.PizzaType)
                .Include(c => c.PizzaInCarts)
                    .ThenInclude(d => d.AdditionalToppings)
                        .ThenInclude(d => d.Topping)
                .Include(c => c.PizzaInCarts)
                    .ThenInclude(d => d.AdditionalToppings)
                        .ThenInclude(d => d.Topping)
                .Include(c => c.AppUser!);
            var domainEntities = await query.FirstOrDefaultAsync();
            return Mapper.Map(domainEntities);
        }
        
        public virtual async Task<DTO.Cart> GetActiveCartWithoutCollections(Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Where(a => a.Active == true);
                
            var domainEntities = await query.FirstOrDefaultAsync();
            return Mapper.Map(domainEntities);
        }

        public virtual async Task<DTO.Cart> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(c => c.AppUser!)
                .Where(x => x.Id == Id);
            var domainEntity = await query.FirstOrDefaultAsync();
            return Mapper.Map(domainEntity);

        }
    }
}