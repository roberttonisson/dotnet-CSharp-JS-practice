using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1.Mappers
{
    public class PizzaInCartDTOMapper
    {
        public PizzaInCartDTO GetDTO(BLL.App.DTO.PizzaInCart inObject)
        {
            return new PizzaInCartDTO
            {
                    Id = inObject.Id,
                    Price = inObject.Price,
                    Quantity = inObject.Quantity,
                    PizzaTypeId = inObject.PizzaTypeId,
                    PizzaType = new PizzaTypeDTO
                    {
                        Id = inObject.PizzaType!.Id,
                        Name = inObject.PizzaType.Name,
                        Price = inObject.PizzaType.Price
                    },
                    CrustId = inObject.CrustId,
                    Crust = new CrustDTO
                    {
                        Id = inObject.Crust!.Id,
                        Name = inObject.Crust.Name,
                        Price = inObject.Crust.Price,
                    },
                    SizeId = inObject.SizeId,
                    Size = new SizeDTO
                    {
                        Id = inObject.Size!.Id,
                        Name = inObject.Size.Name,
                        Price = inObject.Size.Price,
                        SizeCm = inObject.Size.SizeCm
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
        
        public BLL.App.DTO.PizzaInCart GetBLL(PizzaInCartDTO inObject)
        {
            return new BLL.App.DTO.PizzaInCart
            {
                Id = inObject.Id,
                Price = inObject.Price,
                Quantity = inObject.Quantity,
                PizzaTypeId = inObject.PizzaTypeId,
                CrustId = inObject.CrustId,
                SizeId = inObject.SizeId,
                CartId = inObject.CartId,
            };
        }
    }
}