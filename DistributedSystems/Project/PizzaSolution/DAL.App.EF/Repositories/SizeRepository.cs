using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class SizeRepository :
        EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.Size, DAL.App.DTO.Size>,
        ISizeRepository
    {
        public SizeRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new BaseMapper<Size, DTO.Size>())
        {
        }
    }
}