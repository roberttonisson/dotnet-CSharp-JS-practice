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
    public class InvoiceLineRepository : BaseRepository<InvoiceLine>, IInvoiceLineRepository
    {
        public InvoiceLineRepository(DbContext dbContext) : base(dbContext)
        {
        }
        
        public async Task<IEnumerable<InvoiceLine>> GetIncluded(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(i => i.Invoice!)
                    .ThenInclude(c => c.AppUser!)
                .Include(i => i.DrinkInCart!)
                    .ThenInclude(d => d.Drink!)
                .Include(i => i.PizzaInCart!)
                    .ThenInclude(p => p.PizzaType!)
                .AsQueryable();
            if (userId != null)
            {
                query = query.Where(x => x.Invoice!.AppUser!.Id == userId);
            }

            return await query.ToListAsync();
        }
        
        public async Task<InvoiceLine> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(i => i.Invoice!)
                    .ThenInclude(c => c.AppUser!)
                .Include(i => i.DrinkInCart!)
                    .ThenInclude(d => d.Drink!)
                .Include(i => i.PizzaInCart!)
                    .ThenInclude(p => p.PizzaType!)
                .Where(x => x.Id == id)
                .AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.Invoice!.AppUser!.Id == userId);
            }

            return await query.FirstOrDefaultAsync();
        }
        
        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var invoiceLine = await FirstOrDefaultAsync(id, userId);
            base.Remove(invoiceLine);
        }
    }
}