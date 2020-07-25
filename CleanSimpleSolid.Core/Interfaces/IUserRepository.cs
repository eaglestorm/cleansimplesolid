using System.Threading.Tasks;
using CleanSimpleSolid.Core.Model.User;

namespace CleanSimpleSolid.Core.Interfaces
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
    }
}