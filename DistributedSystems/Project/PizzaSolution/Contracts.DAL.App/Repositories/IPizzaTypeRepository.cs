using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPizzaTypeRepository  : IBaseRepository<PizzaType>, IPizzaTypeRepositoryCustom
    {
        
    }
}