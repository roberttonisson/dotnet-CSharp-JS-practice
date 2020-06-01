using BLL.App.DTO;
using BLL.App.DTO.Identity;

using Contracts.DAL.App.Repositories;
using ee.itcollege.rotoni.pizzaApp.Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface IAppUserService : IBaseEntityService<AppUser>, IAppUserRepositoryCustom<AppUser>
    {
    }
}