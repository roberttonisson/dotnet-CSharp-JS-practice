using BLL.App.DTO;

using Contracts.DAL.App.Repositories;
using ee.itcollege.rotoni.pizzaApp.Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface ICrustService : IBaseEntityService<Crust>, ICrustRepositoryCustom<Crust>
    {
    }
}