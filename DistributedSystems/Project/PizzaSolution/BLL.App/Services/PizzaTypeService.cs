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
    public class PizzaTypeService :
        BaseEntityService<IAppUnitOfWork, IPizzaTypeRepository, IPizzaTypeServiceMapper,
            DAL.App.DTO.PizzaType, BLL.App.DTO.PizzaType>, IPizzaTypeService
    {
        public PizzaTypeService(IAppUnitOfWork uow) : base(uow, uow.PizzaTypes, new PizzaTypeServiceMapper())
        {
        }

    }
}