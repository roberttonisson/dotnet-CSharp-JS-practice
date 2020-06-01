using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using ee.itcollege.rotoni.pizzaApp.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class ToppingService :
        BaseEntityService<IAppUnitOfWork, IToppingRepository, IToppingServiceMapper,
            DAL.App.DTO.Topping, BLL.App.DTO.Topping>, IToppingService
    {
        public ToppingService(IAppUnitOfWork uow) : base(uow, uow.Toppings, new ToppingServiceMapper())
        {
        }
        
    }
}