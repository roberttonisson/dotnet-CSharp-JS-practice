using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;
using DAL.App.DTO.Identity;

namespace Contracts.DAL.App.Repositories
{
    public interface IAppUserRepositoryCustom: IAppUserRepositoryCustom<AppUser>
    {
    }

    public interface IAppUserRepositoryCustom<TAppUser>
    {
    }
    
}