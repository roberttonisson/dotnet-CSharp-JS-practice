using System;
using System.Threading.Tasks;
using BLL.App.DTO;

using Contracts.DAL.App.Repositories;
using ee.itcollege.rotoni.pizzaApp.Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface IInvoiceService : IBaseEntityService<Invoice>, IInvoiceRepositoryCustom<Invoice>
    {
        Task<bool> ReOrder(IAppBLL bll, Guid invoiceId, Guid? UserId);
    }
}