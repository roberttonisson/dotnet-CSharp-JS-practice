using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class DTOMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public DTOMapper() : base()
        { 
            // add more mappings
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Identity.AppUser, Identity.AppUserDTO>();
            MapperConfigurationExpression.CreateMap<Domain.Identity.AppUser, Identity.AppUserDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.AdditionalTopping, AdditionalToppingDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Cart, CartDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Crust, CrustDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.DefaultTopping, DefaultToppingDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Drink, DrinkDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.DrinkInCart, DrinkInCartDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Invoice, InvoiceDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.InvoiceLine, InvoiceLineDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.PartyOrder, PartyOrderDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.PartyOrderInvoice, PartyOrderInvoiceDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.PizzaInCart, PizzaInCartDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.PartyOrder, PartyOrderDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.PizzaType, PizzaTypeDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Size, SizeDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Topping, ToppingDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Transport, TransportDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.OrderStatus, OrderStatusDTO>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}