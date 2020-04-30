

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class AppUserRepository :
        EFBaseRepository<AppDbContext, Domain.Identity.AppUser, AppUser, DAL.App.DTO.Identity.AppUser>,
        IAppUserRepository
    {
        public AppUserRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new BaseMapper<AppUser, DAL.App.DTO.Identity.AppUser>())
        {
        }
        
    }
}