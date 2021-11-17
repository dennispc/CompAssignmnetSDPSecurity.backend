using CompAssignmnetSDPSecurity.Core.Models;
using Xunit;

namespace CompAssignmnetSDPSecurity.Core.Test.Models
{
    public class UserTest
    {
        private User _user;
        public UserTest()
        {
            _user = new User();
        }
        
        [Fact]
        public void User_CanBeInitialized()
        {
            var user = new User();
            Assert.NotNull(user);
        }

        [Fact]
        public void User_Id_MustBeInt()
        {
            Assert.True(_user.Id is int);
        }

        [Fact]
        public void User_SetId_StoresId()
        {
            var User = new User();
            User.Id = 1;
            Assert.Equal(1, User.Id);
        }

        [Fact]
        public void User_SetName_StoresNameAsString()
        {
            var User = new User();
            User.Name = "user";
            Assert.Equal("user", User.Name);
        }
    }
}