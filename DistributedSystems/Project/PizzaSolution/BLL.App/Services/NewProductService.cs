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
    public class NewProductService :
        BaseEntityService<IAppUnitOfWork, INewProductRepository, INewProductServiceMapper,
            DAL.App.DTO.NewProduct, BLL.App.DTO.NewProduct>, INewProductService
    {
        public NewProductService(IAppUnitOfWork uow) : base(uow, uow.NewProducts, new NewProductServiceMapper())
        {
        }

        public async Task<IEnumerable<NewProduct>> GetAllAsync(Guid? userId = null, bool noTracking = true)
        {
            return (await Repository.GetAllAsync(userId, noTracking)).Select(e => Mapper.Map(e));
        }

        public async Task<IEnumerable<NewProduct>> GetAllWithCollectionsAsync(Guid? userId = null, bool noTracking = true)
        {
            return (await Repository.GetAllWithCollectionsAsync(userId, noTracking)).Select(e => Mapper.Map(e));
        }
        
        public async Task<NewProduct> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true)
        {
            return Mapper.Map(await Repository.FirstOrDefaultAsync(Id, userId));
        }
        public async Task<NewProduct> FirstWithCollectionsAsync(Guid Id, Guid? userId = null, bool noTracking = true)
        {
            return Mapper.Map(await Repository.FirstWithCollectionsAsync(Id, userId));
        }
        
    }
}