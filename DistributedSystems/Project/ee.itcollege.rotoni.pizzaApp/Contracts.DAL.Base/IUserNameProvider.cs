namespace ee.itcollege.rotoni.pizzaApp.Contracts.DAL.Base
{
    public interface IUserNameProvider
    {
        string CurrentUserName { get;  }
    }
}