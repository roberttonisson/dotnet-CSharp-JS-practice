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
    public class ToppingRepository :
        EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.Topping, DAL.App.DTO.Topping>,
        IToppingRepository
    {
        public ToppingRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new BaseMapper<Topping, DTO.Topping>())
        {
        }
        
    }
}