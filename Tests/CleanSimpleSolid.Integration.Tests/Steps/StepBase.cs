using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FluentAssertions;
using Gauge.CSharp.Lib;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using RestSharp;
using RestSharp.Authenticators;

namespace CleanSimpleSolid.Integration.Tests.Steps
{
    public abstract class StepBase
    {
        protected IRestResponse SetupRequest(string path, object data)
        {
            var client = new RestClient(GetUrl(path));
            var token = GetToken();
            client.Authenticator = new JwtAuthenticator(token);
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);

            //request.AddHeader("Authorization", token);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", data, ParameterType.RequestBody);
            GaugeMessages.WriteMessage(client.BaseUrl.ToString());
            
            IRestResponse response = client.Execute(request);
            return response;
        }

        protected void ValidateOk(IRestResponse response)
        {
            GaugeMessages.WriteMessage(response.StatusDescription);
            GaugeMessages.WriteMessage(response.Content);
            response.IsSuccessful.Should().BeTrue();
        }

        private Uri GetUrl(string path)
        {
            var baseUrl = new Uri(Environment.GetEnvironmentVariable(Constants.Host));
            return new Uri(baseUrl, path);
        }

        private string GetToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateJwtSecurityToken(
                "eldar systems",
                "http://localhost:5000",
                new ClaimsIdentity(new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, "testing@jsmith.com.au"),
                    new Claim(ClaimTypes.Name, "John Smith"),
                    new Claim(ClaimTypes.NameIdentifier, "jsmith")
                }),
                DateTime.UtcNow.AddDays(-1),
                DateTime.UtcNow.AddDays(1),
                DateTime.UtcNow,
                new SigningCredentials(
                    new SymmetricSecurityKey(
                        Convert.FromBase64String(Environment.GetEnvironmentVariable(Constants.SecurityKey))),
                    SecurityAlgorithms.HmacSha256Signature)
            );

            return tokenHandler.WriteToken(token);
        }
    }
}