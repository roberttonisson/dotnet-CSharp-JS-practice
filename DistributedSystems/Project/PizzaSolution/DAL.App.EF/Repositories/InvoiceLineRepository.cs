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
    public class InvoiceLineRepository :
        EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.InvoiceLine, DAL.App.DTO.InvoiceLine>,
        IInvoiceLineRepository
    {
        public InvoiceLineRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<InvoiceLine, DTO.InvoiceLine>())
        {
        }

        public virtual async Task<IEnumerable<DTO.InvoiceLine>> GetAllAsync(Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(i => i.Invoice!)
                    .ThenInclude(c => c.AppUser!)
                .Include(i => i.Invoice!)
                    .ThenInclude(c => c.Transport!)
                .Include(i => i.Invoice!)
                    .ThenInclude(c => c.OrderStatus!)
                
                .Include(i => i.DrinkInCart!)
                    .ThenInclude(d => d.Drink!)
                .Include(i => i.DrinkInCart!)
                    .ThenInclude(d => d.Cart!)
                        .ThenInclude(d => d.AppUser)
                .Include(i => i.DrinkInCart!)
                    .ThenInclude(d => d.Drink!)
                
                .Include(i => i.PizzaInCart!)
                    .ThenInclude(p => p.PizzaType!)
                .Include(i => i.PizzaInCart!)
                    .ThenInclude(p => p.Crust!)
                .Include(i => i.PizzaInCart!)
                    .ThenInclude(p => p.Size!)
                .Include(i => i.PizzaInCart!)
                    .ThenInclude(p => p.Cart!)
                        .ThenInclude(p => p.AppUser);
            if (userId != null)
            {
                query = query.Where(a => a.Invoice!.AppUser!.Id == userId);
            }
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public virtual async Task<DTO.InvoiceLine> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(i => i.Invoice!)
                    .ThenInclude(c => c.AppUser!)
                .Include(i => i.Invoice!)
                    .ThenInclude(c => c.Transport!)
                .Include(i => i.Invoice!)
                    .ThenInclude(c => c.OrderStatus!)
                
                .Include(i => i.DrinkInCart!)
                    .ThenInclude(d => d.Drink!)
                .Include(i => i.DrinkInCart!)
                    .ThenInclude(d => d.Cart!)
                        .ThenInclude(d => d.AppUser)
                .Include(i => i.DrinkInCart!)
                    .ThenInclude(d => d.Drink!)
                
                .Include(i => i.PizzaInCart!)
                    .ThenInclude(p => p.PizzaType!)
                .Include(i => i.PizzaInCart!)
                    .ThenInclude(p => p.Crust!)
                .Include(i => i.PizzaInCart!)
                    .ThenInclude(p => p.Size!)
                .Include(i => i.PizzaInCart!)
                    .ThenInclude(p => p.Cart!)
                        .ThenInclude(p => p.AppUser)
                .Where(x => x.Id == Id);
            if (userId != null)
            {
                query = query.Where(a => a.Invoice!.AppUser!.Id == userId);
            }
            var domainEntity = await query.FirstOrDefaultAsync();
            return Mapper.Map(domainEntity);

        }
    }
}