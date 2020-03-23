using Contracts.DAL.Base.Repositories;
using Domain;
using Domain.Identity;

namespace Contracts.DAL.App.Repositories
{
    public interface IUserRepository : IBaseRepository<AppUser>
    {
        // add your custom methods here!
    }
}