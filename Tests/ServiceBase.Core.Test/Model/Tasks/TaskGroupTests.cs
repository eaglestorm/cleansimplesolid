using CleanSimpleSolid.Core.Model.Tasks;
using Xunit;

namespace ServiceBase.Core.Test.Model.Context
{
    public class TaskGroupTests
    {
        private const string GroupName = "Group1";

        [Fact]
        public void CanCreateGroup()
        {
            var group = new TaskGroup(GroupName);
            
            Assert.Equal(GroupName,group.Name);
        }

        [Theory]
        [InlineData("Group1")]
        [InlineData("Group1alsdkfjalsdkfjlsdkfjfaewfasdfasdfaseaeasdfsd")]
        [InlineData("G")]
        [InlineData("1234567890")]
        public void CanCreateGroupValidName(string name)
        {
            var group = new TaskGroup(name);
            
            Assert.Equal(name,group.Name);
        }

        [Theory]
        [InlineData("Group1#")]
        [InlineData("Group1alsdkfjalsdkfjlsdkfjfaewfasdfasdfaseaeasdfsd2")]
        [InlineData(null)]
        [InlineData("Group2<")]
        public void InvalidNameHasErrors(string name)
        {
            var group = new TaskGroup(name);
            
            Assert.False(group.IsValid());
        }
        
    }
}