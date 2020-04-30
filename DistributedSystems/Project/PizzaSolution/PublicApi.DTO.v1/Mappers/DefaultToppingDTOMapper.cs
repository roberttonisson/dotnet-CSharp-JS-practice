namespace PublicApi.DTO.v1.Mappers
{
    public class DefaultToppingDTOMapper : BaseMapper<BLL.App.DTO.DefaultTopping, DefaultToppingDTO >
    {
        public DefaultToppingDTO GetDTO(BLL.App.DTO.DefaultTopping inObject)
        {
            return new DefaultToppingDTO
            {
                Id = inObject.Id,
                ToppingId = inObject.ToppingId,
                Topping = new ToppingDTO
                {
                    Id = inObject.Topping!.Id,
                    Name = inObject.Topping.Name,
                    Price = inObject.Topping.Price
                },
                PizzaTypeId = inObject.PizzaTypeId,
                PizzaType = new PizzaTypeDTO
                {
                    Id = inObject.PizzaType!.Id,
                    Name = inObject.PizzaType.Name,
                    Price = inObject.PizzaType.Price
                }
            };
        }
        
        public BLL.App.DTO.DefaultTopping GetBLL(DefaultToppingDTO inObject)
        {
            return new BLL.App.DTO.DefaultTopping
            {
                Id = inObject.Id,
                ToppingId = inObject.ToppingId,
                PizzaTypeId = inObject.PizzaTypeId,
            };
        }
    }
}