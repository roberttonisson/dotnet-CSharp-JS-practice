using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface ISizeRepository : IBaseRepository<Size>
    {
        Task<SizeDTO> SelectDTO();
        Task<IEnumerable<SizeDTO>> SelectAllDTO();
    }
}