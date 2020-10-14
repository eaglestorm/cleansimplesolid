using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CleanConnect.Common.Model.Errors;
using CleanDdd.Common.Model.Errors;
using CleanSimpleSolid.Core.Interfaces;
using CleanSimpleSolid.Core.Model.User;

namespace CleanSimpleSolid.Core.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        //private readonly IIdentityProvider _identityProvider;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<bool> ValidateSubject(ClaimsPrincipal claimsPrincipal)
        {
            //Claims Principal is a bloody mess.
            var subject = claimsPrincipal.FindFirst(ClaimTypes.Name).Value;
            var user = await _userRepository.GetBySubject(subject);
            if (user == null)
            {
                //we have a valid token but we don't know who the user is.
                //Either we can get the user, throw an error or just add the user.
                //Ideally we should have a message broker telling us about all new registered users
                // or if we don't need a message broker we can call the user info endpoint.
                //user = await _identityProvider.GetUser(token);
                var fullName = claimsPrincipal.FindFirst(ClaimTypes.GivenName) + " " +
                               claimsPrincipal.FindFirst(ClaimTypes.Surname);
                user = new CssUser(fullName,subject,claimsPrincipal.FindFirst(ClaimTypes.Email).Value);
                await _userRepository.Insert(user);
            }

            return true;
        }
    }
}