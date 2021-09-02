using System.Runtime.InteropServices;
using CleanSimpleSolid.Core.Model.User;
using Xunit;

namespace ServiceBase.Core.Test.Model.Users
{
    public class CssUserTests
    {
        public const string Name = "John Smith";
        public const string Subject = "jsmith";
        public const string Email = "jsmith@testing.com.au";
        
        [Fact]
        public void CanCreateUser()
        {
            var result = new CssUser(Name,Subject,Email);
            
            Assert.Equal(Name,result.Name);
            Assert.Equal(Subject, result.Subject);
            Assert.Equal(Email,result.Email);
        }

        [Theory]
        [InlineData("John Smith2")]
        [InlineData("J")]
        [InlineData("1234567890")]
        public void SetNameValid(string name)
        {
            var result = new CssUser(Name,Subject,Email);
            result.SetName(name);
            
            Assert.Equal(name,result.Name);
        }
        
        [Theory]
        [InlineData("John Smith2#")]
        [InlineData(null)]
        [InlineData("John Smith<")]
        public void SetNameInvalid(string name)
        {
            var result = new CssUser(Name,Subject,Email);
            result.SetName(name);
            
            Assert.False(result.IsValid());
        }
    }
}