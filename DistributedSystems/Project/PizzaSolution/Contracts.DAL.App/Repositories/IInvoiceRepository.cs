using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IInvoiceRepository : IBaseRepository<Invoice>
    {
        Task<IEnumerable<Invoice>> GetIncluded(Guid? userId = null);
        Task<Invoice> FirstOrDefaultAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
    }
}