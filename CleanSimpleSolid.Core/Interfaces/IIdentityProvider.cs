using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using CleanSimpleSolid.Core.Model.User;

namespace CleanSimpleSolid.Core.Interfaces
{
    /// <summary>
    /// Defines methods that interface with the configured identity provider.
    /// </summary>
    public interface IIdentityProvider
    {
        /// <summary>
        /// Gets the user info from the identity provider and maps it to an internal user.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<CssUser> GetUser(JwtSecurityToken token);
    }
}