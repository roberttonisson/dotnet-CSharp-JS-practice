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
    public class OrderStatusService :
        BaseEntityService<IAppUnitOfWork, IOrderStatusRepository, IOrderStatusServiceMapper,
            DAL.App.DTO.OrderStatus, BLL.App.DTO.OrderStatus>, IOrderStatusService
    {
        public OrderStatusService(IAppUnitOfWork uow) : base(uow, uow.OrderStatuses, new OrderStatusServiceMapper())
        {
        }

    }
}