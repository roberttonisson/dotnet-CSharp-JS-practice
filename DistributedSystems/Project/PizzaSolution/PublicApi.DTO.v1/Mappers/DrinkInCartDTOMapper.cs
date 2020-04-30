using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1.Mappers
{
    public class DrinkInCartDTOMapper
    {
        public DrinkInCartDTO GetDTO(BLL.App.DTO.DrinkInCart inObject)
        {
            return new DrinkInCartDTO
            {
                Id = inObject.Id,
                Quantity = inObject.Quantity,
                Price = inObject.Price,
                DrinkId = inObject.DrinkId,
                Drink = new DrinkDTO
                {
                    Id = inObject.Drink!.Id,
                    Name = inObject.Drink.Name,
                    Price = inObject.Drink.Price,
                    Size = inObject.Drink.Size
                },
                CartId = inObject.CartId,
                Cart = new CartDTO
                {
                    Id = inObject.Cart!.Id,
                    Total = inObject.Cart!.Total,
                    AppUserId = inObject.Cart!.AppUserId,
                    AppUser = new AppUserDTO
                    {
                        Id = inObject.Cart.AppUser!.Id,
                        Email = inObject.Cart.AppUser!.Email,
                        UserName = inObject.Cart.AppUser!.UserName,
                        FirstName = inObject.Cart.AppUser!.FirstName,
                        LastName = inObject.Cart.AppUser!.LastName,
                        Address = inObject.Cart.AppUser!.Address
                    }
                }
            };
        }
        
        public BLL.App.DTO.DrinkInCart GetBLL(DrinkInCartDTO inObject)
        {
            return new BLL.App.DTO.DrinkInCart
            {
                Id = inObject.Id,
                Quantity = inObject.Quantity,
                Price = inObject.Price,
                DrinkId = inObject.DrinkId,
                CartId = inObject.CartId,
            };
        }
    }
}