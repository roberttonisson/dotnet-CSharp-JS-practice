
using ee.itcollege.rotoni.pizzaApp.Contracts.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface IAppUserServiceMapper: IBaseMapper<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>
    {
        
    }
}