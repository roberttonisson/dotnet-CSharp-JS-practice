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
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(DbContext dbContext) : base(dbContext)
        {
        }
        
        public async Task<IEnumerable<Invoice>> GetIncluded(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(i => i.AppUser)
                .Include(i => i.Transport)
                .AsQueryable();
            if (userId != null)
            {
                query = query.Where(i => i.AppUser.Id == userId);
            }

            return await query.ToListAsync();
        }
        
        public async Task<Invoice> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(i => i.AppUser)
                .Include(i => i.Transport)
                .Where(x => x.Id == id)
                .AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUser.Id == userId);
            }

            return await query.FirstOrDefaultAsync();
        }
        
        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var invoice = await FirstOrDefaultAsync(id, userId);
            base.Remove(invoice);
        }
    }
}