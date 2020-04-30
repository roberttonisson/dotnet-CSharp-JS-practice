using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1.Mappers
{
    public class PartyOrderInvoiceDTOMapper
    {
        public PartyOrderInvoiceDTO GetDTO(BLL.App.DTO.PartyOrderInvoice inObject)
        {
            return new PartyOrderInvoiceDTO
            {
                Id = inObject.Id,
                PartyOrderId = inObject.PartyOrderId,
                PartyOrder = new PartyOrderDTO(){
                    Id = inObject.Id,
                    AppUserId = inObject.PartyOrder!.AppUserId,
                    AppUser = new AppUserDTO
                    {
                        Id = inObject.PartyOrder.AppUser!.Id,
                        Email = inObject.PartyOrder.AppUser!.Email,
                        UserName = inObject.PartyOrder.AppUser!.UserName,
                        FirstName = inObject.PartyOrder.AppUser!.FirstName,
                        LastName = inObject.PartyOrder.AppUser!.LastName,
                        Address = inObject.PartyOrder.AppUser!.Address
                    },
                    Start = inObject.PartyOrder.Start,
                    End = inObject.PartyOrder.End,
                    Total = inObject.PartyOrder.Total,
                    Address = inObject.PartyOrder.Address,
                    InviteKey = inObject.PartyOrder.InviteKey
                },
                InvoiceId = inObject.InvoiceId,
                Invoice = new InvoiceDTO(){
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
                    Total = inObject.Invoice.Total,
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
        
        public BLL.App.DTO.PartyOrderInvoice GetBLL(PartyOrderInvoiceDTO inObject)
        {
            return new BLL.App.DTO.PartyOrderInvoice
            {
                Id = inObject.Id,
                PartyOrderId = inObject.PartyOrderId,
                InvoiceId = inObject.InvoiceId,
            };
        }
    }
}