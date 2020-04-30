using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class TransportService :
        BaseEntityService<IAppUnitOfWork, ITransportRepository, ITransportServiceMapper,
            DAL.App.DTO.Transport, BLL.App.DTO.Transport>, ITransportService
    {
        public TransportService(IAppUnitOfWork uow) : base(uow, uow.Transports, new TransportServiceMapper())
        {
        }

    }
}