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
    public class InvoiceRepository :
        EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.Invoice, DAL.App.DTO.Invoice>,
        IInvoiceRepository
    {
        public InvoiceRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new BaseMapper<Invoice, DTO.Invoice>())
        {
        }

        public virtual async Task<IEnumerable<DTO.Invoice>> GetAllAsync(Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(i => i.AppUser)
                .Include(i => i.Transport);
            if (userId != null)
            {
                query = query.Where(a => a.AppUser!.Id == userId);
            }
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