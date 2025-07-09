using Microsoft.AspNetCore.Mvc;
using Moq;
using ReciClan.Controllers;
using ReciClan.Services;
using Xunit;

namespace TestReciClan;

public class AuthTests
{
    private readonly Mock<IAuthService> _authMock;
    private readonly AuthController _controller;

    public AuthTests()
    {
        _authMock = new Mock<IAuthService>();
        _controller = new AuthController(_authMock.Object);
    }

    [Fact]
    public void Login_ConCredencialesValidas_RetornaToken()
    {
        const string tokenEsperado = "TOKEN-123";
        _authMock.Setup(a => a.Login("user@test.com", "valid123"))
            .Returns(tokenEsperado);

        var result = _controller.Login(new LoginReq("user@test.com", "valid123"));

        var ok = Assert.IsType<OkObjectResult>(result);
        var body = Assert.IsType<LoginRes>(ok.Value);
        Assert.Equal(tokenEsperado, body.Token);

        _authMock.Verify(a => a.Login("user@test.com", "valid123"), Times.Once);
    }

    [Fact]
    public void Login_ConCredencialesInvalidas_RetornaUnauthorized()
    {
        _authMock.Setup(a => a.Login("user@test.com", "wrong"))
            .Returns<string?>(null);

        var result = _controller.Login(new LoginReq("user@test.com", "wrong"));

        Assert.IsType<UnauthorizedObjectResult>(result);
        _authMock.Verify(a => a.Login("user@test.com", "wrong"), Times.Once);
    }
}