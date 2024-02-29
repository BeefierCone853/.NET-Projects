using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using WebApi.FunctionalTests.Abstractions;
using WebApi.FunctionalTests.Contracts;

namespace WebApi.FunctionalTests.Authorization;

[Collection(nameof(SharedTestCollection))]
public class AuthorizationTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory), IAsyncLifetime
{
    [Fact]
    public async Task Should_RegisterUser_WhenEmailAndPasswordAreCorrectlyValidated()
    {
        // Arrange
        var request = new UserLoginRequest("cleanprojects@gmail.com", "Cleanproject1@");

        // Act
        var response = await UnauthorizedHttpClient.PostAsJsonAsync(RegisterEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Should_ReturnAccessToken_WhenLoginCredentialsAreProvided()
    {
        // Arrange
        var request = new UserLoginRequest("cleanprojects@gmail.com", "Cleanproject1@");
        await UnauthorizedHttpClient.PostAsJsonAsync(RegisterEndpoint, request);

        // Act
        var response = await UnauthorizedHttpClient.PostAsJsonAsync(LoginEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    public Task InitializeAsync() => ResetIdentityDatabase();

    public Task DisposeAsync() => Task.CompletedTask;
}