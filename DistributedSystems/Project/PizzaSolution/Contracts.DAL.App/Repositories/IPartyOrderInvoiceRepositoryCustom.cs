using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPartyOrderInvoiceRepositoryCustom: IPartyOrderInvoiceRepositoryCustom<PartyOrderInvoice>
    {
    }

    public interface IPartyOrderInvoiceRepositoryCustom<TPartyOrderInvoice>
    {
        Task<IEnumerable<TPartyOrderInvoice>> GetAllAsync( Guid? userId = null, bool noTracking = true);
        Task<TPartyOrderInvoice> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true);
    }
    
}