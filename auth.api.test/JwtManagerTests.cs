using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace auth.api.test;

public class JwtManagerTests
{
    private IConfiguration configStub = new StubConfig();

    [Fact]
    public void Test2()
    {
        // Arrange
        var sut = new JwtManager(configStub);
        var mockUser = new User
        {
            FirstName = "Dapper",
            LastName = "Labs",
            Email = "test@test.com"
        };


        // Act
        var result = sut.CreateToken(mockUser);

        // Assert
        Assert.IsType<string>(result);
    }
}

public class StubConfig : IConfiguration
{
    public string this[string key] { get => "my super secret key"; set => throw new NotImplementedException(); }

    public IEnumerable<IConfigurationSection> GetChildren()
    {
        throw new NotImplementedException();
    }

    public IChangeToken GetReloadToken()
    {
        throw new NotImplementedException();
    }

    public IConfigurationSection GetSection(string key)
    {
        throw new NotImplementedException();
    }
}
