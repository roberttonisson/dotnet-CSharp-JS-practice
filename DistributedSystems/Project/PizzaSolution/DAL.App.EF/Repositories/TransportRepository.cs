using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;



using Domain;
using ee.itcollege.rotoni.pizzaApp.DAL.Base.EF.Repositories;
using ee.itcollege.rotoni.pizzaApp.DAL.Base.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class TransportRepository :
        EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.Transport, DAL.App.DTO.Transport>,
        ITransportRepository
    {
        public TransportRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new BaseMapper<Transport, DTO.Transport>())
        {
        }
        
    }
}