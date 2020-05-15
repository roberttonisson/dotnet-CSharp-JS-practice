using AutoMapper;
using DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class DALMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public DALMapper() : base()
        { 
            // add more mappings
            
            MapperConfigurationExpression.CreateMap<Domain.Identity.AppUser, DAL.App.DTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<Domain.AdditionalTopping, DAL.App.DTO.AdditionalTopping>();
            MapperConfigurationExpression.CreateMap<Domain.Cart, DAL.App.DTO.Cart>();
            MapperConfigurationExpression.CreateMap<Domain.Crust, DAL.App.DTO.Crust>();
            MapperConfigurationExpression.CreateMap<Domain.DefaultTopping, DAL.App.DTO.DefaultTopping>();
            MapperConfigurationExpression.CreateMap<Domain.Drink, DAL.App.DTO.Drink>();
            MapperConfigurationExpression.CreateMap<Domain.DrinkInCart, DAL.App.DTO.DrinkInCart>();
            MapperConfigurationExpression.CreateMap<Domain.Invoice, DAL.App.DTO.Invoice>();
            MapperConfigurationExpression.CreateMap<Domain.InvoiceLine, DAL.App.DTO.InvoiceLine>();
            MapperConfigurationExpression.CreateMap<Domain.PartyOrder, DAL.App.DTO.PartyOrder>();
            MapperConfigurationExpression.CreateMap<Domain.PartyOrderInvoice, DAL.App.DTO.PartyOrderInvoice>();
            MapperConfigurationExpression.CreateMap<Domain.PizzaInCart, DAL.App.DTO.PizzaInCart>();
            MapperConfigurationExpression.CreateMap<Domain.PartyOrder, DAL.App.DTO.PartyOrder>();
            MapperConfigurationExpression.CreateMap<Domain.PizzaType, DAL.App.DTO.PizzaType>();
            MapperConfigurationExpression.CreateMap<Domain.Size, DAL.App.DTO.Size>();
            MapperConfigurationExpression.CreateMap<Domain.Topping, DAL.App.DTO.Topping>();
            MapperConfigurationExpression.CreateMap<Domain.Transport, DAL.App.DTO.Transport>();
            MapperConfigurationExpression.CreateMap<Domain.OrderStatus, DAL.App.DTO.OrderStatus>();
            
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}