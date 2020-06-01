namespace ee.itcollege.rotoni.pizzaApp.Contracts.BLL.Base.Mappers
{
    public interface IBaseMapper<TLeftObject, TRightObject> : ee.itcollege.rotoni.pizzaApp.Contracts.DAL.Base.Mappers.IBaseMapper<TLeftObject, TRightObject>
        where TLeftObject: class?, new()
        where TRightObject: class?, new()
    {
        
    }
}