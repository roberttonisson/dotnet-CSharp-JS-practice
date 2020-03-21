using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PartyOrderInvoiceRepository : BaseRepository<PartyOrderInvoice>, IPartyOrderInvoiceRepository
    {
        public PartyOrderInvoiceRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}