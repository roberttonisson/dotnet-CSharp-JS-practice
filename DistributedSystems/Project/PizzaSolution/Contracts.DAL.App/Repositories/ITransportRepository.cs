using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface ITransportRepository : IBaseRepository<Transport>
    {
        Task<TransportDTO> SelectDTO();
        Task<IEnumerable<TransportDTO>> SelectAllDTO();
    }
}