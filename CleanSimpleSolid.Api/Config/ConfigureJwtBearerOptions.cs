using System.Security.Claims;
using CleanDdd.Common.Model.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ServiceBase.Config
{
    public class ConfigureJwtBearerOptions: IPostConfigureOptions<JwtBearerOptions>
    {
        private readonly JwtAuthentication _jwtAuthentication;

        public ConfigureJwtBearerOptions(JwtAuthentication jwtAuthentication)
        {
            _jwtAuthentication = jwtAuthentication ?? throw new System.ArgumentNullException(nameof(jwtAuthentication));
        }

        public void PostConfigure(string name, JwtBearerOptions options)
        {
            var jwtAuthentication = _jwtAuthentication;

            options.ClaimsIssuer = jwtAuthentication.ValidIssuer;
            options.IncludeErrorDetails = true;
            options.RequireHttpsMetadata = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateActor = false,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtAuthentication.ValidIssuer,
                ValidAudience = jwtAuthentication.ValidAudience,
                IssuerSigningKey = jwtAuthentication.SymmetricSecurityKey,
                NameClaimType = ClaimTypes.NameIdentifier
            };
            options.Authority = _jwtAuthentication.Authority;
            
        }
    }
}