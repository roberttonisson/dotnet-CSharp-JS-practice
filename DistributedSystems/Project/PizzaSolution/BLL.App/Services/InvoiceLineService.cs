using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using ee.itcollege.rotoni.pizzaApp.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class InvoiceLineService :
        BaseEntityService<IAppUnitOfWork, IInvoiceLineRepository, IInvoiceLineServiceMapper,
            DAL.App.DTO.InvoiceLine, BLL.App.DTO.InvoiceLine>, IInvoiceLineService
    {
        public InvoiceLineService(IAppUnitOfWork uow) : base(uow, uow.InvoiceLines, new InvoiceLineServiceMapper())
        {
        }

        public async Task<IEnumerable<InvoiceLine>> GetAllAsync(Guid? userId = null, bool noTracking = true)
        {
            return (await Repository.GetAllAsync(userId, noTracking)).Select(e => Mapper.Map(e));
        }

        public async Task<InvoiceLine> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true)
        {
            return Mapper.Map(await Repository.FirstOrDefaultAsync(Id, userId));
        }
    }
}