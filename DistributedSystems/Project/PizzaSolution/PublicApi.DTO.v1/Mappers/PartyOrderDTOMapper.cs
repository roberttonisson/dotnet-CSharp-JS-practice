using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1.Mappers
{
    public class PartyOrderDTOMapper
    {
        public PartyOrderDTO GetDTO(BLL.App.DTO.PartyOrder inObject)
        {
            return new PartyOrderDTO
            {
                Id = inObject.Id,
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
                Start = inObject.Start,
                End = inObject.End,
                Total = inObject.Total,
                Address = inObject.Address,
                InviteKey = inObject.InviteKey
            };
        }


        public BLL.App.DTO.PartyOrder GetBLL(PartyOrderDTO inObject)
        {
            return new BLL.App.DTO.PartyOrder
            {
                Id = inObject.Id,
                AppUserId = inObject.AppUserId,
                Start = inObject.Start,
                End = inObject.End,
                Total = inObject.Total,
                Address = inObject.Address,
                InviteKey = inObject.InviteKey,
            };
        }
    }
}