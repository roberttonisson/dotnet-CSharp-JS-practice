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
using Drink = BLL.App.DTO.Drink;

namespace DAL.App.EF.Repositories
{
    public class InvoiceRepository :
        EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.Invoice, DAL.App.DTO.Invoice>,
        IInvoiceRepository
    {
        public InvoiceRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Invoice, DTO.Invoice>())
        {
        }

        public virtual async Task<IEnumerable<DTO.Invoice>> GetAllAsync(Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(i => i.AppUser)
                .Include(i => i.Transport)
                .Include(i => i.OrderStatus);
            if (userId != null)
            {
                query = query.Where(a => a.AppUser!.Id == userId);
            }
            query = query.OrderBy(t => t.CreatedAt);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }
        
        public virtual async Task<IEnumerable<DTO.Invoice>> GetAllWithCollectionsAsync(Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(i => i.AppUser)
                .Include(i => i.Transport)
                .Include(i => i.OrderStatus)
               
                .Include(c => c.InvoiceLines)
                    .ThenInclude(e => e.DrinkInCart)
                        .ThenInclude(f => f.Drink)
                
                .Include(c => c.InvoiceLines)
                    .ThenInclude(e => e.PizzaInCart)
                        .ThenInclude(f => f.PizzaType)
                .Include(c => c.InvoiceLines)
                    .ThenInclude(e => e.PizzaInCart)
                        .ThenInclude(f => f.Crust)
                .Include(c => c.InvoiceLines)
                    .ThenInclude(e => e.PizzaInCart)
                        .ThenInclude(f => f.Size)
                .Include(c => c.InvoiceLines)
                    .ThenInclude(e => e.PizzaInCart)
                        .ThenInclude(f => f.AdditionalToppings)
                            .ThenInclude(x => x.Topping)
                
                .Include(c => c.InvoiceLines)
                    .ThenInclude(e => e.DrinkInCart)
                        .ThenInclude(f => f.Drink);
            if (userId != null)
            {
                query = query.Where(a => a.AppUser!.Id == userId);
            }

            query = query.OrderByDescending(t => t.CreatedAt);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public virtual async Task<DTO.Invoice> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(i => i.AppUser)
                .Include(i => i.Transport)
                .Include(i => i.OrderStatus)
                .Where(x => x.Id == Id);
            if (userId != null)
            {
                query = query.Where(a => a.AppUser!.Id == userId);
            }
            var domainEntity = await query.FirstOrDefaultAsync();
            return Mapper.Map(domainEntity);

        }
    }
}