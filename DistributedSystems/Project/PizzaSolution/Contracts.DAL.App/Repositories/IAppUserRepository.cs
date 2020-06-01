
using DAL.App.DTO;
using DAL.App.DTO.Identity;
using ee.itcollege.rotoni.pizzaApp.Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.App.Repositories
{
    public interface IAppUserRepository  : IBaseRepository<AppUser>, IAppUserRepositoryCustom
    {
        
    }
}