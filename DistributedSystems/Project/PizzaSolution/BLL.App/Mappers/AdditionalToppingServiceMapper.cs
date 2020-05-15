using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;
namespace BLL.App.Mappers
{
    public class AdditionalToppingServiceMapper : BaseMapper<DALAppDTO.AdditionalTopping, BLLAppDTO.AdditionalTopping>, IAdditionalToppingServiceMapper
    {
        public AdditionalToppingServiceMapper() : base()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.AdditionalTopping, BLLAppDTO.AdditionalTopping>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Cart, BLLAppDTO.Cart>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Crust, BLLAppDTO.Crust>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.DefaultTopping, BLLAppDTO.DefaultTopping>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Drink, BLLAppDTO.Drink>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.DrinkInCart, BLLAppDTO.DrinkInCart>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Invoice, BLLAppDTO.Invoice>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.InvoiceLine, BLLAppDTO.InvoiceLine>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.PartyOrder, BLLAppDTO.PartyOrder>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.PartyOrderInvoice, BLLAppDTO.PartyOrderInvoice>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.PizzaInCart, BLLAppDTO.PizzaInCart>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.PartyOrder, BLLAppDTO.PartyOrder>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.PizzaType, BLLAppDTO.PizzaType>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Size, BLLAppDTO.Size>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Topping, BLLAppDTO.Topping>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Transport, BLLAppDTO.Transport>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.OrderStatus, BLLAppDTO.OrderStatus>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}