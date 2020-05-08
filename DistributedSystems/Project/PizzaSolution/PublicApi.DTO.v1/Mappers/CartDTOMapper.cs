using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1.Mappers
{
    public class CartDTOMapper : DTOMapper<BLL.App.DTO.Cart, CartDTO>

    {
    /* public CartDTO GetDTO(BLL.App.DTO.Cart inObject)
     {
         var outObject = new CartDTO
         {
             Id = inObject.Id,
             Total = inObject.Total,
             AppUserId = inObject.AppUserId,
             AppUser = new AppUserDTO
             {
                 Id = inObject.AppUser!.Id,
                 Email = inObject.AppUser!.Email,
                 UserName = inObject.AppUser!.UserName,
                 FirstName = inObject.AppUser!.FirstName,
                 LastName = inObject.AppUser!.LastName,
                 Address = inObject.AppUser!.Address
             },
         };
         return outObject;
     }

     public BLL.App.DTO.Cart GetBLL(CartDTO inObject)
     {
         return new BLL.App.DTO.Cart
         {
             Id = inObject.Id,
             AppUserId = inObject.AppUserId,
             Total = inObject.Total,
         };
     }*/
    }
}