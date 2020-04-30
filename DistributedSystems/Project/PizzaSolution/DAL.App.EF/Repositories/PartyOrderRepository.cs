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
    public class PartyOrderRepository :
        EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.PartyOrder, DAL.App.DTO.PartyOrder>,
        IPartyOrderRepository
    {
        public PartyOrderRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new BaseMapper<PartyOrder, DTO.PartyOrder>())
        {
        }

        public virtual async Task<IEnumerable<DTO.PartyOrder>> GetAllAsync(Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(a => a.AppUser!);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public virtual async Task<DTO.PartyOrder> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(a => a.AppUser!)
                .Where(x => x.Id == Id);
            var domainEntity = await query.FirstOrDefaultAsync();
            return Mapper.Map(domainEntity);

        }
    }
}