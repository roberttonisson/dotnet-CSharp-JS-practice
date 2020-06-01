
using Contracts.BLL.App.Mappers;
using ee.itcollege.rotoni.pizzaApp.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;
namespace BLL.App.Mappers
{
    public class AppUserServiceMapper : BaseMapper<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>, IAppUserServiceMapper
    {
        
    }
}