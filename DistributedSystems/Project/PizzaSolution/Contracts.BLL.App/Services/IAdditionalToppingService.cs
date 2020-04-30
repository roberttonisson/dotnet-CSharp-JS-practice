using BLL.App.DTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IAdditionalToppingService : IBaseEntityService<AdditionalTopping>, IAdditionalToppingRepositoryCustom<AdditionalTopping>
    {
    }
}