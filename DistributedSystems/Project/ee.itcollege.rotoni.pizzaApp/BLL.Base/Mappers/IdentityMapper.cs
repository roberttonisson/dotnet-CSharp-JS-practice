namespace ee.itcollege.rotoni.pizzaApp.BLL.Base.Mappers
{
    public class IdentityMapper<TLeftObject, TRightObject> : ee.itcollege.rotoni.pizzaApp.DAL.Base.Mappers.IdentityMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new() 
        where TLeftObject : class?, new()
    {
    }
}