using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1.Mappers
{
    public class InvoiceDTOMapper
    {
        public InvoiceDTO GetDTO(BLL.App.DTO.Invoice inObject)
        {
            return new InvoiceDTO
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
                Total = inObject.Total,
                IsPaid = inObject.IsPaid,
                TransportId = inObject.TransportId,
                Transport = new TransportDTO
                {
                    Id = inObject.Transport!.Id,
                    Cost = inObject.Transport.Cost,
                    Address = inObject.Transport.Address
                }
            };
        }
        
        
        public BLL.App.DTO.Invoice GetBLL(InvoiceDTO inObject)
        {
            return new BLL.App.DTO.Invoice
            {
                Id = inObject.Id,
                AppUserId = inObject.AppUserId,
                Total = inObject.Total,
                IsPaid = inObject.IsPaid,
                TransportId = inObject.TransportId,
            };
        }
    }
}