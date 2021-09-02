using System.Security.Claims;
using System.Threading.Tasks;
using CleanSimpleSolid.Core.Model.User;

namespace CleanSimpleSolid.Core.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Validate that the user provided in the JWT exists in our database.
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        Task<bool> ValidateSubject(ClaimsPrincipal claimsPrincipal);

        Task<CssUser> GetCurrentUser(ClaimsPrincipal claimsPrincipal);
    }
}