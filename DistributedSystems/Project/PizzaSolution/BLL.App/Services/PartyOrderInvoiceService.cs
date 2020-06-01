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
    public class PartyOrderInvoiceService :
        BaseEntityService<IAppUnitOfWork, IPartyOrderInvoiceRepository, IPartyOrderInvoiceServiceMapper,
            DAL.App.DTO.PartyOrderInvoice, BLL.App.DTO.PartyOrderInvoice>, IPartyOrderInvoiceService
    {
        public PartyOrderInvoiceService(IAppUnitOfWork uow) : base(uow, uow.PartyOrderInvoices, new PartyOrderInvoiceServiceMapper())
        {
        }

        public async Task<IEnumerable<PartyOrderInvoice>> GetAllAsync(Guid? userId = null, bool noTracking = true)
        {
            return (await Repository.GetAllAsync(userId, noTracking)).Select(e => Mapper.Map(e));
        }

        public async Task<PartyOrderInvoice> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true)
        {
            return Mapper.Map(await Repository.FirstOrDefaultAsync(Id, userId));
        }
    }
}