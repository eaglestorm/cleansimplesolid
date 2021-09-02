using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Security.Claims;
using CleanSimpleSolid.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Infrastructure;
using Microsoft.Net.Http.Headers;
using ServiceBase.Core.Services;

namespace ServiceBase.Controllers
{
    public class SessionController : Controller
    {
        /// <summary>
        /// Notice this doesn't use an interface.  It's not using an interface
        /// </summary>
        private readonly UserService _userService;

        public SessionController(UserService userService)
        {
            _userService = userService;
        }
        
        /// <summary>
        /// Check the user exists and get their info if it doesn't.
        /// </summary>
        /// <returns></returns>
        // GET
        [Authorize]
        [HttpGet("/init")]
        public IActionResult Init()
        {
            _userService.ValidateSubject(User);
            
            return Ok();
        }
    }
}