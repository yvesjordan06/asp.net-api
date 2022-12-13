using BankingServer.Auth.Domain.Models;
using Xunit;

namespace BankingServer.Auth.Domain.Test.Models;

public class UserModelTest
{
    [Fact]
    public void UserModel_ShouldHasPassword()
    {
        var user = new UserModel(
            id: 1,
            email: "test@test",
            password: "password",
            username: "test"
        );
        
        Assert.False(user.CheckPassword("password"));
        
        user.SetPassword("password");
        
        Assert.True(user.CheckPassword("password"));
        
        Assert.Equal("test@test", user.Email);
        Assert.Equal("test", user.Username);
        
        Assert.Equal(1, user.Id);
        
        user.SetPassword("password2");
        
        Assert.True(user.CheckPassword("password2"));
        Assert.False(user.CheckPassword("password"));
    }
}