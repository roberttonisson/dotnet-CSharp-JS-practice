using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using DAL.App.DTO.Identity;

namespace Contracts.DAL.App.Repositories
{
    public interface IAppUserRepository  : IBaseRepository<AppUser>, IAppUserRepositoryCustom
    {
        
    }
}