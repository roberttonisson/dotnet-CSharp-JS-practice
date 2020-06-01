
using ee.itcollege.rotoni.pizzaApp.Contracts.DAL.Base;
using Microsoft.AspNetCore.Http;

namespace WebApp.Helpers
{
    /// <summary>
    /// Class for getting the logged in User's username
    /// </summary>
    public class UserNameProvider : IUserNameProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        public UserNameProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Current User's Username
        /// </summary>
        public string CurrentUserName  => _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "-";
    }
}