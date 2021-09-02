using System.Threading.Tasks;
using CleanSimpleSolid.Core.Model.User;

namespace CleanSimpleSolid.Core.Interfaces.Repository
{
    public interface IUserRepository
    {
        /// <summary>
        /// Get the user based on the external identity.
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        Task<CssUser> GetBySubject(string subject);
        
        /// <summary>
        /// Add a new user for an authenticated user that we don't know about.
        /// </summary>
        /// <param name="cssUser"></param>
        Task Insert(CssUser cssUser);

        /// <summary>
        /// Update the user, either after login or from an external event.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task Update(CssUser user);
    }
}