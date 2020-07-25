using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using CleanSimpleSolid.Core.Interfaces;

namespace CleanSimpleSolid.Core.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IIdentityProvider _identityProvider;

        public UserService(IUserRepository userRepository, IIdentityProvider identityProvider)
        {
            _userRepository = userRepository;
            _identityProvider = identityProvider;
        }
        
        public async Task<bool> ValidateSubject(string header)
        {
            var token = new JwtSecurityToken(header);
            var user = await _userRepository.GetBySubject(token.Subject);
            if (user == null)
            {
                //we have a valid token but we don't know who the user is.
                //Either we can get the user or throw an error.
                //Ideally we should have a message broker telling us about all new registered users
                // or if we don't need a message broker we can call the user info endpoint.
                user = await _identityProvider.GetUser(token);
                await _userRepository.Insert(user);
            }

            return true;
        }
    }
}