using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1.Mappers
{
    public class AdditionalToppingDTOMapper
    {
        public AdditionalToppingDTO GetDTO(BLL.App.DTO.AdditionalTopping inObject)
        {
            return new AdditionalToppingDTO()
            {
                Id = inObject.Id,
                ToppingId = inObject.ToppingId,
                Topping = new ToppingDTO
                {
                    Id = inObject.Topping!.Id,
                    Name = inObject.Topping.Name,
                    Price = inObject.Topping.Price
                },
                
                PizzaInCartId = inObject.PizzaInCartId,
                PizzaInCart = new PizzaInCartDTO
                {
                    Id = inObject.PizzaInCart!.Id,
                    Price = inObject.PizzaInCart.Price,
                    Quantity = inObject.PizzaInCart.Quantity,
                    PizzaTypeId = inObject.PizzaInCart.PizzaTypeId,
                    PizzaType = new PizzaTypeDTO
                    {
                        Id = inObject.PizzaInCart.PizzaType!.Id,
                        Name = inObject.PizzaInCart.PizzaType.Name,
                        Price = inObject.PizzaInCart.PizzaType.Price
                    },
                    CrustId = inObject.PizzaInCart.CrustId,
                    Crust = new CrustDTO
                    {
                        Id = inObject.PizzaInCart.Crust!.Id,
                        Name = inObject.PizzaInCart.Crust.Name,
                        Price = inObject.PizzaInCart.Crust.Price,
                    },
                    SizeId = inObject.PizzaInCart.SizeId,
                    Size = new SizeDTO
                    {
                        Id = inObject.PizzaInCart.Size!.Id,
                        Name = inObject.PizzaInCart.Size.Name,
                        Price = inObject.PizzaInCart.Size.Price,
                        SizeCm = inObject.PizzaInCart.Size.SizeCm
                    },
                    CartId = inObject.PizzaInCart.CartId,
                    Cart = new CartDTO
                    {
                        Id = inObject.PizzaInCart.Cart!.Id,
                        Total = inObject.PizzaInCart.Cart!.Total,
                        AppUserId = inObject.PizzaInCart.Cart!.AppUserId,
                        AppUser = new AppUserDTO
                        {
                            Id = inObject.PizzaInCart.Cart.AppUser!.Id,
                            Email = inObject.PizzaInCart.Cart.AppUser!.Email,
                            UserName = inObject.PizzaInCart.Cart.AppUser!.UserName,
                            FirstName = inObject.PizzaInCart.Cart.AppUser!.FirstName,
                            LastName = inObject.PizzaInCart.Cart.AppUser!.LastName,
                            Address = inObject.PizzaInCart.Cart.AppUser!.Address
                        }
                    }
                }
            };
        }
        
        public BLL.App.DTO.AdditionalTopping GetBLL(AdditionalToppingDTO inObject)
        {
            return new BLL.App.DTO.AdditionalTopping
            {
                Id = inObject.Id,
                ToppingId = inObject.ToppingId,
                PizzaInCartId = inObject.PizzaInCartId,
            };
        }
    }
}