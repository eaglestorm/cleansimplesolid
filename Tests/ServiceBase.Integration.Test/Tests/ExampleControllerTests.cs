using System.Net;
using System.Net.Http.Headers;
using ServiceBase.Integration.Test.Config;
using Xunit;

namespace ServiceBase.Integration.Test.Tests
{
    public class ExampleControllerTests: IClassFixture<TestWebApplicationFactory<Startup>>
    {
        private readonly TestWebApplicationFactory<Startup> _factory;
        
        public ExampleControllerTests(TestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        public async void Get_Unauthorized(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            Assert.True(response.StatusCode == HttpStatusCode.Unauthorized);

        }
        
        [Theory]
        [InlineData("/")]
        public async void Get_authorized(string url)
        {
            var client = _factory.CreateClient();
            
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer","eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VybmFtZSIsIm5iZiI6MTU1OTEyNjg5NiwiZXhwIjoxNTYwMzM2NDk2LCJpYXQiOjE1NTkxMjY4OTYsImlzcyI6ImVsZGFyIHN5c3RlbXMiLCJhdWQiOiJzZXJ2aWNlIGJhc2UifQ.vX-QarYa4lsZsI0LqQ4rUEQyf8WFOPSfGlVa5Bp38Rw");

            var response = await client.GetAsync(url);

            Assert.True(response.StatusCode == HttpStatusCode.OK);

        }
    }
}