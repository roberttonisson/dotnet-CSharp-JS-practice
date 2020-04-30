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
    public class CrustService :
        BaseEntityService<IAppUnitOfWork, ICrustRepository, ICrustServiceMapper,
            DAL.App.DTO.Crust, BLL.App.DTO.Crust>, ICrustService
    {
        public CrustService(IAppUnitOfWork uow) : base(uow, uow.Crusts, new CrustServiceMapper())
        {
        }
        
    }
}