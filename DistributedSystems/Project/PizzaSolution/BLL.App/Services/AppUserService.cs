using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.DTO.Identity;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class AppUserService :
        BaseEntityService<IAppUnitOfWork, IAppUserRepository, IAppUserServiceMapper,
            DAL.App.DTO.Identity.AppUser, BLL.App.DTO.Identity.AppUser>, IAppUserService
    {
        public AppUserService(IAppUnitOfWork uow) : base(uow, uow.AppUsers, new AppUserServiceMapper())
        {
        }
        
    }
}