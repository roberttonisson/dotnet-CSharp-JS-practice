using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IInvoiceLineRepository : IBaseRepository<InvoiceLine>
    {
        Task<IEnumerable<InvoiceLine>> GetIncluded(Guid? userId = null);
        Task<InvoiceLine> FirstOrDefaultAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
    }
}