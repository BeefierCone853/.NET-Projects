using System.Net;
using System.Net.Http.Json;
using Application.Features.BlogPosts.DTOs;
using FluentAssertions;
using WebApi.FunctionalTests.Abstractions;

namespace WebApi.FunctionalTests.BlogPosts;

[Collection(nameof(SharedTestCollection))]
public class BlogPostAuthorizationTests(FunctionalTestWebAppFactory factory)
    : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task GetListShould_ReturnUnauthorized_WhenCredentialsNotProvided()
    {
        // Act
        var response = await UnauthorizedHttpClient.GetAsync($"{BlogPostEndpoint}?Page=1&PageSize=10");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetByIdShould_ReturnUnauthorized_WhenCredentialsNotProvided()
    {
        // Act
        var response = await UnauthorizedHttpClient.GetAsync($"{BlogPostEndpoint}/10");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task CreateShould_ReturnUnauthorized_WhenCredentialsNotProvided()
    {
        // Arrange
        var request = new CreateBlogPostDto("This is the title", "This is the description");

        // Act
        var response = await UnauthorizedHttpClient.PostAsJsonAsync(BlogPostEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task UpdateShould_ReturnUnauthorized_WhenCredentialsNotProvided()
    {
        // Arrange
        var updateRequest = new UpdateBlogPostDto("New title", "New description");

        // Act
        var response = await UnauthorizedHttpClient.PutAsJsonAsync($"{BlogPostEndpoint}/{10}", updateRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task DeleteShould_ReturnUnauthorized_WhenCredentialsNotProvided()
    {
        // Act
        var response = await UnauthorizedHttpClient.DeleteAsync($"{BlogPostEndpoint}/10");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}