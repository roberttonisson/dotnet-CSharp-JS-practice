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
    public class PartyOrderInvoiceRepository :
        EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.PartyOrderInvoice, DAL.App.DTO.PartyOrderInvoice>,
        IPartyOrderInvoiceRepository
    {
        public PartyOrderInvoiceRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new BaseMapper<PartyOrderInvoice, DTO.PartyOrderInvoice>())
        {
        }

        public virtual async Task<IEnumerable<DTO.PartyOrderInvoice>> GetAllAsync(Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(a => a.Invoice!)
                    .ThenInclude(p => p.AppUser!)
                .Include(a => a.Invoice!)
                    .ThenInclude(c => c.Transport!)
                .Include(a => a.PartyOrder!)
                    .ThenInclude(c => c.AppUser!);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public virtual async Task<DTO.PartyOrderInvoice> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(a => a.Invoice!)
                    .ThenInclude(p => p.AppUser!)
                .Include(a => a.Invoice!)
                    .ThenInclude(c => c.Transport!)
                .Include(a => a.PartyOrder!)
                    .ThenInclude(c => c.AppUser!)
                .Where(x => x.Id == Id);
            var domainEntity = await query.FirstOrDefaultAsync();
            return Mapper.Map(domainEntity);

        }
    }
}