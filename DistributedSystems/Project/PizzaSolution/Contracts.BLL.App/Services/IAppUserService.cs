using BLL.App.DTO;
using BLL.App.DTO.Identity;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IAppUserService : IBaseEntityService<AppUser>, IAppUserRepositoryCustom<AppUser>
    {
    }
}