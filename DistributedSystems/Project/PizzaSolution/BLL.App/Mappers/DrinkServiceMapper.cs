
using Contracts.BLL.App.Mappers;
using ee.itcollege.rotoni.pizzaApp.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;
namespace BLL.App.Mappers
{
    public class DrinkServiceMapper : BaseMapper<DALAppDTO.Drink, BLLAppDTO.Drink>, IDrinkServiceMapper
    {
        
    }
}