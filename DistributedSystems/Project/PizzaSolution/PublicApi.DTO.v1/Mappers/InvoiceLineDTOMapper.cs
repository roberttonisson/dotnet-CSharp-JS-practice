using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1.Mappers
{
    public class InvoiceLineDTOMapper
    {
        public InvoiceLineDTO GetDTO(BLL.App.DTO.InvoiceLine inObject)
        {
            return new InvoiceLineDTO
            {
                Id = inObject.Id,
                Total = inObject.Total,
                Quantity = inObject.Quantity,
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
                },
                DrinkInCartId = inObject.DrinkInCartId,
                DrinkInCart = new DrinkInCartDTO
                {
                    Id = inObject.Id,
                    Quantity = inObject.Quantity,
                    Price = inObject.DrinkInCart!.Price,
                    DrinkId = inObject.DrinkInCart.DrinkId,
                    Drink = new DrinkDTO
                    {
                        Id = inObject.DrinkInCart.Drink!.Id,
                        Name = inObject.DrinkInCart.Drink.Name,
                        Price = inObject.DrinkInCart.Drink.Price,
                        Size = inObject.DrinkInCart.Drink.Size
                    },
                    CartId = inObject.DrinkInCart.CartId,
                    Cart = new CartDTO
                    {
                        Id = inObject.DrinkInCart.Cart!.Id,
                        Total = inObject.DrinkInCart.Cart!.Total,
                        AppUserId = inObject.DrinkInCart.Cart!.AppUserId,
                        AppUser = new AppUserDTO
                        {
                            Id = inObject.DrinkInCart.Cart.AppUser!.Id,
                            Email = inObject.DrinkInCart.Cart.AppUser!.Email,
                            UserName = inObject.DrinkInCart.Cart.AppUser!.UserName,
                            FirstName = inObject.DrinkInCart.Cart.AppUser!.FirstName,
                            LastName = inObject.DrinkInCart.Cart.AppUser!.LastName,
                            Address = inObject.DrinkInCart.Cart.AppUser!.Address
                        }
                    }
                },
                InvoiceId = inObject.InvoiceId,
                Invoice = new InvoiceDTO
                {
                    Id = inObject.Id,
                    AppUserId = inObject.Invoice!.AppUserId,
                    AppUser = new AppUserDTO
                    {
                        Id = inObject.Invoice.AppUser!.Id,
                        Email = inObject.Invoice.AppUser!.Email,
                        UserName = inObject.Invoice.AppUser!.UserName,
                        FirstName = inObject.Invoice.AppUser!.FirstName,
                        LastName = inObject.Invoice.AppUser!.LastName,
                        Address = inObject.Invoice.AppUser!.Address
                    },
                    Total = inObject.Total,
                    IsPaid = inObject.Invoice.IsPaid,
                    TransportId = inObject.Invoice.TransportId,
                    Transport = new TransportDTO
                    {
                        Id = inObject.Invoice.Transport!.Id,
                        Cost = inObject.Invoice.Transport.Cost,
                        Address = inObject.Invoice.Transport.Address
                    }
                }
            };
        }
        
        public BLL.App.DTO.InvoiceLine GetBLL(InvoiceLineDTO inObject)
        {
            return new BLL.App.DTO.InvoiceLine
            {
                Id = inObject.Id,
                Total = inObject.Total,
                Quantity = inObject.Quantity,
                PizzaInCartId = inObject.PizzaInCartId,
                DrinkInCartId = inObject.DrinkInCartId,
                InvoiceId = inObject.InvoiceId,
            };
        }
    }
}