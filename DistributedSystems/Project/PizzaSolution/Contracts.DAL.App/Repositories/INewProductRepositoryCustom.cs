using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface INewProductRepositoryCustom: INewProductRepositoryCustom<NewProduct>
    {
    }

    public interface INewProductRepositoryCustom<TNewProduct>
    {
        Task<IEnumerable<TNewProduct>> GetAllAsync(Guid? userId = null, bool noTracking = true);
        Task<IEnumerable<TNewProduct>> GetAllWithCollectionsAsync(Guid? userId = null, bool noTracking = true);
        Task<TNewProduct> FirstWithCollectionsAsync(Guid Id, Guid? userId = null, bool noTracking = true);
        Task<TNewProduct> FirstOrDefaultAsync(Guid Id, Guid? userId = null, bool noTracking = true);
    }
}