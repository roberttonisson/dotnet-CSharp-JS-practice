using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1.Mappers
{
    public class CartDTOMapper
    {
        public CartDTO GetDTO(BLL.App.DTO.Cart inObject)
        {
            return new CartDTO
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
                }
            };
        }
        
        public BLL.App.DTO.Cart GetBLL(CartDTO inObject)
        {
            return new BLL.App.DTO.Cart
            {
                Id = inObject.Id,
                AppUserId = inObject.AppUserId,
                Total = inObject.Total,
            };
        }
    }
}