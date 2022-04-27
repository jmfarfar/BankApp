using Microsoft.AspNetCore.Http;
using System.Linq;

namespace BankApp.Core.JWT
{
    public class UserSession : IUserSession
    {
        // retrieve the username from the httpcontext
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserSession()
        {
            var username = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(u => u.Type == "username")?.Value;
            return username;
        }
    }
}
