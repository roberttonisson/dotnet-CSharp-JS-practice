
using Contracts.BLL.App.Mappers;
using ee.itcollege.rotoni.pizzaApp.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;
namespace BLL.App.Mappers
{
    public class ToppingServiceMapper : BaseMapper<DALAppDTO.Topping, BLLAppDTO.Topping>, IToppingServiceMapper
    {
        
    }
}